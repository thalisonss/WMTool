using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace WMTool.Reprocessing.ViewDsl
{
    public static class BindParameterScanner
    {
        private static readonly Regex Pattern = new Regex(@"(?<![A-Za-z0-9_])@([A-Za-z0-9_]+)", RegexOptions.Compiled);

        public static IReadOnlyList<string> ExtractNames(string sql)
        {
            return Pattern.Matches(sql)
                .Cast<Match>()
                .Select(m => m.Groups[1].Value)
                .Distinct()
                .ToList();
        }
    }
}
