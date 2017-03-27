using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using EFWCoreLib.CoreFrame.ProcessManage;
using efwplusWebApi;

namespace TestApi.Controllers
{
    public class HelloController : ApiController
    {
        [HttpGet]
        public object test()
        {
            return "Hello World";
        }

        //获取配置信息
        [HttpGet]
        public string ShowConfig()
        {
            return WebApiGlobal.normalIPC.CallCmd(IPCName.GetProcessName(IPCType.efwplusBase), "getmnodetext", null);
        }
    }
}
