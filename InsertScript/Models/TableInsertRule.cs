using System.Collections.Generic;

namespace WMTool.InsertScript.Models
{
    public class TableInsertRule
    {
        public string TableName { get; set; }
        public bool Enabled { get; set; } = true;
        public RowSourceType RowSourceType { get; set; } = RowSourceType.SingleRow;

        // Usado quando RowSourceType == JsonArray: caminho do array no JSON raiz (ex.:
        // "$.nfe_batch.nfe_batch.Nota[0].produtos"). Cada elemento vira uma linha/INSERT.
        public string ArrayJsonPath { get; set; }

        // Query Geral desta tabela: roda 1x por linha (item), alimentando toda coluna com
        // SourceType == GeneralResult. Opcional — só precisa existir se alguma coluna usar.
        public string GeneralSqlTemplate { get; set; }

        public List<ColumnRule> Columns { get; set; } = new List<ColumnRule>();
    }
}
