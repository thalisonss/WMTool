namespace WMTool.InsertScript.Models
{
    // De onde vem o valor de uma coluna do INSERT gerado: Literal é uma expressão SQL crua (ex.:
    // NULL, GETDATE(), 1), JsonPath lê direto do JSON de entrada, GeneralResult lê uma coluna da
    // Query Geral daquela tabela (1 SELECT, várias colunas) e CustomSql roda uma query dedicada só
    // pra essa coluna — mesmo espírito do ComparisonSourceType (Validation) e do
    // ParameterDiscoverySourceType (Reprocessing), com o acréscimo de Literal e JsonPath porque aqui
    // o valor é o próprio dado a inserir, não uma comparação.
    public enum ColumnValueSourceType
    {
        Literal,
        JsonPath,
        GeneralResult,
        CustomSql
    }
}
