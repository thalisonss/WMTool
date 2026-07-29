using System.Collections.Generic;

namespace WMTool.Reprocessing.ViewDsl.Models
{
    public class ViewJoinDefinition
    {
        public string JoinType { get; set; }
        public string Entity { get; set; }
        public string Alias { get; set; }
        public string OnExpression { get; set; }

        // Presente quando "Entity" não é uma tabela real, e sim outra MC1_View usada como derived table
        // (ex.: Custom_WM_NF_ProductTot faz JOIN direto com Custom_WM_NF_ProductOrder). Cada entrada
        // remapeia um parâmetro DA VIEW ANINHADA (Name) para uma expressão da DSL no escopo de fora
        // (Value, ex.: ":varcIDOrder"). Parâmetros da view aninhada sem mapeamento explícito aqui são
        // passados adiante pelo mesmo nome (o parâmetro precisa existir com esse nome no escopo externo).
        public List<ViewParameterMapping> ParameterMappings { get; set; } = new List<ViewParameterMapping>();
    }
}
