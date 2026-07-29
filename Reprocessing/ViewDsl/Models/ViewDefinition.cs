using System.Collections.Generic;

namespace WMTool.Reprocessing.ViewDsl.Models
{
    public class ViewDefinition
    {
        public List<ViewParameterDefinition> Parameters { get; set; } = new List<ViewParameterDefinition>();
        public ViewFromDefinition From { get; set; }
        public ViewFilterDefinition Filter { get; set; }
        public string WhereExpression { get; set; }
        public ViewSelectDefinition Select { get; set; }
    }
}
