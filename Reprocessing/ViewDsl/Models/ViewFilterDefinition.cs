using System.Collections.Generic;

namespace WMTool.Reprocessing.ViewDsl.Models
{
    public class ViewFilterDefinition
    {
        public bool CompanyFilter { get; set; }
        public List<string> EnabledEntities { get; set; } = new List<string>();
    }
}
