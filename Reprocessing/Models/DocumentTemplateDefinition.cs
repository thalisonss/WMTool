using System.Collections.Generic;
using Newtonsoft.Json.Linq;

namespace WMTool.Reprocessing.Models
{
    public class DocumentTemplateDefinition
    {
        public string TemplateName { get; set; }
        public int Version { get; set; }
        public List<DataSourceReference> DataSources { get; set; } = new List<DataSourceReference>();
        public JObject TemplateRoot { get; set; }
    }
}
