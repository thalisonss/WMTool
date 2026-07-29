namespace WMTool.Validation.Models
{
    // Uma regra de comparação banco-a-banco: os dois lados (origem/destino) são sempre SQL, cada um
    // podendo vir da Query Geral daquele lado (SourceType=GeneralResult) ou de um SQL só seu
    // (SourceType=CustomSql, igual à regra de ValidationRule).
    public class DbComparisonRule
    {
        public string Name { get; set; }

        public ComparisonSourceType OriginSourceType { get; set; } = ComparisonSourceType.CustomSql;
        public string OriginSqlTemplate { get; set; }
        public string OriginResultColumn { get; set; }

        public ComparisonSourceType DestinationSourceType { get; set; } = ComparisonSourceType.CustomSql;
        public string DestinationSqlTemplate { get; set; }
        public string DestinationResultColumn { get; set; }

        public ComparisonType Comparison { get; set; } = ComparisonType.EqualsTrimmed;
    }
}
