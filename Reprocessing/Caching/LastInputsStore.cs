using System;
using System.IO;
using Newtonsoft.Json;

namespace WMTool.Reprocessing.Caching
{
    // Guarda os últimos cIDInvoice/cSerie/cIDBranchInvoice/cIDCompany executados, pra tela abrir com
    // esses campos já preenchidos em vez de vazios (é raro reprocessar uma nota totalmente diferente
    // da última sessão de trabalho).
    public class LastInputsStore
    {
        private static readonly string FilePath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "last_reprocess_inputs.json");

        public LastReprocessInputs Load()
        {
            if (!File.Exists(FilePath))
            {
                return null;
            }

            string json = File.ReadAllText(FilePath);
            return JsonConvert.DeserializeObject<LastReprocessInputs>(json);
        }

        public void Save(LastReprocessInputs inputs)
        {
            File.WriteAllText(FilePath, JsonConvert.SerializeObject(inputs, Formatting.Indented));
        }
    }
}
