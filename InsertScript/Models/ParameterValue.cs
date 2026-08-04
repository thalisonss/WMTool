namespace WMTool.InsertScript.Models
{
    // Um par "Localizar/Substituir" da tela: Name é o nome canônico do placeholder, sem chaves
    // (ex.: "cIDOrderReferencia" para {cIDOrderReferencia}), Value é o texto que substitui.
    public class ParameterValue
    {
        public string Name { get; set; }
        public string Value { get; set; }
    }
}
