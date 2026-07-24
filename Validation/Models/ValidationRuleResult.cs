namespace WMTool.Validation.Models
{
    public class ValidationRuleResult
    {
        public string RuleName { get; set; }
        public string Expected { get; set; }
        public string Actual { get; set; }
        public bool Passed { get; set; }
        public string Message { get; set; }
    }
}
