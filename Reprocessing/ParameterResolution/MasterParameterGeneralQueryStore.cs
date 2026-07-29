using System;
using System.IO;
using Newtonsoft.Json;

namespace WMTool.Reprocessing.ParameterResolution
{
    // A "Query Geral" da descoberta de parâmetros: uma única query, rodada uma vez por reprocessamento,
    // com uma coluna por parâmetro (ex.: varcIDLE, varxSector, varcIDLI...). Mesmo papel do
    // GeneralSqlTemplate do ValidationEngine / GeneralOriginSqlTemplate do DatabaseComparisonEngine, só
    // que persistida direto em disco (não faz parte de um arquivo de regras portátil) — segue o mesmo
    // padrão dos demais overrides do reprocessador (CustomViewSqlOverrideStore etc.).
    public class MasterParameterGeneralQueryStore
    {
        private static readonly string FilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "master_parameter_general_query.json");

        private class Payload
        {
            public string Sql { get; set; }
        }

        public string Load()
        {
            if (!File.Exists(FilePath))
            {
                return string.Empty;
            }

            string json = File.ReadAllText(FilePath);
            Payload payload = JsonConvert.DeserializeObject<Payload>(json);
            return payload?.Sql ?? string.Empty;
        }

        public void Save(string sql)
        {
            File.WriteAllText(FilePath, JsonConvert.SerializeObject(new Payload { Sql = sql }, Formatting.Indented));
        }
    }
}
