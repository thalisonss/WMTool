using System.Text.RegularExpressions;

namespace WMTool.Reprocessing.Templating
{
    internal static class PlaceholderRegex
    {
        public static readonly Regex Pattern = new Regex(
            @"\{\s*(?<alias>[A-Za-z0-9_]+)\.(?<field>[A-Za-z0-9_]+)\s*\}",
            RegexOptions.Compiled);
    }
}
