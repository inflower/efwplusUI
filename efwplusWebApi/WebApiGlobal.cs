using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using efwplusWebApi.App_Start;
using efwplusWebApi.SSO;

namespace efwplusWebApi
{
    public class WebApiGlobal
    {
        public static Action<string> ShowMsg;
        static WebApiSelfHosting webapiHost = null;
        public static void Main()
        {
            string url = System.Configuration.ConfigurationSettings.AppSettings["WebApiUri"];
            webapiHost = new WebApiSelfHosting(url);
            webapiHost.StartHost();

            SsoHelper.Start();

            ShowMsg("WebAPI服务已启动");
        }

        public static void Exit()
        {
            webapiHost.StopHost();

            ShowMsg("WebAPI服务已关闭");
        }
    }
}
