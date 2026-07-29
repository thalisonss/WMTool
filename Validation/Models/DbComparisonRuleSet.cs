using System.Collections.Generic;

namespace WMTool.Validation.Models
{
    public class DbComparisonRuleSet
    {
        // Comparação de valor único (1 linha de cada lado).
        public string GeneralOriginSqlTemplate { get; set; } = string.Empty;
        public string GeneralDestinationSqlTemplate { get; set; } = string.Empty;
        public List<DbComparisonRule> Rules { get; set; } = new List<DbComparisonRule>();

        // Comparação linha-a-linha (N itens) — Query Geral aqui devolve VÁRIAS linhas (ex.: todos os
        // produtos da nota), reaproveitada por toda regra de RowSetRules com SourceType=GeneralResult.
        public string GeneralOriginRowSetSqlTemplate { get; set; } = string.Empty;
        public string GeneralDestinationRowSetSqlTemplate { get; set; } = string.Empty;
        public List<DbRowSetComparisonRule> RowSetRules { get; set; } = new List<DbRowSetComparisonRule>();

        // Checagens de presença/ausência (não comparam dois lados).
        public List<DbPresenceRule> PresenceRules { get; set; } = new List<DbPresenceRule>();
    }
}
