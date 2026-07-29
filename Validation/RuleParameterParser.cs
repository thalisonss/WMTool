using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace WMTool.Validation
{
    public static class RuleParameterParser
    {
        private static readonly Regex PlaceholderPattern = new Regex(@"\{(\w+)\}", RegexOptions.Compiled);

        public static string ToParameterizedSql(string sqlTemplate)
        {
            return PlaceholderPattern.Replace(sqlTemplate, "@$1");
        }

        public static List<string> ExtractPlaceholderNames(string sqlTemplate)
        {
            return PlaceholderPattern.Matches(sqlTemplate)
                .Cast<Match>()
                .Select(m => m.Groups[1].Value)
                .Distinct()
                .ToList();
        }
    }
}
