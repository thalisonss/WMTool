using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
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

        public async Task<List<ValidationRuleResult>> RunAsync(JObject json, IEnumerable<ValidationRule> rules, string connectionString)
        {
            var results = new List<ValidationRuleResult>();

            foreach (ValidationRule rule in rules)
            {
                results.Add(await RunRuleAsync(json, rule, connectionString));
            }

            return results;
        }

        public async Task<List<FileValidationResult>> RunBatchAsync(string folderPath, IEnumerable<ValidationRule> rules, string connectionString)
        {
            var ruleList = rules.ToList();
            var fileResults = new List<FileValidationResult>();

            foreach (string filePath in Directory.GetFiles(folderPath, "*.json"))
            {
                var fileResult = new FileValidationResult { FileName = Path.GetFileName(filePath) };

                try
                {
                    JObject json = JObject.Parse(File.ReadAllText(filePath));
                    fileResult.Results = await RunAsync(json, ruleList, connectionString);
                }
                catch (JsonException ex)
                {
                    fileResult.ParseError = ex.Message;
                }

                fileResults.Add(fileResult);
            }

            return fileResults;
        }

        private async Task<ValidationRuleResult> RunRuleAsync(JObject json, ValidationRule rule, string connectionString)
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

            var parameters = new Dictionary<string, object>();
            foreach (RuleParameter parameter in rule.Parameters)
            {
                if (!RuleParameterParser.TryResolve(parameter, json, out string value, out string error))
                {
                    result.Passed = false;
                    result.Message = error;
                    return result;
                }

                parameters["@" + parameter.Name] = value;
            }

            List<string> undeclared = RuleParameterParser.ExtractPlaceholderNames(rule.SqlTemplate)
                .Where(name => !parameters.ContainsKey("@" + name))
                .ToList();

            if (undeclared.Count > 0)
            {
                result.Passed = false;
                result.Message = $"SQL usa {{{string.Join("}}, {{", undeclared)}}} mas não há parâmetro correspondente na coluna Parâmetros.";
                return result;
            }

            string sql = RuleParameterParser.ToParameterizedSql(rule.SqlTemplate);

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

            if (!dataTable.Columns.Contains(rule.ResultColumn))
            {
                result.Passed = false;
                result.Message = $"Coluna '{rule.ResultColumn}' não veio no resultado da query.";
                return result;
            }

            object actualValue = dataTable.Rows[0][rule.ResultColumn];
            result.Actual = actualValue == DBNull.Value ? null : actualValue.ToString();
            result.Passed = Compare(result.Expected, result.Actual, rule.Comparison);

            return result;
        }

        private static bool Compare(string expected, string actual, ComparisonType comparison)
        {
            if (expected == null || actual == null)
            {
                return expected == actual;
            }

            switch (comparison)
            {
                case ComparisonType.NumericEquals:
                    bool expectedIsNumber = decimal.TryParse(expected, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal expectedNumber);
                    bool actualIsNumber = decimal.TryParse(actual, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal actualNumber);
                    return expectedIsNumber && actualIsNumber && expectedNumber == actualNumber;

                case ComparisonType.Contains:
                    return actual.IndexOf(expected.Trim(), StringComparison.OrdinalIgnoreCase) >= 0;

                case ComparisonType.EqualsTrimmed:
                default:
                    return string.Equals(expected.Trim(), actual.Trim(), StringComparison.OrdinalIgnoreCase);
            }
        }

        private static readonly JsonSerializerSettings RuleFileSettings = new JsonSerializerSettings
        {
            Converters = { new StringEnumConverter() }
        };

        public static List<ValidationRule> LoadRules(string path)
        {
            string json = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<List<ValidationRule>>(json, RuleFileSettings) ?? new List<ValidationRule>();
        }

        public static void SaveRules(string path, IEnumerable<ValidationRule> rules)
        {
            string json = JsonConvert.SerializeObject(rules, Formatting.Indented, RuleFileSettings);
            File.WriteAllText(path, json);
        }
    }
}
