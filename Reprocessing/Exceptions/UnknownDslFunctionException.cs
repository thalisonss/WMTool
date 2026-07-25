namespace WMTool.Reprocessing.Exceptions
{
    public class UnknownDslFunctionException : ReprocessingException
    {
        public UnknownDslFunctionException(string functionName, string expression, string viewName)
            : base($"Função DSL desconhecida '{functionName}' na view '{viewName}' (expressão: '{expression}'). Registre um tradutor em DslFunctionRegistry antes de reprocessar.")
        {
        }
    }
}
