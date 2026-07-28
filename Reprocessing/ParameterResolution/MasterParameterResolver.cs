using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using WMTool.Business;
using WMTool.Reprocessing.Exceptions;
using WMTool.Reprocessing.Models;
using WMTool.Reprocessing.Repositories;

namespace WMTool.Reprocessing.ParameterResolution
{
    public class MasterParameterResolver
    {
        private readonly InvoiceDerivedDataRepository _derivedDataRepository;
        private readonly TripVehicleCodeRepository _tripVehicleCodeRepository;
        private readonly WMBusiness _business;
        private readonly MasterParameterQueryOverrideStore _queryOverrideStore;

        public MasterParameterResolver(
            InvoiceDerivedDataRepository derivedDataRepository,
            TripVehicleCodeRepository tripVehicleCodeRepository,
            WMBusiness business,
            MasterParameterQueryOverrideStore queryOverrideStore)
        {
            _derivedDataRepository = derivedDataRepository;
            _tripVehicleCodeRepository = tripVehicleCodeRepository;
            _business = business;
            _queryOverrideStore = queryOverrideStore;
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

        // Roda a query cadastrada (se houver) pra esse parâmetro e usa a 1ª coluna da 1ª linha como
        // valor resolvido. A query usa os mesmos binds ":varIDInvoice"/":varcSerie"/
        // ":varcIDBranchInvoice"/":cIDCompany" das views (convertidos aqui pra "@nome"), resolvidos com
        // os 4 inputs da tela — não dá pra usar parâmetros AINDA NÃO resolvidos (ex.: :varcIDCustomer),
        // porque a ordem de resolução dos demais parâmetros não é garantida. Se não houver query
        // cadastrada, ou a query falhar/não retornar linha, devolve null — quem chama cai pro próximo
        // critério de resolução (não trava o reprocessamento por causa de uma query customizada ruim).
        private async Task<string> TryResolveFromConfiguredQueryAsync(string parameterName, MasterParameterInputs inputs, string connectionString)
        {
            string sqlTemplate = _queryOverrideStore.GetSql(parameterName);
            if (string.IsNullOrWhiteSpace(sqlTemplate))
            {
                return null;
            }

            string sql = Regex.Replace(sqlTemplate, @"(?<![A-Za-z0-9_@:]):([A-Za-z0-9_]+)", "@$1");
            var parameters = new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase)
            {
                ["@varIDInvoice"] = inputs.CIDInvoice,
                ["@varcSerie"] = inputs.CSerie,
                ["@varcIDBranchInvoice"] = inputs.CIDBranchInvoice,
                ["@cIDCompany"] = inputs.CIDCompany
            };

            try
            {
                DataTable table = await _business.ConsultDB(sql, connectionString, parameters);
                if (table.Rows.Count == 0 || table.Columns.Count == 0)
                {
                    return null;
                }

                object value = table.Rows[0][0];
                return value == DBNull.Value ? string.Empty : Convert.ToString(value, System.Globalization.CultureInfo.InvariantCulture);
            }
            catch (Exception ex)
            {
                Utils.LogError.Log(ex);
                return null;
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

            foreach (string name in required)
            {
                if (overrides.TryGetValue(name, out string overrideValue))
                {
                    values[name] = overrideValue;
                    continue;
                }

                string canonicalName = Canonicalize(name);

                if (!BaseInputParameterNames.Contains(canonicalName))
                {
                    string configuredValue = await TryResolveFromConfiguredQueryAsync(name, inputs, connectionString);
                    if (configuredValue != null)
                    {
                        values[name] = configuredValue;
                        continue;
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
