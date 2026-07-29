namespace WMTool.Reprocessing.Exceptions
{
    public class TemplateNotFoundException : ReprocessingException
    {
        public TemplateNotFoundException(string templateName)
            : base($"Template '{templateName}' não encontrado ou desabilitado em MC1_DocumentTemplate.")
        {
        }
    }
}
