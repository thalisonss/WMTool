using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using WMTool.Business;
using WMTool.Reprocessing.Exceptions;
using WMTool.Reprocessing.Models;
using WMTool.Reprocessing.Repositories;
using WMTool.Validation;

namespace WMTool.Reprocessing.ParameterResolution
{
    public class MasterParameterResolver
    {
        private readonly InvoiceDerivedDataRepository _derivedDataRepository;
        private readonly TripVehicleCodeRepository _tripVehicleCodeRepository;
        private readonly WMBusiness _business;
        private readonly MasterParameterQueryOverrideStore _queryOverrideStore;
        private readonly MasterParameterGeneralQueryStore _generalQueryStore;

        public MasterParameterResolver(
            InvoiceDerivedDataRepository derivedDataRepository,
            TripVehicleCodeRepository tripVehicleCodeRepository,
            WMBusiness business,
            MasterParameterQueryOverrideStore queryOverrideStore,
            MasterParameterGeneralQueryStore generalQueryStore)
        {
            _derivedDataRepository = derivedDataRepository;
            _tripVehicleCodeRepository = tripVehicleCodeRepository;
            _business = business;
            _queryOverrideStore = queryOverrideStore;
            _generalQueryStore = generalQueryStore;
        }

        public async Task<IReadOnlyList<RequiredParameterInfo>> SuggestAsync(
            MasterParameterInputs inputs, IEnumerable<string> requiredParameterNames, string connectionString)
        {
            Dictionary<string, string> resolved = await ResolveValuesAsync(inputs, requiredParameterNames, connectionString, throwOnUnresolved: false);

            return resolved
                .Select(kv => new RequiredParameterInfo { ParameterName = kv.Key, SuggestedValue = kv.Value })
                .ToList();
        }

        public async Task<MasterParameterSet> ResolveAsync(
            MasterParameterInputs inputs, IEnumerable<string> requiredParameterNames, string connectionString)
        {
            Dictionary<string, string> resolved = await ResolveValuesAsync(inputs, requiredParameterNames, connectionString, throwOnUnresolved: true);
            return new MasterParameterSet(resolved);
        }

        private static readonly HashSet<string> DerivedParameterNames = new HashSet<string>
        {
            "varcIDCustomer", "varcIDTrip", "varcForm", "varcIDOrder", "varcIDLE"
        };

        // Nem toda view segue a convenção "var"+nome (ex.: Custom_WM_NF_Taxes declara "cIDOrder", não
        // "varcIDOrder", para o mesmo parâmetro semântico). Para não duplicar um case por variação,
        // normalizamos para o nome canônico "var..." antes de resolver — mas o valor resolvido continua
        // sendo gravado sob o nome ORIGINAL, que é o que a view realmente usa como bind (:nome).
        private static readonly HashSet<string> KnownCanonicalNames = new HashSet<string>
        {
            "varIDInvoice", "varcSerie", "varcIDBranchInvoice", "varcIDCustomer",
            "varcIDTrip", "varcForm", "varcIDOrder", "varcIDLE", "varcIDLI", "varcIDLD", "varxSector"
        };

        // Os 4 inputs digitados na tela nunca precisam de uma query de descoberta — já são exatos por
        // definição. Toda a variação/incerteza está nos parâmetros DERIVADOS (LE, LI, LD, xSector,
        // cIDCustomer etc.), que é justamente onde faz sentido cadastrar uma query de descoberta.
        private static readonly HashSet<string> BaseInputParameterNames = new HashSet<string>
        {
            "varIDInvoice", "varcSerie", "varcIDBranchInvoice", "cIDCompany"
        };

        // As duas formas de descoberta configurável (CustomSql e GeneralResult) usam os mesmos
        // placeholders "{cIDInvoice}"/"{cSerie}"/"{cIDBranchInvoice}"/"{cIDCompany}" das regras de
        // Validação JSON x Banco e Banco x Banco (RuleParameterParser) — um único padrão em toda a
        // aplicação pra referenciar os 4 campos do topo da tela numa query, em vez de dois (esse aqui
        // usava ":varIDInvoice", estilo do bind das views da DSL, o que não tem nada a ver pro usuário
        // que só quer os 4 campos digitados). Resolvidos com os 4 inputs da tela — não dá pra usar
        // parâmetros AINDA NÃO resolvidos (ex.: {cIDCustomer}), porque a ordem de resolução dos demais
        // parâmetros não é garantida.
        private static string ToParameterizedSql(string sqlTemplate)
        {
            return RuleParameterParser.ToParameterizedSql(sqlTemplate);
        }

