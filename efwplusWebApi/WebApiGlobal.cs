using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFWCoreLib.CoreFrame.ProcessManage;
using efwplusWebApi.App_Start;
using efwplusWebApi.SSO;

namespace efwplusWebApi
{
    public class WebApiGlobal
    {
        public static Action<string> ShowMsg;
        public static NormalIPCManager normalIPC;
        public static string FileStore;
        public static bool IsToken = false;

        static WebApiSelfHosting webapiHost = null;
        public static void Main()
        {
            Func<string, Dictionary<string, string>, string> _funcExecCmd = ExecuteCmd;
            Action<string> _actionReceiveData = ShowMsg;
            normalIPC = new NormalIPCManager(IPCType.efwplusWebAPI, _funcExecCmd, _actionReceiveData);
            IsToken = ConfigurationSettings.AppSettings["istoken"] == "true" ? true : false;
            FileStore = ConfigurationSettings.AppSettings["FileStore"];

            string url = ConfigurationSettings.AppSettings["WebApiUri"];
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

        static string ExecuteCmd(string m, Dictionary<string, string> a)
        {
            string retData = "succeed";
            try
            {

                switch (m)
                {
                    case "stop":
                        efwplusWebApi.WebApiGlobal.Exit();
                        break;
                    case "start":
                        efwplusWebApi.WebApiGlobal.Main();
                        break;
                    case "close":
                        Environment.Exit(0);
                        break;
                }
                ShowMsg("efwplusWebApi命令执行完成：" + m);
                return retData;
            }
            catch (Exception e)
            {
                retData = e.Message;
                return retData;
            }
        }
    }
}
