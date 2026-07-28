namespace WMTool.Reprocessing.ParameterResolution
{
    public class MasterParameterQueryOverride
    {
        public string ParameterName { get; set; }
        public ParameterDiscoverySourceType SourceType { get; set; } = ParameterDiscoverySourceType.CustomSql;

        // Usado quando SourceType == CustomSql: query dedicada só pra esse parâmetro.
        public string Sql { get; set; }

        // Usado quando SourceType == GeneralResult: nome da coluna a ler da Query Geral. Vazio/nulo
        // significa "usar o próprio nome do parâmetro como nome da coluna".
        public string ResultColumn { get; set; }
    }
}
