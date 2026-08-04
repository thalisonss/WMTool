using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using WMTool.Business;
using WMTool.Validation;

namespace WMTool.InsertScript
{
    // Mesmo papel do SqlRuleHelper (Validation/SqlRuleHelper.cs), mas genérico: em vez de validar
    // contra os 4 campos fixos de ValidationContextInputs, valida contra um dicionário de
    // parâmetros arbitrário — aqui os placeholders são definidos pelo usuário na grid de
    // "Localizar/Substituir" (mais os valores de coluna já resolvidos da mesma linha), não um
    // conjunto fixo de nomes.
    public static class GenericSqlParameterResolver
    {
        public static bool TryBuildParameterizedSql(
            string sqlTemplate, IDictionary<string, string> parameterValues, out string sql, out Dictionary<string, object> parameters, out string error)
        {
            Dictionary<string, object> resolvedParameters = (parameterValues ?? new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase))
                .ToDictionary(kv => "@" + kv.Key, kv => (object)kv.Value, StringComparer.OrdinalIgnoreCase);
            parameters = resolvedParameters;

            List<string> undeclared = RuleParameterParser.ExtractPlaceholderNames(sqlTemplate)
                .Where(name => !resolvedParameters.ContainsKey("@" + name))
                .ToList();

            if (undeclared.Count > 0)
            {
                sql = null;
                string available = parameterValues == null || parameterValues.Count == 0
                    ? "(nenhum)"
                    : string.Join(", ", parameterValues.Keys.Select(k => "{" + k + "}"));

                error = $"SQL usa {{{string.Join("}}, {{", undeclared)}}}, mas os únicos parâmetros disponíveis são {available}.";
                return false;
            }

            sql = RuleParameterParser.ToParameterizedSql(sqlTemplate);
            error = null;
            return true;
        }

        // Roda o SQL e devolve a 1ª linha inteira (ou null + motivo) — usado pela Query Geral de
        // uma tabela, igual ao SqlRuleHelper.ResolveGeneralRowAsync.
        public static async Task<(DataRow Row, string Error)> RunRowAsync(
            WMBusiness business, string sqlTemplate, string connectionString, IDictionary<string, string> parameterValues)
        {
            if (string.IsNullOrWhiteSpace(sqlTemplate))
            {
                return (null, "Query não configurada.");
            }

            if (!TryBuildParameterizedSql(sqlTemplate, parameterValues, out string sql, out Dictionary<string, object> parameters, out string placeholderError))
            {
                return (null, placeholderError);
            }

            try
            {
                DataTable table = await business.ConsultDB(sql, connectionString, parameters);
                if (table.Rows.Count == 0)
                {
                    return (null, "Nenhum registro encontrado.");
                }

                return (table.Rows[0], null);
            }
            catch (Exception ex)
            {
                return (null, "Erro ao executar a query: " + ex.Message);
            }
        }

        // Roda o SQL e devolve a 1ª célula da 1ª linha (ou null + motivo) — usado pela query
        // dedicada de uma coluna, igual ao MasterParameterResolver.TryResolveCustomSqlAsync, mas
        // preservando o motivo do erro pra virar warning no motor de geração.
        public static async Task<(string Value, string Error)> RunScalarAsync(
            WMBusiness business, string sqlTemplate, string connectionString, IDictionary<string, string> parameterValues)
        {
            (DataRow row, string error) = await RunRowAsync(business, sqlTemplate, connectionString, parameterValues);
            if (row == null)
            {
                return (null, error);
            }

            if (row.Table.Columns.Count == 0)
            {
                return (null, "A query não retornou nenhuma coluna.");
            }

            object value = row[0];
            string result = value == DBNull.Value ? string.Empty : Convert.ToString(value, CultureInfo.InvariantCulture);
            return (result, null);
        }
    }
}
