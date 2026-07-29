using System.Collections.Generic;

namespace WMTool.Reprocessing.ViewDsl
{
    public class TranslatedSqlQuery
    {
        public string Sql { get; set; }
        public IReadOnlyList<string> ParameterNames { get; set; }
    }
}
