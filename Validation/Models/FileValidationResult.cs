using System.Collections.Generic;

namespace WMTool.Validation.Models
{
    public class FileValidationResult
    {
        public string FileName { get; set; }
        public string ParseError { get; set; }
        public List<ValidationRuleResult> Results { get; set; } = new List<ValidationRuleResult>();
    }
}
