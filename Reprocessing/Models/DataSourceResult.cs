using System.Data;

namespace WMTool.Reprocessing.Models
{
    public class DataSourceResult
    {
        public string Alias { get; set; }
        public DataTable Rows { get; set; }
    }
}
