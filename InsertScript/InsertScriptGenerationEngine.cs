using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using WMTool.Business;
using WMTool.InsertScript.Models;

namespace WMTool.InsertScript
{
    // Caminho inverso do JsonReprocessingEngine: em vez de JSON -> JSON (via DSL de views), aqui é
    // JSON -> texto de script SQL de INSERT. Nunca executa o INSERT (WMBusiness.ConsultDB bloqueia
    // a palavra-chave) — o SQL gerado é só texto de saída pro usuário revisar/rodar manualmente.
    // Mesma filosofia "avisa mas não trava" do MasterParameterResolver/ValidationEngine: qualquer
    // coluna/tabela com problema vira warning e o valor cai pra NULL, sem interromper o resto.
    public class InsertScriptGenerationEngine
    {
        private readonly WMBusiness _business;

        public InsertScriptGenerationEngine(WMBusiness business)
        {
            _business = business;
        }

        public async Task<InsertScriptGenerationResult> GenerateAsync(JObject json, InsertScriptRuleSet ruleSet, string connectionString)
        {
            var result = new InsertScriptGenerationResult();
            var globalParameters = (ruleSet.Parameters ?? new List<ParameterValue>())
                .Where(p => !string.IsNullOrWhiteSpace(p.Name))
                .ToDictionary(p => p.Name, p => p.Value ?? string.Empty, StringComparer.OrdinalIgnoreCase);

            var tableBlocks = new List<string>();

            foreach (TableInsertRule table in ruleSet.Tables ?? new List<TableInsertRule>())
            {
                if (!table.Enabled)
                {
                    continue;
                }

                List<JToken> rows = ResolveRows(json, table, result.Warnings);
                if (rows.Count == 0)
                {
                    continue;
                }

                var rowStatements = new List<string>();

                foreach (JToken row in rows)
                {
                    rowStatements.Add(await BuildInsertStatementAsync(table, row, globalParameters, connectionString, result.Warnings));
                }

                tableBlocks.Add($"-- {table.TableName} ({rowStatements.Count} registro(s))\r\n" + string.Join("\r\n", rowStatements));
            }

            result.Sql = tableBlocks.Count == 0
                ? "-- Nenhuma tabela habilitada gerou INSERT."
                : string.Join("\r\n\r\n", tableBlocks);

            return result;
        }

        private static List<JToken> ResolveRows(JObject json, TableInsertRule table, List<string> warnings)
        {
            if (table.RowSourceType == RowSourceType.SingleRow)
            {
                return new List<JToken> { json };
            }

            JToken arrayToken = string.IsNullOrWhiteSpace(table.ArrayJsonPath) ? null : json.SelectToken(table.ArrayJsonPath);

            if (arrayToken == null)
            {
                warnings.Add($"Tabela {table.TableName}: caminho de array '{table.ArrayJsonPath}' não encontrado no JSON — nenhum INSERT gerado.");
                return new List<JToken>();
            }

            if (!(arrayToken is JArray array))
            {
                warnings.Add($"Tabela {table.TableName}: '{table.ArrayJsonPath}' não é um array no JSON — nenhum INSERT gerado.");
                return new List<JToken>();
            }

            if (array.Count == 0)
            {
                warnings.Add($"Tabela {table.TableName}: array '{table.ArrayJsonPath}' está vazio — nenhum INSERT gerado.");
            }

            return array.Cast<JToken>().ToList();
        }

