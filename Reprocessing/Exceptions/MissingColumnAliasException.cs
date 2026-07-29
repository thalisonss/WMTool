namespace WMTool.Reprocessing.Exceptions
{
    public class MissingColumnAliasException : ReprocessingException
    {
        public MissingColumnAliasException(string viewName, string expression)
            : base($"Coluna sem alias em expressão calculada na view '{viewName}': '{expression}'. É necessário um alias explícito nesse tipo de expressão.")
        {
        }
    }
}
