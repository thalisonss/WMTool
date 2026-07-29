using System.Data;
using System.Collections.Generic;
using Newtonsoft.Json.Linq;
using WMTool.Reprocessing.Models;

namespace WMTool.Reprocessing.Templating
{
    public class TemplateEngine
    {
        public JObject Expand(JObject templateRoot, IReadOnlyDictionary<string, DataSourceResult> dataSources)
        {
            var resolver = new PlaceholderValueResolver(dataSources);
            return (JObject)ExpandToken(templateRoot, resolver, dataSources);
        }

        private JToken ExpandToken(JToken token, PlaceholderValueResolver resolver, IReadOnlyDictionary<string, DataSourceResult> dataSources)
        {
            switch (token.Type)
            {
                case JTokenType.Object:
                    return ExpandObject((JObject)token, resolver, dataSources);

                case JTokenType.Array:
                    var array = new JArray();
                    foreach (JToken item in (JArray)token)
                    {
                        array.Add(ExpandToken(item, resolver, dataSources));
                    }

                    return array;

                case JTokenType.String:
                    return ExpandString((string)token, resolver);

                default:
                    return token.DeepClone();
            }
        }

        private JObject ExpandObject(JObject source, PlaceholderValueResolver resolver, IReadOnlyDictionary<string, DataSourceResult> dataSources)
        {
            var output = new JObject();

            foreach (JProperty property in source.Properties())
            {
                if (property.Name.StartsWith("$") && property.Name.EndsWith("_source"))
                {
                    continue;
                }

                JProperty sourceMarker = source.Property("$" + property.Name + "_source");

                if (sourceMarker != null && property.Value.Type == JTokenType.Array)
                {
                    output[property.Name] = ExpandRepeatingArray((JArray)property.Value, sourceMarker.Value.ToString(), resolver, dataSources);
                    continue;
                }

                output[property.Name] = ExpandToken(property.Value, resolver, dataSources);
            }

            return output;
        }

        private JArray ExpandRepeatingArray(JArray itemTemplateArray, string alias, PlaceholderValueResolver resolver, IReadOnlyDictionary<string, DataSourceResult> dataSources)
        {
            var result = new JArray();

            if (itemTemplateArray.Count == 0 || !dataSources.TryGetValue(alias, out DataSourceResult dataSource))
            {
                return result;
            }

            JToken itemTemplate = itemTemplateArray[0];

            foreach (DataRow row in dataSource.Rows.Rows)
            {
                PlaceholderValueResolver rowResolver = resolver.WithRowOverride(alias, row);
                result.Add(ExpandToken(itemTemplate, rowResolver, dataSources));
            }

            return result;
        }

        private static string ExpandString(string template, PlaceholderValueResolver resolver)
        {
            return PlaceholderRegex.Pattern.Replace(template, match =>
                resolver.Resolve(match.Groups["alias"].Value, match.Groups["field"].Value));
        }
    }
}
