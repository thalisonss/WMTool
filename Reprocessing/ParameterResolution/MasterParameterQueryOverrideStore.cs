using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace WMTool.Reprocessing.ParameterResolution
{
    // Igual ao CustomViewSqlOverrideStore (Reprocessing/CustomSql), mas para parâmetros mestre: permite
    // cadastrar, por nome de parâmetro (ex.: "varcIDLE"), uma query SQL que descobre o valor real em vez
    // de cair no default estático (ex.: "LE = Customer"). A query usa os mesmos binds ":varIDInvoice"/
    // ":varcSerie"/":varcIDBranchInvoice"/":cIDCompany" das views, resolvidos com os 4 inputs já digitados
    // na tela — funciona pra qualquer nota, não só a que foi usada pra descobrir a query.
    public class MasterParameterQueryOverrideStore
    {
        private static readonly string FilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "master_parameter_query_overrides.json");

        public IReadOnlyDictionary<string, string> LoadAll()
        {
            if (!File.Exists(FilePath))
            {
                return new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            }

            string json = File.ReadAllText(FilePath);
            List<MasterParameterQueryOverride> list = JsonConvert.DeserializeObject<List<MasterParameterQueryOverride>>(json) ?? new List<MasterParameterQueryOverride>();

            return list.ToDictionary(x => x.ParameterName, x => x.Sql, StringComparer.OrdinalIgnoreCase);
        }

        public string GetSql(string parameterName)
        {
            return LoadAll().TryGetValue(parameterName, out string sql) ? sql : null;
        }

        public void Save(string parameterName, string sql)
        {
            Dictionary<string, string> all = LoadAll().ToDictionary(kv => kv.Key, kv => kv.Value, StringComparer.OrdinalIgnoreCase);
            all[parameterName] = sql;
            Persist(all);
        }

        public void Remove(string parameterName)
        {
            Dictionary<string, string> all = LoadAll().ToDictionary(kv => kv.Key, kv => kv.Value, StringComparer.OrdinalIgnoreCase);
            all.Remove(parameterName);
            Persist(all);
        }

        private static void Persist(IReadOnlyDictionary<string, string> all)
        {
            List<MasterParameterQueryOverride> list = all
                .Select(kv => new MasterParameterQueryOverride { ParameterName = kv.Key, Sql = kv.Value })
                .ToList();

            File.WriteAllText(FilePath, JsonConvert.SerializeObject(list, Formatting.Indented));
        }
    }
}
