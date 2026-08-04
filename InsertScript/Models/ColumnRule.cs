namespace WMTool.InsertScript.Models
{
    public class ColumnRule
    {
        public string ColumnName { get; set; }
        public ColumnValueSourceType SourceType { get; set; } = ColumnValueSourceType.JsonPath;

        // Usado quando SourceType == JsonPath: caminho lido a partir da linha atual (o JSON raiz
        // pra tabela SingleRow, ou o item do array pra tabela JsonArray).
        public string JsonPath { get; set; }

        // Usado quando SourceType == Parameter: nome (com ou sem chaves) a buscar direto no pool de
        // parâmetros da linha — globais da grid Localizar/Substituir, RowIndex/RowNumber, ou outra
        // coluna Literal/JsonPath já resolvida da mesma tabela. Não toca o banco.
        public string ParameterName { get; set; }

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

        // Quando false, essa entrada resolve normalmente e alimenta o pool de parâmetros da linha
        // (vira {ColumnName} pras demais colunas) mas NÃO entra na lista de colunas/valores do
        // INSERT final — útil pra expor um campo do JSON (ex.: cIDTrip) que a tabela de destino não
        // possui, mas que é necessário como {placeholder} de uma Query Geral/Customizada de outra
        // coluna dessa mesma tabela.
        public bool IncludeInInsert { get; set; } = true;
    }
}
