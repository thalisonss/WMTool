using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using WMTool.InsertScript.Models;

namespace WMTool.InsertScript
{
    // Save/Load do InsertScriptRuleSet num arquivo JSON portátil (escolhido pelo usuário via
    // SaveFileDialog/OpenFileDialog) — mesmo papel do ValidationEngine.SaveRules/LoadRules
    // (Validation/ValidationEngine.cs), com o StringEnumConverter pra gravar os enums (SourceType
    // etc.) por nome em vez de número, deixando o arquivo legível/editável à mão.
    public static class InsertScriptRuleSetStore
    {
        private static readonly JsonSerializerSettings Settings = new JsonSerializerSettings
        {
            Converters = { new StringEnumConverter() }
        };

        public static InsertScriptRuleSet LoadRules(string path)
        {
            string json = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<InsertScriptRuleSet>(json, Settings) ?? new InsertScriptRuleSet();
        }

        public static void SaveRules(string path, InsertScriptRuleSet ruleSet)
        {
            string json = JsonConvert.SerializeObject(ruleSet, Formatting.Indented, Settings);
            File.WriteAllText(path, json);
        }
    }
}
