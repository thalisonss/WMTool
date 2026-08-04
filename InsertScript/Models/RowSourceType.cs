namespace WMTool.InsertScript.Models
{
    // SingleRow: a tabela gera 1 INSERT usando o JSON raiz (ex.: MC1_Invoice, MC1_Order).
    // JsonArray: a tabela gera 1 INSERT por item de um array do JSON (ex.: produtos da nota em
    // MC1_OrderProduct), localizado por ArrayJsonPath.
    public enum RowSourceType
    {
        SingleRow,
        JsonArray
    }
}
