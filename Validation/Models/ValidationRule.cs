using System.Collections.Generic;

namespace WMTool.Validation.Models
{
    public class ValidationRule
    {
        public string Name { get; set; }
        public string JsonPath { get; set; }
        public string SqlTemplate { get; set; }
        public string ResultColumn { get; set; }
        public ComparisonType Comparison { get; set; } = ComparisonType.EqualsTrimmed;
        public List<RuleParameter> Parameters { get; set; } = new List<RuleParameter>();
    }
}
