using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WMTool.Utils
{
    public class GitHubRelease
    {
        public string tag_name { get; set; }
        public string body { get; set; }
        public List<GitHubAsset> assets { get; set; }
    }

    public class GitHubAsset
    {
        public string browser_download_url { get; set; }
    }
}
