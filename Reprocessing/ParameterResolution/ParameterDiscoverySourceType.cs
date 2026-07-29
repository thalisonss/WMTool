namespace WMTool.Reprocessing.ParameterResolution
{
    // Mesmo espírito do ComparisonSourceType (Validation.Models): GeneralResult usa a "Query Geral" (uma
    // única query, rodada uma vez, com uma coluna por parâmetro); CustomSql é uma query dedicada só pra
    // esse parâmetro, pros casos que a Query Geral não cobre.
    public enum ParameterDiscoverySourceType
    {
        CustomSql,
        GeneralResult
    }
}