        private static Dictionary<string, object> BuildBaseParameters(MasterParameterInputs inputs)
        {
            return new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase)
            {
                ["@cIDInvoice"] = inputs.CIDInvoice,
                ["@cSerie"] = inputs.CSerie,
                ["@cIDBranchInvoice"] = inputs.CIDBranchInvoice,
                ["@cIDCompany"] = inputs.CIDCompany
            };
        }

        // Roda a query dedicada de um parâmetro (SourceType == CustomSql) e usa a 1ª coluna da 1ª linha
        // como valor resolvido. Se a query falhar/não retornar linha, devolve null — quem chama cai pro
        // próximo critério de resolução (não trava o reprocessamento por causa de uma query ruim).
        private async Task<string> TryResolveCustomSqlAsync(string sqlTemplate, MasterParameterInputs inputs, string connectionString)
        {
            if (string.IsNullOrWhiteSpace(sqlTemplate))
            {
                return null;
            }

            try
            {
                DataTable table = await _business.ConsultDB(ToParameterizedSql(sqlTemplate), connectionString, BuildBaseParameters(inputs));
                if (table.Rows.Count == 0 || table.Columns.Count == 0)
                {
                    return null;
                }

                object value = table.Rows[0][0];
                return value == DBNull.Value ? string.Empty : Convert.ToString(value, CultureInfo.InvariantCulture);
            }
            catch (Exception ex)
            {
                Utils.LogError.Log(ex);
                return null;
            }
        }

        // A Query Geral roda uma única vez por resolução (não uma vez por parâmetro) e guarda só a
        // primeira linha — todo parâmetro com SourceType == GeneralResult lê uma coluna dessa mesma
        // linha. Mesmo papel do SqlRuleHelper.ResolveGeneralRowAsync (ValidationEngine/
        // DatabaseComparisonEngine), sem a dependência cruzada com o módulo de Validation.
        private async Task<(DataRow Row, string Error)> ResolveGeneralRowAsync(MasterParameterInputs inputs, string connectionString)
        {
            string sqlTemplate = _generalQueryStore.Load();
            if (string.IsNullOrWhiteSpace(sqlTemplate))
            {
                return (null, "Query Geral de parâmetros não configurada.");
            }

            try
            {
                DataTable table = await _business.ConsultDB(ToParameterizedSql(sqlTemplate), connectionString, BuildBaseParameters(inputs));
                if (table.Rows.Count == 0)
                {
                    return (null, "Query Geral de parâmetros não retornou nenhum registro.");
                }

                return (table.Rows[0], null);
            }
            catch (Exception ex)
            {
                Utils.LogError.Log(ex);
                return (null, "Erro ao executar a Query Geral de parâmetros: " + ex.Message);
            }
        }

        private static string Canonicalize(string name)
        {
            if (KnownCanonicalNames.Contains(name))
            {
                return name;
            }

            string withVarPrefix = "var" + name;
            return KnownCanonicalNames.Contains(withVarPrefix) ? withVarPrefix : name;
        }

