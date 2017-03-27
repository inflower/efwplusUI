using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using efwplusWebApi.App_Start;

namespace MonitorPlatform.Models
{
    public class User: AbstractMongoModel
    {
        public string usercode { get; set; }
        public string pwd { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        public int flag { get; set; }
    }
}
