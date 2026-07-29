namespace WMTool.Validation.Models
{
    public enum PresenceExpectation
    {
        // A query deve retornar 0 linhas (ex.: "XML com erro", "Pedido Split", checagem de nulos —
        // no script original, todo "SELECT ... WHERE condição ruim" que só devia achar algo quando tem
        // problema).
        RowsMustNotExist,

        // A query deve retornar pelo menos 1 linha (ex.: "deve existir um MC1_OrderInvoice pra essa nota").
        RowsMustExist
    }
}
