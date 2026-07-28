namespace WMTool.Validation.Models
{
    // Os 4 identificadores da nota sendo validada — os mesmos em toda regra de uma mesma execução,
    // por isso ficam como campos únicos na tela em vez de precisar ser mapeados regra a regra.
    public class ValidationContextInputs
    {
        public string CIDInvoice { get; set; }
        public string CSerie { get; set; }
        public string CIDBranchInvoice { get; set; }
        public string CIDCompany { get; set; }
    }
}
