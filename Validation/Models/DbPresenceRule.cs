namespace WMTool.Validation.Models
{
    // Checagem de presença/ausência: não compara dois valores, só verifica se uma query acha (ou não
    // acha) alguma coisa — o padrão usado em checagens tipo "XML com erro", "Pedido Split", "campo tal
    // não pode ser nulo".
    public class DbPresenceRule
    {
        public string Name { get; set; }
        public string SqlTemplate { get; set; }
        public PresenceExpectation Expectation { get; set; } = PresenceExpectation.RowsMustNotExist;

        // Opcional: nome de uma coluna do resultado (ex.: uma mensagem já formatada) usada como detalhe
        // quando a regra falha. Sem isso, o detalhe é só a contagem de linhas encontradas.
        public string MessageColumn { get; set; }
    }
}
