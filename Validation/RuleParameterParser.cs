using System.Text.RegularExpressions;
using Newtonsoft.Json.Linq;
using WMTool.Validation.Models;

namespace WMTool.Validation
{
    public static class RuleParameterParser
    {
        private static readonly Regex PlaceholderPattern = new Regex(@"\{(\w+)\}", RegexOptions.Compiled);

        public static string ToParameterizedSql(string sqlTemplate)
        {
            return PlaceholderPattern.Replace(sqlTemplate, "@$1");
        }

        public static bool TryResolve(RuleParameter parameter, JObject json, out string value, out string error)
        {
            if (parameter.SourceType == RuleParameterSource.FixedValue)
            {
                value = parameter.Value;
                error = null;
                return true;
            }

            JToken token = json.SelectToken(parameter.Value);
            if (token == null)
            {
                value = null;
                error = $"Parâmetro '{parameter.Name}': caminho '{parameter.Value}' não encontrado no JSON.";
                return false;
            }

            value = token.ToString();
            error = null;
            return true;
        }
    }
}
