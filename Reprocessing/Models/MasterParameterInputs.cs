using System;
using System.Collections.Generic;

namespace WMTool.Reprocessing.Models
{
    public class MasterParameterInputs
    {
        public string CIDInvoice { get; set; }
        public string CSerie { get; set; }
        public string CIDBranchInvoice { get; set; }
        public string CIDCompany { get; set; }
        public IDictionary<string, string> Overrides { get; set; } = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        // Aliases de data source que o usuário optou por NÃO executar (ex.: views sabidamente lentas),
        // pra validar o resto do reprocessamento sem esperar por elas. O JSON final fica sem os campos
        // que viriam desses aliases.
        public ISet<string> DisabledAliases { get; set; } = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
    }
}
