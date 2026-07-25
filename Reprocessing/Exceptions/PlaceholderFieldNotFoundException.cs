namespace WMTool.Reprocessing.Exceptions
{
    public class PlaceholderFieldNotFoundException : ReprocessingException
    {
        public PlaceholderFieldNotFoundException(string alias, string field)
            : base($"Campo '{field}' não encontrado no resultado do data source '{alias}'.")
        {
        }
    }
}
