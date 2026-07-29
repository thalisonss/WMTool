using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using static WMTool.Reprocessing.ViewDsl.SqlTextScanner;

namespace WMTool.Reprocessing.ViewDsl
{
    // A DSL de views permite misturar colunas agregadas (SUM/COUNT/...) com colunas simples no mesmo
    // <select>, sem nenhuma tag de GROUP BY — o motor original deve montar o GROUP BY implicitamente a
    // partir das colunas não agregadas. Esta classe só identifica se uma expressão já traduzida contém
    // uma chamada de agregação no nível da query atual (não dentro de uma subquery "(SELECT ...)" própria,
    // que tem escopo isolado).
    public static class AggregateColumnDetector
    {
        private static readonly HashSet<string> AggregateFunctionNames = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            "SUM", "COUNT", "AVG", "MIN", "MAX"
        };

        public static bool ContainsTopLevelAggregate(string sql)
        {
            int i = 0;

            while (i < sql.Length)
            {
                char c = sql[i];

                if (c == '\'')
                {
                    i = SkipStringLiteral(sql, i);
                    continue;
                }

                if (IsIdentifierStart(c))
                {
                    int wordEnd = ConsumeWord(sql, i);
                    string word = sql.Substring(i, wordEnd - i);
                    int afterSpaces = SkipWhitespace(sql, wordEnd);

                    if (afterSpaces < sql.Length && sql[afterSpaces] == '(')
                    {
                        int closeParen = FindMatchingParen(sql, afterSpaces);
                        string innerContent = sql.Substring(afterSpaces + 1, closeParen - afterSpaces - 1);

                        if (AggregateFunctionNames.Contains(word) && !IsSubquery(innerContent))
                        {
                            return true;
                        }

                        if (!IsSubquery(innerContent) && ContainsTopLevelAggregate(innerContent))
                        {
                            return true;
                        }

                        i = closeParen + 1;
                        continue;
                    }

                    i = wordEnd;
                    continue;
                }

                if (c == '(')
                {
                    int closeParen = FindMatchingParen(sql, i);
                    string innerContent = sql.Substring(i + 1, closeParen - i - 1);

                    if (!IsSubquery(innerContent) && ContainsTopLevelAggregate(innerContent))
                    {
                        return true;
                    }

                    i = closeParen + 1;
                    continue;
                }

                i++;
            }

            return false;
        }

        private static bool IsSubquery(string parenContent)
        {
            return Regex.IsMatch(parenContent.TrimStart(), @"^SELECT\b", RegexOptions.IgnoreCase);
        }
    }
}
