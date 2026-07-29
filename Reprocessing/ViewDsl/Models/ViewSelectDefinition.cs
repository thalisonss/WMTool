using System.Collections.Generic;

namespace WMTool.Reprocessing.ViewDsl.Models
{
    public class ViewSelectDefinition
    {
        public bool Distinct { get; set; }
        public int? Limit { get; set; }
        public List<ViewColumnDefinition> Columns { get; set; } = new List<ViewColumnDefinition>();
    }
}
