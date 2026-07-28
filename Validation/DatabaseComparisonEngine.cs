using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using WMTool.Business;
using WMTool.Validation.Models;

namespace WMTool.Validation
{
    // Comparação banco-a-banco: os dois lados de cada regra (origem/destino) vêm de SQL rodado na
    // MESMA connection string — útil pra comparar, por exemplo, uma view nova com a tabela original, ou
    // dois cálculos que deveriam bater. Reaproveita a mesma lógica de Query Geral/parâmetros/comparação
    // do ValidationEngine via SqlRuleHelper.
    public class DatabaseComparisonEngine
    {
        private readonly WMBusiness _business;

        public DatabaseComparisonEngine(WMBusiness business)
        {
            _business = business;
        }

        public async Task<List<ValidationRuleResult>> RunAsync(
            IEnumerable<DbComparisonRule> rules,
            string connectionString,
            ValidationContextInputs context,
            string generalOriginSqlTemplate,
            string generalDestinationSqlTemplate)
        {
            (DataRow originGeneralRow, string originGeneralError) =
                await SqlRuleHelper.ResolveGeneralRowAsync(_business, generalOriginSqlTemplate, connectionString, context);
            (DataRow destinationGeneralRow, string destinationGeneralError) =
                await SqlRuleHelper.ResolveGeneralRowAsync(_business, generalDestinationSqlTemplate, connectionString, context);

            var results = new List<ValidationRuleResult>();

            foreach (DbComparisonRule rule in rules)
            {
                results.Add(await RunRuleAsync(
                    rule, connectionString, context,
                    originGeneralRow, originGeneralError,
                    destinationGeneralRow, destinationGeneralError));
            }

            return results;
        }

        private async Task<ValidationRuleResult> RunRuleAsync(
            DbComparisonRule rule,
            string connectionString,
            ValidationContextInputs context,
            DataRow originGeneralRow,
            string originGeneralError,
            DataRow destinationGeneralRow,
            string destinationGeneralError)
        {
            var result = new ValidationRuleResult { RuleName = rule.Name };

            (bool originOk, string originValue, string originError) = await ResolveValueAsync(
                rule.OriginSourceType, rule.OriginSqlTemplate, rule.OriginResultColumn,
                connectionString, context, originGeneralRow, originGeneralError, "origem");

            if (!originOk)
            {
                result.Passed = false;
                result.Message = originError;
                return result;
            }

            result.Expected = originValue;

            (bool destinationOk, string destinationValue, string destinationError) = await ResolveValueAsync(
                rule.DestinationSourceType, rule.DestinationSqlTemplate, rule.DestinationResultColumn,
                connectionString, context, destinationGeneralRow, destinationGeneralError, "destino");

            if (!destinationOk)
            {
                result.Passed = false;
                result.Message = destinationError;
                return result;
            }

            result.Actual = destinationValue;
            result.Passed = SqlRuleHelper.Compare(result.Expected, result.Actual, rule.Comparison);

            return result;
        }

        private async Task<(bool Ok, string Value, string Error)> ResolveValueAsync(
            ComparisonSourceType sourceType,
            string sqlTemplate,
            string resultColumn,
            string connectionString,
            ValidationContextInputs context,
            DataRow generalRow,
            string generalRowError,
            string sideLabel)
        {
            DataRow sourceRow;

            if (sourceType == ComparisonSourceType.GeneralResult)
            {
                if (generalRow == null)
                {
                    return (false, null, generalRowError ?? $"Resultado geral ({sideLabel}) indisponível.");
                }

                sourceRow = generalRow;
            }
            else
            {
                if (!SqlRuleHelper.TryBuildParameterizedSql(sqlTemplate, context, out string sql, out Dictionary<string, object> parameters, out string placeholderError))
                {
                    return (false, null, placeholderError);
                }

                DataTable dataTable;
                try
                {
                    dataTable = await _business.ConsultDB(sql, connectionString, parameters);
                }
                catch (Exception ex)
                {
                    return (false, null, $"Erro ao consultar o banco ({sideLabel}): {ex.Message}");
                }

                if (dataTable.Rows.Count == 0)
                {
                    return (false, null, $"Nenhum registro encontrado no banco ({sideLabel}).");
                }

                sourceRow = dataTable.Rows[0];
            }

            if (!sourceRow.Table.Columns.Contains(resultColumn))
            {
                string origin = sourceType == ComparisonSourceType.GeneralResult ? "no resultado geral" : "no resultado da query";
                return (false, null, $"Coluna '{resultColumn}' ({sideLabel}) não veio {origin}.");
            }

            object value = sourceRow[resultColumn];
            string stringValue = SqlRuleHelper.ToInvariantString(value);
            return (true, stringValue, null);
        }

