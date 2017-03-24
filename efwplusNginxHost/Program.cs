using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace efwplusNginxHost
{
    class Program
    {
        static void Main(string[] args)
        {
            setprivatepath();
            try
            {
                MongodbManager.StopDB();
                MongodbManager.StartDB();
                ShowMsg("MongoDB已启动");
                NginxManager.StopWeb();
                NginxManager.StartWeb();
                ShowMsg("Nginx已启动");
                efwplusWebApi.WebApiGlobal.ShowMsg = ShowMsg;
                efwplusWebApi.WebApiGlobal.Main();

                Process.Start("http://localhost:8088/login.html");
            }
            catch (Exception e)
            {
                ShowMsg("启动服务失败\n\r" + e.Message);
            }
            while (true)
            {
                System.Threading.Thread.Sleep(30 * 1000);
            }
        }

        static void setprivatepath()
        {
            string privatepath = @"Component";
            AppDomain.CurrentDomain.SetData("PRIVATE_BINPATH", privatepath);
            AppDomain.CurrentDomain.SetData("BINPATH_PROBE_ONLY", privatepath);
            var m = typeof(AppDomainSetup).GetMethod("UpdateContextProperty", BindingFlags.NonPublic | BindingFlags.Static);
            var funsion = typeof(AppDomain).GetMethod("GetFusionContext", BindingFlags.NonPublic | BindingFlags.Instance);
            m.Invoke(null, new object[] { funsion.Invoke(AppDomain.CurrentDomain, null), "PRIVATE_BINPATH", privatepath });
        }

        static void ShowMsg(string msg)
        {
            string text = ("[" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "] : " + msg);
            Console.WriteLine(text);
        }
    }
}
