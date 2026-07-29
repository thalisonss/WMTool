using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using WMTool.Reprocessing.Models;

namespace WMTool.Reprocessing.Caching
{
    // A lista de data sources (alias -> nome da view) do template WM_Invoice_Generate quase nunca muda.
    // Guardamos um snapshot local pra tela abrir com a grid já populada sem precisar de uma consulta ao
    // banco — só é atualizada quando o usuário pede explicitamente (botão "Atualizar DataSources").
    public class DataSourceCacheStore
    {
        private static readonly string FilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "datasource_cache.json");

        public CachedDataSourceSet Load()
        {
            if (!File.Exists(FilePath))
            {
                return null;
            }

            string json = File.ReadAllText(FilePath);
            return JsonConvert.DeserializeObject<CachedDataSourceSet>(json);
        }

        public void Save(string templateName, IReadOnlyList<DataSourceReference> dataSources)
        {
            var cached = new CachedDataSourceSet
            {
                TemplateName = templateName,
                LastUpdatedUtc = DateTime.UtcNow,
                DataSources = new List<DataSourceReference>(dataSources)
            };

            File.WriteAllText(FilePath, JsonConvert.SerializeObject(cached, Formatting.Indented));
        }
    }
}
