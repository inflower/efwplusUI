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
            efwplusWebApi.WebApiGlobal.ShowMsg = ((string msg)=>
            {
                string text = ("[" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "] : " + msg);
                Console.WriteLine(text);
            });
            efwplusWebApi.WebApiGlobal.Main();

            NginxManager.StopWeb();
            NginxManager.StartWeb();

            Process.Start("http://localhost:8088/login.html");

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
    }
}
