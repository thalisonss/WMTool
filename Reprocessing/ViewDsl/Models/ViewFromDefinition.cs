using System.Collections.Generic;

namespace WMTool.Reprocessing.ViewDsl.Models
{
    public class ViewFromDefinition
    {
        public string Entity { get; set; }
        public string Alias { get; set; }
        public List<ViewJoinDefinition> Joins { get; set; } = new List<ViewJoinDefinition>();
    }
}
