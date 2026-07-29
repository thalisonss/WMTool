namespace WMTool.Reprocessing.Exceptions
{
    public class UnresolvedParameterException : ReprocessingException
    {
        public UnresolvedParameterException(string parameterName)
            : base($"Parâmetro '{parameterName}' é exigido por um ou mais data sources mas não pôde ser resolvido. Informe um valor manual em Overrides.")
        {
        }
    }
}
