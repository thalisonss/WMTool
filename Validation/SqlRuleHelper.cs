using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using WMTool.Business;
using WMTool.Validation.Models;

namespace WMTool.Validation
{
    // Peças compartilhadas entre ValidationEngine (JSON x Banco) e DatabaseComparisonEngine (Banco x
    // Banco): montagem dos parâmetros {cIDInvoice}/etc., resolução da "Query Geral" (rodada uma única
    // vez, guardando só a primeira linha) e a comparação final entre dois valores de texto.
    public static class SqlRuleHelper
    {
        private const string UndeclaredPlaceholderMessage =
            "mas os únicos valores disponíveis são {cIDInvoice}, {cSerie}, {cIDBranchInvoice}, {cIDCompany} (preenchidos nos campos do topo da tela).";

        public static Dictionary<string, object> BuildParameters(ValidationContextInputs context)
        {
            return new Dictionary<string, object>(StringComparer.OrdinalIgnoreCase)
            {
                ["@cIDInvoice"] = context?.CIDInvoice,
                ["@cSerie"] = context?.CSerie,
                ["@cIDBranchInvoice"] = context?.CIDBranchInvoice,
                ["@cIDCompany"] = context?.CIDCompany
            };
        }

        public static bool TryBuildParameterizedSql(
            string sqlTemplate, ValidationContextInputs context, out string sql, out Dictionary<string, object> parameters, out string error)
        {
            parameters = BuildParameters(context);
            Dictionary<string, object> resolvedParameters = parameters;

            List<string> undeclared = RuleParameterParser.ExtractPlaceholderNames(sqlTemplate)
                .Where(name => !resolvedParameters.ContainsKey("@" + name))
                .ToList();

            if (undeclared.Count > 0)
            {
                sql = null;
                error = $"SQL usa {{{string.Join("}}, {{", undeclared)}}}, {UndeclaredPlaceholderMessage}";
                return false;
            }

            sql = RuleParameterParser.ToParameterizedSql(sqlTemplate);
            error = null;
            return true;
        }

        // Executa um SQL parametrizado ({cIDInvoice}/etc.) e devolve a DataTable completa. Não lança —
        // qualquer problema (placeholder não declarado, erro de banco) volta como Error, pra quem chamar
        // decidir o que fazer (ex.: falhar só as regras que dependem desse resultado).
        public static async Task<(DataTable Table, string Error)> RunQueryAsync(
            WMBusiness business, string sqlTemplate, string connectionString, ValidationContextInputs context, string queryLabel)
        {
            if (string.IsNullOrWhiteSpace(sqlTemplate))
            {
                return (null, $"{queryLabel} não configurada.");
            }

            if (!TryBuildParameterizedSql(sqlTemplate, context, out string sql, out Dictionary<string, object> parameters, out string placeholderError))
            {
                return (null, placeholderError);
            }

            try
            {
                DataTable dataTable = await business.ConsultDB(sql, connectionString, parameters);
                return (dataTable, null);
            }
            catch (Exception ex)
            {
                return (null, $"Erro ao executar {queryLabel}: {ex.Message}");
            }
        }

        // Roda uma "Query Geral" (se configurada) uma única vez e guarda só a primeira linha. Não lança
        // exceção: um problema aqui não deve derrubar regras que não dependem dela; o erro só aparece
        // nas regras que realmente pedem SourceType=GeneralResult pra esse lado.
        public static async Task<(DataRow Row, string Error)> ResolveGeneralRowAsync(
            WMBusiness business, string generalSqlTemplate, string connectionString, ValidationContextInputs context)
        {
            (DataTable dataTable, string error) = await RunQueryAsync(business, generalSqlTemplate, connectionString, context, "Query Geral");

            if (error != null)
            {
                return (null, error);
            }

            if (dataTable.Rows.Count == 0)
            {
                return (null, "Query Geral não retornou nenhum registro.");
            }

            return (dataTable.Rows[0], null);
        }

        // Igual à Query Geral, mas guarda TODAS as linhas — usado pelas regras linha-a-linha (N itens),
        // onde cada rodada de comparação precisa do conjunto inteiro (ex.: todos os produtos da nota),
        // não só a primeira linha.
        public static async Task<(DataTable Table, string Error)> ResolveGeneralTableAsync(
            WMBusiness business, string generalSqlTemplate, string connectionString, ValidationContextInputs context)
        {
            return await RunQueryAsync(business, generalSqlTemplate, connectionString, context, "Query Geral (conjunto)");
        }

        // .ToString() sem cultura usa CultureInfo.CurrentCulture — num ambiente pt-BR, um decimal vira
        // "3,0000" (vírgula). O NumericEquals abaixo faz o parse de volta em InvariantCulture (ponto),
        // então qualquer valor numérico convertido com a cultura atual falhava o parse silenciosamente
        // e a comparação dava "falso" mesmo quando os valores eram iguais. Convertendo sempre em
        // invariant culture aqui na origem, os dois lados ficam consistentes.
        public static string ToInvariantString(object value)
        {
            if (value == null || value == DBNull.Value)
            {
                return null;
            }

            return value is IFormattable formattable
                ? formattable.ToString(null, CultureInfo.InvariantCulture)
                : value.ToString();
        }

        public static bool Compare(string expected, string actual, ComparisonType comparison)
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
    }
}