        private async Task<Dictionary<string, string>> ResolveValuesAsync(
            MasterParameterInputs inputs, IEnumerable<string> requiredParameterNames, string connectionString, bool throwOnUnresolved)
        {
            var values = new Dictionary<string, string>();
            IDictionary<string, string> overrides = inputs.Overrides ?? new Dictionary<string, string>();
            List<string> required = requiredParameterNames.Distinct().ToList();
            List<string> canonicalRequired = required.Select(Canonicalize).ToList();

            bool needsInvoiceRow = canonicalRequired.Any(DerivedParameterNames.Contains) || required.Contains("cIDUser");

            DerivedInvoiceData derived = needsInvoiceRow
                ? await _derivedDataRepository.GetInvoiceDataAsync(inputs, connectionString)
                : null;

            string orderId = canonicalRequired.Contains("varcIDOrder")
                ? await _derivedDataRepository.GetOrderIdAsync(inputs, derived?.CForm, connectionString)
                : null;

            string vehicleCode = required.Contains("cIDUser")
                ? await _tripVehicleCodeRepository.GetVehicleCodeAsync(derived?.CIDTrip, inputs.CIDCompany, connectionString)
                : null;

            Dictionary<string, MasterParameterQueryOverride> queryOverrides = required
                .Select(n => _queryOverrideStore.Get(n))
                .Where(o => o != null)
                .ToDictionary(o => o.ParameterName, StringComparer.OrdinalIgnoreCase);

            bool needsGeneralRow = queryOverrides.Values.Any(o => o.SourceType == ParameterDiscoverySourceType.GeneralResult);
            DataRow generalRow = null;

            if (needsGeneralRow)
            {
                (DataRow row, string error) generalRowResult = await ResolveGeneralRowAsync(inputs, connectionString);
                generalRow = generalRowResult.row;
                if (generalRow == null)
                {
                    Utils.LogError.Log(new InvalidOperationException(generalRowResult.error));
                }
            }

            foreach (string name in required)
            {
                if (overrides.TryGetValue(name, out string overrideValue))
                {
                    values[name] = overrideValue;
                    continue;
                }

                string canonicalName = Canonicalize(name);

                if (!BaseInputParameterNames.Contains(canonicalName) && queryOverrides.TryGetValue(name, out MasterParameterQueryOverride queryOverride))
                {
                    if (queryOverride.SourceType == ParameterDiscoverySourceType.GeneralResult)
                    {
                        string columnName = string.IsNullOrWhiteSpace(queryOverride.ResultColumn) ? name : queryOverride.ResultColumn;

                        if (generalRow != null && generalRow.Table.Columns.Contains(columnName))
                        {
                            object value = generalRow[columnName];
                            values[name] = value == DBNull.Value ? string.Empty : Convert.ToString(value, CultureInfo.InvariantCulture);
                            continue;
                        }
                        // Query Geral indisponível ou sem essa coluna (generalRowError tem o motivo):
                        // cai pro default abaixo, sem travar o reprocessamento inteiro.
                    }
                    else
                    {
                        string configuredValue = await TryResolveCustomSqlAsync(queryOverride.Sql, inputs, connectionString);
                        if (configuredValue != null)
                        {
                            values[name] = configuredValue;
                            continue;
                        }
                    }
                }

                switch (canonicalName)
                {
                    case "varIDInvoice":
                        values[name] = inputs.CIDInvoice;
                        break;

                    case "varcSerie":
                        values[name] = inputs.CSerie;
                        break;

                    case "varcIDBranchInvoice":
                        values[name] = inputs.CIDBranchInvoice;
                        break;

                    case "cIDCompany":
                        values[name] = inputs.CIDCompany;
                        break;

                    case "varcIDCustomer":
                        values[name] = derived?.CIDCustomer ?? string.Empty;
                        break;

                    case "varcIDTrip":
                        values[name] = derived?.CIDTrip ?? string.Empty;
                        break;

                    case "varcForm":
                        values[name] = derived?.CForm ?? string.Empty;
                        break;

                    case "varcIDOrder":
                        values[name] = orderId ?? string.Empty;
                        break;

                    case "varcIDLE":
                        values[name] = derived?.CIDCustomer ?? string.Empty;
                        break;

                    case "varcIDLI":
                    case "varcIDLD":
                    case "varxSector":
                        values[name] = string.Empty;
                        break;

                    case "cIDUser":
                        // Só usado por Custom_WM_NF_Transportadora, que não declara esse parâmetro em
                        // <parameters>; o valor real é o código do veículo (cCodeVehicle) da viagem.
                        values[name] = vehicleCode ?? string.Empty;
                        break;

                    case "cIDLanguage":
                        // Usado por Custom_WM_NF_ProductOrder para escolher a tradução do produto
                        // (MC1_ProductLang). Não varia por nota — é uma constante da empresa; "1" é o
                        // único valor observado em MC1_ProductLang para esta base.
                        values[name] = "1";
                        break;

                    case "varVersionDevice":
                        // Algumas views (ex.: Custom_WM_NF_ProductOrder, Custom_WM_NF_ProductTot) usam esse
                        // parâmetro só para decidir entre uma fórmula antiga e uma nova, comparando
                        // ":varVersionDevice >= '<data-de-corte>'". Passar o timestamp atual sempre ativa a
                        // fórmula mais recente, que é o comportamento equivalente ao de uma emissão em tempo real.
                        values[name] = DateTime.Now.ToString("yyyyMMddHHmmss");
                        break;

                    default:
                        if (throwOnUnresolved)
                        {
                            throw new UnresolvedParameterException(name);
                        }

                        values[name] = string.Empty;
                        break;
                }
            }

            return values;
        }
    }
}
