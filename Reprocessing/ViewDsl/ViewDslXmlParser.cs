using System;
using System.Linq;
using System.Xml.Linq;
using WMTool.Reprocessing.ViewDsl.Models;

namespace WMTool.Reprocessing.ViewDsl
{
    public class ViewDslXmlParser
    {
        private static readonly XNamespace Ns = "http://schemas.mc1.com.br/common/view/2015-04/view.xsd";

        public ViewDefinition Parse(string xmlContent)
        {
            XElement root = XDocument.Parse(xmlContent).Root;

            return new ViewDefinition
            {
                Parameters = ParseParameters(root),
                From = ParseFrom(root),
                Filter = ParseFilter(root),
                WhereExpression = root.Element(Ns + "where")?.Value,
                Select = ParseSelect(root)
            };
        }

        private static System.Collections.Generic.List<ViewParameterDefinition> ParseParameters(XElement root)
        {
            return root.Element(Ns + "parameters")?.Elements(Ns + "parameter")
                .Select(p => new ViewParameterDefinition
                {
                    Name = p.Attribute("name")?.Value,
                    Type = p.Attribute("type")?.Value,
                    Optional = IsTrue(p.Attribute("optional"))
                })
                .ToList() ?? new System.Collections.Generic.List<ViewParameterDefinition>();
        }

        private static ViewFromDefinition ParseFrom(XElement root)
        {
            XElement fromElement = root.Element(Ns + "from");

            return new ViewFromDefinition
            {
                Entity = fromElement?.Attribute("entity")?.Value,
                Alias = fromElement?.Attribute("alias")?.Value,
                Joins = fromElement?.Elements()
                    .Where(e => e.Name.LocalName == "left-join" || e.Name.LocalName == "inner-join")
                    .Select(e => new ViewJoinDefinition
                    {
                        JoinType = e.Name.LocalName,
                        Entity = e.Attribute("entity")?.Value,
                        Alias = e.Attribute("alias")?.Value,
                        OnExpression = e.Attribute("on")?.Value,
                        ParameterMappings = e.Elements(Ns + "parameter")
                            .Select(p => new ViewParameterMapping
                            {
                                Name = p.Attribute("name")?.Value,
                                Value = p.Attribute("value")?.Value
                            })
                            .ToList()
                    })
                    .ToList() ?? new System.Collections.Generic.List<ViewJoinDefinition>()
            };
        }

        private static ViewFilterDefinition ParseFilter(XElement root)
        {
            XElement filterElement = root.Element(Ns + "filter");

            return new ViewFilterDefinition
            {
                CompanyFilter = IsTrue(filterElement?.Attribute("company")),
                EnabledEntities = filterElement?.Elements(Ns + "enabled")
                    .Select(e => e.Attribute("entity")?.Value)
                    .Where(e => !string.IsNullOrEmpty(e))
                    .ToList() ?? new System.Collections.Generic.List<string>()
            };
        }

        private static ViewSelectDefinition ParseSelect(XElement root)
        {
            XElement selectElement = root.Element(Ns + "select");

            int? limit = int.TryParse(selectElement?.Attribute("limit")?.Value, out int parsedLimit)
                ? parsedLimit
                : (int?)null;

            return new ViewSelectDefinition
            {
                Distinct = IsTrue(selectElement?.Attribute("distinct")),
                Limit = limit,
                Columns = selectElement?.Elements(Ns + "column")
                    .Select(c => new ViewColumnDefinition
                    {
                        Expression = c.Attribute("expression")?.Value,
                        OutputAlias = c.Attribute("alias")?.Value
                    })
                    .ToList() ?? new System.Collections.Generic.List<ViewColumnDefinition>()
            };
        }

        private static bool IsTrue(XAttribute attribute)
        {
            return string.Equals(attribute?.Value, "true", StringComparison.OrdinalIgnoreCase);
        }
    }
}
