namespace WMTool.Reprocessing.ViewDsl
{
    // Helpers de tokenização de baixo nível compartilhados entre DslExpressionTranslator e
    // AggregateColumnDetector — escaneiam texto SQL/DSL respeitando literais de string ('...') e
    // parênteses balanceados, sem entender a semântica do que está dentro.
    internal static class SqlTextScanner
    {
        public static bool IsIdentifierStart(char c)
        {
            return char.IsLetter(c) || c == '_';
        }

        public static int ConsumeWord(string text, int start)
        {
            int i = start;
            while (i < text.Length && (char.IsLetterOrDigit(text[i]) || text[i] == '_'))
            {
                i++;
            }

            return i;
        }

        public static int SkipWhitespace(string text, int start)
        {
            int i = start;
            while (i < text.Length && char.IsWhiteSpace(text[i]))
            {
                i++;
            }

            return i;
        }

        public static int SkipStringLiteral(string text, int quoteIndex)
        {
            int i = quoteIndex + 1;
            while (i < text.Length)
            {
                if (text[i] == '\'')
                {
                    if (i + 1 < text.Length && text[i + 1] == '\'')
                    {
                        i += 2;
                        continue;
                    }

                    return i + 1;
                }

                i++;
            }

            return i;
        }

        public static int FindMatchingParen(string text, int openParenIndex)
        {
            int depth = 0;
            int i = openParenIndex;

            while (i < text.Length)
            {
                char c = text[i];

                if (c == '\'')
                {
                    i = SkipStringLiteral(text, i);
                    continue;
                }

                if (c == '(')
                {
                    depth++;
                }
                else if (c == ')')
                {
                    depth--;
                    if (depth == 0)
                    {
                        return i;
                    }
                }

                i++;
            }

            throw new System.FormatException($"Parênteses desbalanceados no texto: '{text}'.");
        }
    }
}
