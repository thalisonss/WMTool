using System.Collections.Generic;

namespace WMTool.InsertScript.Models
{
    // Um conjunto de regras salvo/carregado como um todo: os parâmetros globais "Localizar/
    // Substituir" + a lista de tabelas a inserir. Mesmo papel do ValidationRuleSet
    // (Validation/Models), só que com uma lista de parâmetros no lugar de uma Query Geral única.
    public class InsertScriptRuleSet
    {
        public List<ParameterValue> Parameters { get; set; } = new List<ParameterValue>();
        public List<TableInsertRule> Tables { get; set; } = new List<TableInsertRule>();
    }
}
