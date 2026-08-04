namespace WMTool.InsertScript.Models
{
    public class ColumnRule
    {
        public string ColumnName { get; set; }
        public ColumnValueSourceType SourceType { get; set; } = ColumnValueSourceType.JsonPath;

        // Usado quando SourceType == JsonPath: caminho lido a partir da linha atual (o JSON raiz
        // pra tabela SingleRow, ou o item do array pra tabela JsonArray).
        public string JsonPath { get; set; }

        // Usado quando SourceType == CustomSql: query dedicada só pra essa coluna, com placeholders
        // {nome} resolvidos pelo pool de parâmetros da linha (globais do rule set + colunas
        // Literal/JsonPath já resolvidas da mesma linha).
        public string SqlTemplate { get; set; }

        // Usado quando SourceType == GeneralResult: nome da coluna a ler da Query Geral da tabela.
        // Vazio/nulo significa "usar o próprio ColumnName como nome da coluna".
        public string ResultColumn { get; set; }

        // Usado quando SourceType == Literal: expressão SQL inserida verbatim (sem aspas/escape),
        // ex.: NULL, GETDATE(), 1, '0'.
        public string LiteralValue { get; set; }
    }
}
