using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using WMTool.Business;
using WMTool.Validation.Models;

namespace WMTool.Validation
{
    class ValidationEngine
    {
        private readonly WMBusiness _business;

        public ValidationEngine(WMBusiness business)
        {
            _business = business;
        }

        public async Task<List<ValidationRuleResult>> RunAsync(
            JObject json, IEnumerable<ValidationRule> rules, string connectionString, ValidationContextInputs context, string generalSqlTemplate)
        {
            (DataRow generalRow, string generalRowError) = await SqlRuleHelper.ResolveGeneralRowAsync(_business, generalSqlTemplate, connectionString, context);
            return await RunRulesAsync(json, rules.ToList(), connectionString, context, generalRow, generalRowError);
        }

        public async Task<List<FileValidationResult>> RunBatchAsync(
            string folderPath, IEnumerable<ValidationRule> rules, string connectionString, ValidationContextInputs context, string generalSqlTemplate)
        {
            var ruleList = rules.ToList();
            var fileResults = new List<FileValidationResult>();

            // A Query Geral usa os mesmos 4 identificadores do topo da tela pra todo o lote — roda uma
            // única vez aqui, não a cada arquivo, exatamente pelo mesmo motivo de não repetir a mesma
            // consulta pra cada campo.
            (DataRow generalRow, string generalRowError) = await SqlRuleHelper.ResolveGeneralRowAsync(_business, generalSqlTemplate, connectionString, context);

            foreach (string filePath in Directory.GetFiles(folderPath, "*.json"))
            {
                var fileResult = new FileValidationResult { FileName = Path.GetFileName(filePath) };

                try
                {
                    JObject json = JObject.Parse(File.ReadAllText(filePath));
                    fileResult.Results = await RunRulesAsync(json, ruleList, connectionString, context, generalRow, generalRowError);
                }
                catch (JsonException ex)
                {
                    fileResult.ParseError = ex.Message;
                }

                fileResults.Add(fileResult);
            }

            return fileResults;
        }

        private async Task<List<ValidationRuleResult>> RunRulesAsync(
            JObject json, List<ValidationRule> rules, string connectionString, ValidationContextInputs context, DataRow generalRow, string generalRowError)
        {
            var results = new List<ValidationRuleResult>();

            foreach (ValidationRule rule in rules)
            {
                results.Add(await RunRuleAsync(json, rule, connectionString, context, generalRow, generalRowError));
            }

            return results;
        }

        private async Task<ValidationRuleResult> RunRuleAsync(
            JObject json, ValidationRule rule, string connectionString, ValidationContextInputs context, DataRow generalRow, string generalRowError)
        {
            var result = new ValidationRuleResult { RuleName = rule.Name };

            JToken expectedToken = json.SelectToken(rule.JsonPath);
            if (expectedToken == null)
            {
                result.Passed = false;
                result.Message = $"Caminho '{rule.JsonPath}' não encontrado no JSON.";
                return result;
            }

            result.Expected = expectedToken.ToString();

            DataRow sourceRow;

            if (rule.SourceType == ComparisonSourceType.GeneralResult)
            {
                if (generalRow == null)
                {
                    result.Passed = false;
                    result.Message = generalRowError ?? "Resultado geral indisponível.";
                    return result;
                }

                sourceRow = generalRow;
            }
            else
            {
                if (!SqlRuleHelper.TryBuildParameterizedSql(rule.SqlTemplate, context, out string sql, out Dictionary<string, object> parameters, out string placeholderError))
                {
                    result.Passed = false;
                    result.Message = placeholderError;
                    return result;
                }

                DataTable dataTable;
                try
                {
                    dataTable = await _business.ConsultDB(sql, connectionString, parameters);
                }
                catch (Exception ex)
                {
                    result.Passed = false;
                    result.Message = $"Erro ao consultar o banco: {ex.Message}";
                    return result;
                }

                if (dataTable.Rows.Count == 0)
                {
                    result.Passed = false;
                    result.Message = "Nenhum registro encontrado no banco.";
                    return result;
                }

                sourceRow = dataTable.Rows[0];
            }

            if (!sourceRow.Table.Columns.Contains(rule.ResultColumn))
            {
                result.Passed = false;
                string origin = rule.SourceType == ComparisonSourceType.GeneralResult ? "no resultado geral" : "no resultado da query";
                result.Message = $"Coluna '{rule.ResultColumn}' não veio {origin}.";
                return result;
            }

            object actualValue = sourceRow[rule.ResultColumn];
            result.Actual = SqlRuleHelper.ToInvariantString(actualValue);
            result.Passed = SqlRuleHelper.Compare(result.Expected, result.Actual, rule.Comparison);

            return result;
        }

        private static readonly JsonSerializerSettings RuleFileSettings = new JsonSerializerSettings
        {
            Converters = { new StringEnumConverter() }
        };

        // Retrocompatível com arquivos salvos antes da Query Geral existir (uma lista JSON "nua", sem
        // GeneralSqlTemplate) — nesse caso vira um ValidationRuleSet com GeneralSqlTemplate vazio.
        public static ValidationRuleSet LoadRules(string path)
        {
            string json = File.ReadAllText(path);
            JToken root = JToken.Parse(json);

            if (root.Type == JTokenType.Array)
            {
                List<ValidationRule> legacyRules = root.ToObject<List<ValidationRule>>(JsonSerializer.Create(RuleFileSettings)) ?? new List<ValidationRule>();
                return new ValidationRuleSet { Rules = legacyRules };
            }

            return root.ToObject<ValidationRuleSet>(JsonSerializer.Create(RuleFileSettings)) ?? new ValidationRuleSet();
        }

        public static void SaveRules(string path, ValidationRuleSet ruleSet)
        {
            string json = JsonConvert.SerializeObject(ruleSet, Formatting.Indented, RuleFileSettings);
            File.WriteAllText(path, json);
        }
    }
}
