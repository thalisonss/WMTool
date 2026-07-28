using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using WMTool.Reprocessing.Exceptions;
using static WMTool.Reprocessing.ViewDsl.SqlTextScanner;

namespace WMTool.Reprocessing.ViewDsl
{
    public class DslExpressionTranslator
    {
        private readonly DslFunctionRegistry _functionRegistry;

        public DslExpressionTranslator(DslFunctionRegistry functionRegistry)
        {
            _functionRegistry = functionRegistry;
        }

        public string Translate(string dslExpression, string viewNameForErrorContext)
        {
            if (string.IsNullOrEmpty(dslExpression))
            {
                return dslExpression;
            }

            var output = new StringBuilder();
            int i = 0;

            while (i < dslExpression.Length)
            {
                char c = dslExpression[i];

                if (c == '\'')
                {
                    int stringEnd = SkipStringLiteral(dslExpression, i);
                    output.Append(dslExpression, i, stringEnd - i);
                    i = stringEnd;
                    continue;
                }

                if (c == ':' && i + 1 < dslExpression.Length && IsIdentifierStart(dslExpression[i + 1]))
                {
                    int nameEnd = ConsumeWord(dslExpression, i + 1);
                    output.Append('@').Append(dslExpression, i + 1, nameEnd - i - 1);
                    i = nameEnd;
                    continue;
                }

                if (c == '|' && i + 1 < dslExpression.Length && dslExpression[i + 1] == '|')
                {
                    output.Append('+');
                    i += 2;
                    continue;
                }

                if (IsIdentifierStart(c))
                {
                    int identifierEnd = ConsumeDottedIdentifier(dslExpression, i);
                    string identifier = dslExpression.Substring(i, identifierEnd - i);

                    int afterSpaces = SkipWhitespace(dslExpression, identifierEnd);

                    if (identifier.Contains(".") && afterSpaces < dslExpression.Length && dslExpression[afterSpaces] == '(')
                    {
                        int closeParenIndex = FindMatchingParen(dslExpression, afterSpaces);
                        string rawArgs = dslExpression.Substring(afterSpaces + 1, closeParenIndex - afterSpaces - 1);
                        string[] translatedArgs = SplitTopLevelArgs(rawArgs)
                            .Select(arg => Translate(arg.Trim(), viewNameForErrorContext))
                            .ToArray();

                        if (!_functionRegistry.TryTranslate(identifier, translatedArgs, out string translatedCall))
                        {
                            throw new UnknownDslFunctionException(identifier, dslExpression, viewNameForErrorContext);
                        }

                        output.Append(translatedCall);
                        i = closeParenIndex + 1;
                        continue;
                    }

                    output.Append(identifier);
                    i = identifierEnd;
                    continue;
                }

                output.Append(c);
                i++;
            }

            return RewriteNumericCoalesce(RewriteInStringList(RewriteEmbeddedLimits(output.ToString())));
        }

        // Vários dos <column expression> somam colunas de outras views com "coalesce(X, 0)" — o padrão
        // usual pra tratar NULL (nenhuma regra de preço bateu) como zero. Mas quando X vem de uma
        // expressão que usa string.replace/CONCAT/etc sobre um valor numérico (ex.: ICMSUFDestValue =
        // REPLACE(subquery_numerica, '-', '')), X fica com tipo VARCHAR. Nesse caso o SQL Server escolhe
        // o tipo do COALESCE por precedência: como int > varchar, ele tenta converter X pra int — e
        // qualquer valor com casas decimais (ex.: "0.00000") derruba a query inteira com "Conversion
        // failed". Isso nunca aparece no teste manual de uma linha só porque X costuma ser NULL nesses
        // casos; em produção, com mais linhas, X eventualmente tem valor e quebra. Reescrevemos só o
        // caso reconhecível "coalesce(X, <literal numérico>)" para "coalesce(TRY_CONVERT(decimal(18,5),
        // X), <literal numérico>)" — não muda nenhum valor (a leitura de X permanece a mesma, só o tipo
        // alvo da conversão implícita passa a ser decimal, que aceita casas decimais), só evita o erro.
        private static string RewriteNumericCoalesce(string sql)
        {
            if (sql.IndexOf("coalesce", StringComparison.OrdinalIgnoreCase) < 0)
            {
                return sql;
            }

            var output = new StringBuilder();
            int i = 0;

            while (i < sql.Length)
            {
                if (sql[i] == '\'')
                {
                    int stringEnd = SkipStringLiteral(sql, i);
                    output.Append(sql, i, stringEnd - i);
                    i = stringEnd;
                    continue;
                }

                if (IsWholeWordMatch(sql, i, "coalesce"))
                {
                    int afterWord = i + "coalesce".Length;
                    int afterSpaces = SkipWhitespace(sql, afterWord);

                    if (afterSpaces < sql.Length && sql[afterSpaces] == '(')
                    {
                        int closeParen = FindMatchingParen(sql, afterSpaces);
                        string rawArgs = sql.Substring(afterSpaces + 1, closeParen - afterSpaces - 1);
                        List<string> args = SplitTopLevelArgs(rawArgs).Select(RewriteNumericCoalesce).ToList();

                        bool isNumericDefaultPattern = args.Count == 2
                            && Regex.IsMatch(args[1].Trim(), @"^-?\d+(\.\d+)?$")
                            && !Regex.IsMatch(args[0].Trim(), @"^-?\d+(\.\d+)?$");

                        output.Append("coalesce(");
                        output.Append(isNumericDefaultPattern
                            ? "TRY_CONVERT(decimal(18,5), " + args[0].Trim() + ")"
                            : args[0].Trim());

                        for (int a = 1; a < args.Count; a++)
                        {
                            output.Append(", ").Append(args[a].Trim());
                        }

                        output.Append(")");
                        i = closeParen + 1;
                        continue;
                    }
                }

                output.Append(sql[i]);
                i++;
            }

            return output.ToString();
        }