        private async Task<string> BuildInsertStatementAsync(
            TableInsertRule table, JToken row, IDictionary<string, string> globalParameters, string connectionString, List<string> warnings)
        {
            var rowParameters = new Dictionary<string, string>(globalParameters, StringComparer.OrdinalIgnoreCase);
            var resolvedValues = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            List<ColumnRule> columns = table.Columns ?? new List<ColumnRule>();

            // 1ª passada: Literal e JsonPath não dependem de banco — resolvem antes e alimentam o
            // pool de parâmetros da linha, pra que GeneralResult/CustomSql possam referenciá-los
            // via {NomeDaColuna} (essencial pro caso de array: cada item expõe seus próprios campos
            // como parâmetro pras queries de resolução dessa mesma linha).
            foreach (ColumnRule column in columns.Where(c => c.SourceType == ColumnValueSourceType.Literal || c.SourceType == ColumnValueSourceType.JsonPath))
            {
                if (column.SourceType == ColumnValueSourceType.Literal)
                {
                    resolvedValues[column.ColumnName] = string.IsNullOrEmpty(column.LiteralValue) ? "NULL" : column.LiteralValue;
                    continue;
                }

                JToken token = string.IsNullOrWhiteSpace(column.JsonPath) ? null : row.SelectToken(column.JsonPath);
                if (token == null || token.Type == JTokenType.Null)
                {
                    if (token == null)
                    {
                        warnings.Add($"Tabela {table.TableName}, coluna {column.ColumnName}: caminho JSON '{column.JsonPath}' não encontrado — usando NULL.");
                    }

                    resolvedValues[column.ColumnName] = "NULL";
                    continue;
                }

                string rawValue = token.ToString();
                rowParameters[column.ColumnName] = rawValue;
                resolvedValues[column.ColumnName] = QuoteSqlLiteral(rawValue);
            }

            // 2ª passada: GeneralResult e CustomSql, já com o pool enriquecido pela 1ª passada.
            DataRow generalRow = null;
            bool generalRowResolved = false;

            foreach (ColumnRule column in columns.Where(c => c.SourceType == ColumnValueSourceType.GeneralResult || c.SourceType == ColumnValueSourceType.CustomSql))
            {
                if (column.SourceType == ColumnValueSourceType.GeneralResult)
                {
                    if (!generalRowResolved)
                    {
                        (DataRow resolvedRow, string generalError) = await GenericSqlParameterResolver.RunRowAsync(_business, table.GeneralSqlTemplate, connectionString, rowParameters);
                        generalRow = resolvedRow;
                        generalRowResolved = true;

                        if (generalRow == null)
                        {
                            warnings.Add($"Tabela {table.TableName}: Query Geral — {generalError}");
                        }
                    }

                    string resultColumn = string.IsNullOrWhiteSpace(column.ResultColumn) ? column.ColumnName : column.ResultColumn;

                    if (generalRow == null || !generalRow.Table.Columns.Contains(resultColumn))
                    {
                        if (generalRow != null)
                        {
                            warnings.Add($"Tabela {table.TableName}, coluna {column.ColumnName}: coluna '{resultColumn}' não veio na Query Geral — usando NULL.");
                        }

                        resolvedValues[column.ColumnName] = "NULL";
                        continue;
                    }

                    object value = generalRow[resultColumn];
                    string strValue = value == DBNull.Value ? null : Convert.ToString(value, CultureInfo.InvariantCulture);
                    rowParameters[column.ColumnName] = strValue ?? string.Empty;
                    resolvedValues[column.ColumnName] = QuoteSqlLiteral(strValue);
                    continue;
                }

                (string customValue, string customError) = await GenericSqlParameterResolver.RunScalarAsync(_business, column.SqlTemplate, connectionString, rowParameters);
                if (customError != null)
                {
                    warnings.Add($"Tabela {table.TableName}, coluna {column.ColumnName}: {customError}");
                    resolvedValues[column.ColumnName] = "NULL";
                    continue;
                }

                rowParameters[column.ColumnName] = customValue ?? string.Empty;
                resolvedValues[column.ColumnName] = QuoteSqlLiteral(customValue);
            }

            string columnList = string.Join(", ", columns.Select(c => c.ColumnName));
            string valueList = string.Join(", ", columns.Select(c => resolvedValues.TryGetValue(c.ColumnName, out string v) ? v : "NULL"));

            return $"INSERT INTO {table.TableName} ({columnList}) VALUES ({valueList});";
        }

        private static string QuoteSqlLiteral(string value)
        {
            return value == null ? "NULL" : "'" + value.Replace("'", "''") + "'";
        }
    }
}
