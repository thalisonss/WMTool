using System.Collections.Generic;

namespace WMTool.InsertScript.Models
{
    public class InsertScriptGenerationResult
    {
        public string Sql { get; set; } = string.Empty;
        public List<string> Warnings { get; set; } = new List<string>();
    }
}