        // Comparação linha-a-linha (N itens): cada lado vira um dicionário chave->valor (ex.: nItem ->
        // qCom), casados pela chave. Gera um ValidationRuleResult por chave encontrada em QUALQUER um
        // dos dois lados — item que só existe de um lado já é um resultado "Falhou" próprio, não passa
        // batido.
        public async Task<List<ValidationRuleResult>> RunRowSetRulesAsync(
            IEnumerable<DbRowSetComparisonRule> rules,
            string connectionString,
            ValidationContextInputs context,
            string generalOriginRowSetSqlTemplate,
            string generalDestinationRowSetSqlTemplate)
        {
            (DataTable originGeneralTable, string originGeneralError) =
                await SqlRuleHelper.ResolveGeneralTableAsync(_business, generalOriginRowSetSqlTemplate, connectionString, context);
            (DataTable destinationGeneralTable, string destinationGeneralError) =
                await SqlRuleHelper.ResolveGeneralTableAsync(_business, generalDestinationRowSetSqlTemplate, connectionString, context);

            var results = new List<ValidationRuleResult>();

            foreach (DbRowSetComparisonRule rule in rules)
            {
                results.AddRange(await RunRowSetRuleAsync(
                    rule, connectionString, context,
                    originGeneralTable, originGeneralError,
                    destinationGeneralTable, destinationGeneralError));
            }

            return results;
        }

        private async Task<List<ValidationRuleResult>> RunRowSetRuleAsync(
            DbRowSetComparisonRule rule,
            string connectionString,
            ValidationContextInputs context,
            DataTable originGeneralTable,
            string originGeneralError,
            DataTable destinationGeneralTable,
            string destinationGeneralError)
        {
            var results = new List<ValidationRuleResult>();

            (Dictionary<string, string> originValues, string originError) = await ResolveKeyedValuesAsync(
                rule.OriginSourceType, rule.OriginSqlTemplate, rule.OriginKeyColumn, rule.OriginValueColumn,
                connectionString, context, originGeneralTable, originGeneralError, "origem");

            if (originError != null)
            {
                results.Add(new ValidationRuleResult { RuleName = rule.Name, Passed = false, Message = originError });
                return results;
            }

            (Dictionary<string, string> destinationValues, string destinationError) = await ResolveKeyedValuesAsync(
                rule.DestinationSourceType, rule.DestinationSqlTemplate, rule.DestinationKeyColumn, rule.DestinationValueColumn,
                connectionString, context, destinationGeneralTable, destinationGeneralError, "destino");

            if (destinationError != null)
            {
                results.Add(new ValidationRuleResult { RuleName = rule.Name, Passed = false, Message = destinationError });
                return results;
            }

            var orderedKeys = new List<string>();
            var seenKeys = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
            foreach (string key in originValues.Keys.Concat(destinationValues.Keys))
            {
                if (seenKeys.Add(key))
                {
                    orderedKeys.Add(key);
                }
            }

            if (orderedKeys.Count == 0)
            {
                results.Add(new ValidationRuleResult
                {
                    RuleName = rule.Name,
                    Passed = false,
                    Message = "Nenhuma linha encontrada nem na origem nem no destino."
                });
                return results;
            }

            foreach (string key in orderedKeys)
            {
                var itemResult = new ValidationRuleResult { RuleName = $"{rule.Name} [{key}]" };

                bool hasOrigin = originValues.TryGetValue(key, out string originValue);
                bool hasDestination = destinationValues.TryGetValue(key, out string destinationValue);

                if (!hasOrigin)
                {
                    itemResult.Actual = destinationValue;
                    itemResult.Passed = false;
                    itemResult.Message = $"Item '{key}' não encontrado na origem.";
                }
                else if (!hasDestination)
                {
                    itemResult.Expected = originValue;
                    itemResult.Passed = false;
                    itemResult.Message = $"Item '{key}' não encontrado no destino.";
                }
                else
                {
                    itemResult.Expected = originValue;
                    itemResult.Actual = destinationValue;
                    itemResult.Passed = SqlRuleHelper.Compare(originValue, destinationValue, rule.Comparison);
                }

                results.Add(itemResult);
            }

            return results;
        }

