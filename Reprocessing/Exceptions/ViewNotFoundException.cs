namespace WMTool.Reprocessing.Exceptions
{
    public class ViewNotFoundException : ReprocessingException
    {
        public ViewNotFoundException(string alias, string viewName)
            : base($"Data source '{alias}' (view '{viewName}') não encontrado ou desabilitado em MC1_View.")
        {
        }
    }
}
