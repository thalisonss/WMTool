namespace WMTool.Validation.Models
{
    // Comparação linha-a-linha (N itens): cada lado (origem/destino) é um conjunto de linhas — casadas
    // pela coluna-chave (ex.: nItem, cIDProduct) — e o valor de cada linha casada é comparado. Gera um
    // ValidationRuleResult por chave encontrada (em qualquer um dos dois lados), não um resultado só.
    public class DbRowSetComparisonRule
    {
        public string Name { get; set; }

        public ComparisonSourceType OriginSourceType { get; set; } = ComparisonSourceType.CustomSql;
        public string OriginSqlTemplate { get; set; }
        public string OriginKeyColumn { get; set; }
        public string OriginValueColumn { get; set; }

        public ComparisonSourceType DestinationSourceType { get; set; } = ComparisonSourceType.CustomSql;
        public string DestinationSqlTemplate { get; set; }
        public string DestinationKeyColumn { get; set; }
        public string DestinationValueColumn { get; set; }

        public ComparisonType Comparison { get; set; } = ComparisonType.EqualsTrimmed;
    }
}