        // A DSL às vezes escreve uma lista de valores de "IN"/"NOT IN" como uma única string com itens
        // separados por vírgula (ex.: "NOT IN '40,41,50'"), o que não é sintaxe válida no SQL Server
        // (IN exige uma lista entre parênteses). Reescrevemos para "NOT IN ('40','41','50')".
        private static string RewriteInStringList(string sql)
        {
            return Regex.Replace(sql, @"\bIN\s+'([^']*)'", match =>
            {
                string[] items = match.Groups[1].Value.Split(',');
                string quotedList = string.Join(",", items.Select(v => "'" + v.Trim() + "'"));
                return "IN (" + quotedList + ")";
            }, RegexOptions.IgnoreCase);
        }

        // Subqueries escritas à mão dentro de uma expression (ex.: "(SELECT ... ORDER BY ... LIMIT 1)")
        // também usam a sintaxe "LIMIT n" da DSL, que o SQL Server não entende. Diferente do "limit" do
        // <select> de nível superior (que vira "TOP n" logo depois do SELECT), aqui o "LIMIT n" pode estar
        // dentro de qualquer subquery aninhada, então localizamos o SELECT correspondente por profundidade
        // de parênteses (pilha), não por um simples IndexOf.
        private static string RewriteEmbeddedLimits(string sql)
        {
            if (sql.IndexOf("LIMIT", StringComparison.OrdinalIgnoreCase) < 0)
            {
                return sql;
            }

            var selectStack = new Stack<(int Position, int Depth)>();
            var edits = new List<(int Start, int Length, string Replacement)>();
            int depth = 0;
            int i = 0;

            while (i < sql.Length)
            {
                char c = sql[i];

                if (c == '\'')
                {
                    i = SkipStringLiteral(sql, i);
                    continue;
                }

                if (c == '(')
                {
                    depth++;
                    i++;
                    continue;
                }

                if (c == ')')
                {
                    while (selectStack.Count > 0 && selectStack.Peek().Depth > depth - 1)
                    {
                        selectStack.Pop();
                    }

                    depth--;
                    i++;
                    continue;
                }

                if (IsWholeWordMatch(sql, i, "SELECT"))
                {
                    selectStack.Push((i, depth));
                    i += "SELECT".Length;
                    continue;
                }

                if (IsWholeWordMatch(sql, i, "LIMIT"))
                {
                    int afterLimit = SkipWhitespace(sql, i + "LIMIT".Length);
                    int numberEnd = afterLimit;
                    while (numberEnd < sql.Length && char.IsDigit(sql[numberEnd]))
                    {
                        numberEnd++;
                    }

                    if (numberEnd > afterLimit && selectStack.Count > 0 && selectStack.Peek().Depth == depth)
                    {
                        (int selectPosition, _) = selectStack.Pop();
                        string count = sql.Substring(afterLimit, numberEnd - afterLimit);
                        edits.Add((selectPosition + "SELECT".Length, 0, " TOP " + count));
                        edits.Add((i, numberEnd - i, string.Empty));
                        i = numberEnd;
                        continue;
                    }
                }

                i++;
            }

            if (edits.Count == 0)
            {
                return sql;
            }

            foreach ((int start, int length, string replacement) in edits.OrderByDescending(e => e.Start))
            {
                sql = sql.Remove(start, length).Insert(start, replacement);
            }

            return Regex.Replace(sql, @"[ \t]{2,}", " ");
        }

        private static bool IsWholeWordMatch(string s, int pos, string word)
        {
            if (pos + word.Length > s.Length || string.Compare(s, pos, word, 0, word.Length, StringComparison.OrdinalIgnoreCase) != 0)
            {
                return false;
            }

            bool leftBoundaryOk = pos == 0 || !IsWordChar(s[pos - 1]);
            int rightPos = pos + word.Length;
            bool rightBoundaryOk = rightPos >= s.Length || !IsWordChar(s[rightPos]);

            return leftBoundaryOk && rightBoundaryOk;
        }

        private static bool IsWordChar(char c)
        {
            return char.IsLetterOrDigit(c) || c == '_';
        }

        private static int ConsumeDottedIdentifier(string expr, int start)
        {
            int i = ConsumeWord(expr, start);
            while (i < expr.Length && expr[i] == '.' && i + 1 < expr.Length && IsIdentifierStart(expr[i + 1]))
            {
                i = ConsumeWord(expr, i + 1);
            }

            return i;
        }

        private static List<string> SplitTopLevelArgs(string rawArgs)
        {
            var result = new List<string>();

            if (string.IsNullOrWhiteSpace(rawArgs))
            {
                return result;
            }

            int depth = 0;
            int start = 0;
            int i = 0;

            while (i < rawArgs.Length)
            {
                char c = rawArgs[i];

                if (c == '\'')
                {
                    i = SkipStringLiteral(rawArgs, i);
                    continue;
                }

                if (c == '(')
                {
                    depth++;
                }
                else if (c == ')')
                {
                    depth--;
                }
                else if (c == ',' && depth == 0)
                {
                    result.Add(rawArgs.Substring(start, i - start));
                    start = i + 1;
                }

                i++;
            }

            result.Add(rawArgs.Substring(start));
            return result;
        }
    }
}
