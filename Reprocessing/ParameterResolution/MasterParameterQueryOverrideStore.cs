using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace WMTool.Reprocessing.ParameterResolution
{
    // Igual ao CustomViewSqlOverrideStore (Reprocessing/CustomSql), mas para parâmetros mestre: permite
    // cadastrar, por nome de parâmetro (ex.: "varcIDLE"), uma forma de descobrir o valor real em vez de
    // cair no default estático (ex.: "LE = Customer") — via a Query Geral (MasterParameterGeneralQueryStore)
    // ou uma query dedicada. Guardada num arquivo JSON simples ao lado do executável, mesmo padrão dos
    // outros overrides do reprocessador.
    public class MasterParameterQueryOverrideStore
    {
        private static readonly string FilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "master_parameter_query_overrides.json");

        public IReadOnlyDictionary<string, MasterParameterQueryOverride> LoadAll()
        {
            if (!File.Exists(FilePath))
            {
                return new Dictionary<string, MasterParameterQueryOverride>(StringComparer.OrdinalIgnoreCase);
            }

            string json = File.ReadAllText(FilePath);
            List<MasterParameterQueryOverride> list = JsonConvert.DeserializeObject<List<MasterParameterQueryOverride>>(json) ?? new List<MasterParameterQueryOverride>();

            return list.ToDictionary(x => x.ParameterName, x => x, StringComparer.OrdinalIgnoreCase);
        }

        public MasterParameterQueryOverride Get(string parameterName)
        {
            return LoadAll().TryGetValue(parameterName, out MasterParameterQueryOverride value) ? value : null;
        }

        public void SaveCustomSql(string parameterName, string sql)
        {
            Save(new MasterParameterQueryOverride
            {
                ParameterName = parameterName,
                SourceType = ParameterDiscoverySourceType.CustomSql,
                Sql = sql
            });
        }

        public void SaveGeneralResult(string parameterName, string resultColumn)
        {
            Save(new MasterParameterQueryOverride
            {
                ParameterName = parameterName,
                SourceType = ParameterDiscoverySourceType.GeneralResult,
                ResultColumn = resultColumn
            });
        }

        private void Save(MasterParameterQueryOverride entry)
        {
            Dictionary<string, MasterParameterQueryOverride> all = LoadAll().ToDictionary(kv => kv.Key, kv => kv.Value, StringComparer.OrdinalIgnoreCase);
            all[entry.ParameterName] = entry;
            Persist(all);
        }

        public void Remove(string parameterName)
        {
            Dictionary<string, MasterParameterQueryOverride> all = LoadAll().ToDictionary(kv => kv.Key, kv => kv.Value, StringComparer.OrdinalIgnoreCase);
            all.Remove(parameterName);
            Persist(all);
        }

        private static void Persist(IReadOnlyDictionary<string, MasterParameterQueryOverride> all)
        {
            File.WriteAllText(FilePath, JsonConvert.SerializeObject(all.Values.ToList(), Formatting.Indented));
        }
    }
}
