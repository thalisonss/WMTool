using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Newtonsoft.Json;

namespace WMTool.Reprocessing.CustomSql
{
    // Permite substituir, para uma view específica (ex.: uma que fica lenta demais quando traduzida da
    // DSL), uma query SQL escrita à mão que deve devolver exatamente as mesmas colunas de saída. Guardada
    // num arquivo JSON simples ao lado do executável — é um cadastro raro/manual, não precisa de banco.
    public class CustomViewSqlOverrideStore
    {
        private static readonly string FilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "custom_view_sql_overrides.json");

        public IReadOnlyDictionary<string, string> LoadAll()
        {
            if (!File.Exists(FilePath))
            {
                return new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
            }

            string json = File.ReadAllText(FilePath);
            List<CustomViewSqlOverride> list = JsonConvert.DeserializeObject<List<CustomViewSqlOverride>>(json) ?? new List<CustomViewSqlOverride>();

            return list.ToDictionary(x => x.ViewName, x => x.Sql, StringComparer.OrdinalIgnoreCase);
        }

        public string GetSql(string viewName)
        {
            return LoadAll().TryGetValue(viewName, out string sql) ? sql : null;
        }

        public void Save(string viewName, string sql)
        {
            Dictionary<string, string> all = LoadAll().ToDictionary(kv => kv.Key, kv => kv.Value, StringComparer.OrdinalIgnoreCase);
            all[viewName] = sql;
            Persist(all);
        }

        public void Remove(string viewName)
        {
            Dictionary<string, string> all = LoadAll().ToDictionary(kv => kv.Key, kv => kv.Value, StringComparer.OrdinalIgnoreCase);
            all.Remove(viewName);
            Persist(all);
        }

        private static void Persist(IReadOnlyDictionary<string, string> all)
        {
            List<CustomViewSqlOverride> list = all
                .Select(kv => new CustomViewSqlOverride { ViewName = kv.Key, Sql = kv.Value })
                .ToList();

            File.WriteAllText(FilePath, JsonConvert.SerializeObject(list, Formatting.Indented));
        }
    }
}
