using System;
using System.Collections.Generic;
using System.Linq;

namespace WMTool.Reprocessing.ViewDsl
{
    public class DslFunctionRegistry
    {
        private readonly Dictionary<string, Func<string[], string>> _translators =
            new Dictionary<string, Func<string[], string>>(StringComparer.OrdinalIgnoreCase);

        public DslFunctionRegistry()
        {
            RegisterDefaults();
        }

        public void Register(string functionName, Func<string[], string> translator)
        {
            _translators[functionName] = translator;
        }

        public bool TryTranslate(string functionName, string[] translatedArgs, out string sql)
        {
            if (!_translators.TryGetValue(functionName, out Func<string[], string> translator))
            {
                sql = null;
                return false;
            }

            sql = translator(translatedArgs);
            return true;
        }

        private void RegisterDefaults()
        {
            Register("date.toString", args => $"CONVERT(varchar(23), {args[0]}, 121)");
            Register("date.truncate", args => $"CONVERT(date, {args[0]})");
            Register("number.toString", args => $"CONVERT(varchar(50), {args[0]})");
            Register("string.concat", args => $"CONCAT({string.Join(", ", args)})");
            Register("string.indexOf", args => $"CHARINDEX({args[1]}, {args[0]})");
            Register("string.length", args => $"LEN({args[0]})");
            Register("string.replace", args => $"REPLACE({args[0]}, {args[1]}, {args[2]})");
            Register("string.substring", TranslateSubstring);
            // TRY_CONVERT (não CONVERT): o valor de origem é dado de negócio em texto — pode não ser um
            // inteiro "limpo" (ex.: "0.00000", vindo de um campo numeric(18,5) convertido pra texto em
            // outro ponto da expressão). Um erro de conversão aqui não pode derrubar o reprocessamento
            // inteiro; a semântica correta de "toInteger" é virar NULL quando o valor não converte.
            Register("string.toInteger", args => $"TRY_CONVERT(int, {args[0]})");
            Register("string.trim", args => $"LTRIM(RTRIM({args[0]}))");
            Register("string.trimend", args => $"RTRIM({args[0]})");
            Register("string.trimstart", args => $"LTRIM({args[0]})");
        }

        private static string TranslateSubstring(string[] args)
        {
            string start = $"({args[1]}) + 1";
            string length = args.Length > 2 ? args[2] : "8000";
            return $"SUBSTRING({args[0]}, {start}, {length})";
        }
    }
}
