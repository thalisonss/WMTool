using System.Collections.Generic;

namespace WMTool.Validation.Models
{
    // Um conjunto de regras salvo/carregado como um todo: a Query Geral (rodada uma vez, compartilhada
    // por todas as regras com SourceType=GeneralResult) + a lista de regras em si.
    public class ValidationRuleSet
    {
        public string GeneralSqlTemplate { get; set; } = string.Empty;
        public List<ValidationRule> Rules { get; set; } = new List<ValidationRule>();
    }
}