        private async Task<(Dictionary<string, string> Values, string Error)> ResolveKeyedValuesAsync(
            ComparisonSourceType sourceType,
            string sqlTemplate,
            string keyColumn,
            string valueColumn,
            string connectionString,
            ValidationContextInputs context,
            DataTable generalTable,
            string generalTableError,
            string sideLabel)
        {
            DataTable table;

            if (sourceType == ComparisonSourceType.GeneralResult)
            {
                if (generalTable == null)
                {
                    return (null, generalTableError ?? $"Query Geral de conjunto ({sideLabel}) indisponível.");
                }

                table = generalTable;
            }
            else
            {
                (DataTable customTable, string error) = await SqlRuleHelper.RunQueryAsync(_business, sqlTemplate, connectionString, context, $"SQL ({sideLabel})");
                if (error != null)
                {
                    return (null, error);
                }

                table = customTable;
            }

            if (!table.Columns.Contains(keyColumn))
            {
                string origin = sourceType == ComparisonSourceType.GeneralResult ? "no resultado geral de conjunto" : "no resultado da query";
                return (null, $"Coluna de chave '{keyColumn}' ({sideLabel}) não veio {origin}.");
            }

            if (!table.Columns.Contains(valueColumn))
            {
                string origin = sourceType == ComparisonSourceType.GeneralResult ? "no resultado geral de conjunto" : "no resultado da query";
                return (null, $"Coluna de valor '{valueColumn}' ({sideLabel}) não veio {origin}.");
            }

            var values = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            foreach (DataRow row in table.Rows)
            {
                if (row[keyColumn] == DBNull.Value)
                {
                    continue;
                }

                string key = SqlRuleHelper.ToInvariantString(row[keyColumn]);
                string value = SqlRuleHelper.ToInvariantString(row[valueColumn]);
                values[key] = value;
            }

            return (values, null);
        }

        // Checagens de presença/ausência: não comparam origem x destino, só verificam se uma query acha
        // (ou não acha) alguma coisa — ex.: "XML com erro", "Pedido Split", "campo tal não pode ser nulo".
        public async Task<List<ValidationRuleResult>> RunPresenceRulesAsync(
            IEnumerable<DbPresenceRule> rules, string connectionString, ValidationContextInputs context)
        {
            var results = new List<ValidationRuleResult>();

            foreach (DbPresenceRule rule in rules)
            {
                results.Add(await RunPresenceRuleAsync(rule, connectionString, context));
            }

            return results;
        }

        private async Task<ValidationRuleResult> RunPresenceRuleAsync(
            DbPresenceRule rule, string connectionString, ValidationContextInputs context)
        {
            var result = new ValidationRuleResult { RuleName = rule.Name };

            (DataTable dataTable, string error) = await SqlRuleHelper.RunQueryAsync(_business, rule.SqlTemplate, connectionString, context, "SQL da checagem");

            if (error != null)
            {
                result.Passed = false;
                result.Message = error;
                return result;
            }

            int rowCount = dataTable.Rows.Count;
            bool exists = rowCount > 0;
            bool mustExist = rule.Expectation == PresenceExpectation.RowsMustExist;

            result.Expected = mustExist ? "Deve existir" : "Não deve existir";
            result.Actual = exists ? $"{rowCount} registro(s) encontrado(s)" : "Nenhum registro encontrado";
            result.Passed = mustExist ? exists : !exists;

            if (!result.Passed)
            {
                if (exists && !string.IsNullOrEmpty(rule.MessageColumn) && dataTable.Columns.Contains(rule.MessageColumn))
                {
                    List<string> messages = dataTable.Rows.Cast<DataRow>()
                        .Select(r => r[rule.MessageColumn] == DBNull.Value ? null : r[rule.MessageColumn].ToString())
                        .Where(m => !string.IsNullOrEmpty(m))
                        .Take(10)
                        .ToList();

                    if (messages.Count > 0)
                    {
                        result.Message = string.Join(" | ", messages);
                    }
                }

                if (string.IsNullOrEmpty(result.Message))
                {
                    result.Message = mustExist
                        ? "Nenhum registro encontrado, mas era esperado que existisse."
                        : $"{rowCount} registro(s) encontrado(s), quando não deveria haver nenhum.";
                }
            }

            return result;
        }

        private static readonly JsonSerializerSettings RuleFileSettings = new JsonSerializerSettings
        {
            Converters = { new StringEnumConverter() }
        };

        public static DbComparisonRuleSet LoadRules(string path)
        {
            string json = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<DbComparisonRuleSet>(json, RuleFileSettings) ?? new DbComparisonRuleSet();
        }

        public static void SaveRules(string path, DbComparisonRuleSet ruleSet)
        {
            string json = JsonConvert.SerializeObject(ruleSet, Formatting.Indented, RuleFileSettings);
            File.WriteAllText(path, json);
        }
    }
}
