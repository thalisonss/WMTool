using System;
using System.Collections.Generic;
using WMTool.Reprocessing.Models;

namespace WMTool.Reprocessing.Caching
{
    public class CachedDataSourceSet
    {
        public string TemplateName { get; set; }
        public DateTime LastUpdatedUtc { get; set; }
        public List<DataSourceReference> DataSources { get; set; } = new List<DataSourceReference>();
    }
}
