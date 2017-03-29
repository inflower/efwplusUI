using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MonitorPlatform.Models
{
    public class amazeuitreenode
    {
        public string title { get; set; }
        public string type { get; set; }
        public List<amazeuitreenode> childs { get; set; }
        public Dictionary<string, string> attr { get; set; }
    }
}
