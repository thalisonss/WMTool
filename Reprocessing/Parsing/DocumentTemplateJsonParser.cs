using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json.Linq;
using WMTool.Reprocessing.Models;

namespace WMTool.Reprocessing.Parsing
{
    public static class DocumentTemplateJsonParser
    {
        public static DocumentTemplateDefinition Parse(string templateName, int version, string cContent)
        {
            JObject root = JObject.Parse(cContent);

            List<DataSourceReference> dataSources = (root["DataSources"] as JArray)?
                .Select(d => new DataSourceReference
                {
                    Alias = d["alias"]?.ToString(),
                    ViewName = d["name"]?.ToString()
                })
                .ToList() ?? new List<DataSourceReference>();

            JObject templateRoot = root["Template"] as JObject ?? new JObject();

            return new DocumentTemplateDefinition
            {
                TemplateName = templateName,
                Version = version,
                DataSources = dataSources,
                TemplateRoot = templateRoot
            };
        }
    }
}
