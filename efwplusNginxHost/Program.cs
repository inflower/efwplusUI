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
            Updater();
            StartListen();

            setprivatepath();
            try
            {
                MongodbManager.ShowMsg = ShowMsg;
                MongodbManager.StopDB();
                MongodbManager.StartDB();

                NginxManager.ShowMsg = ShowMsg;
                NginxManager.StopWeb();
                NginxManager.StartWeb();

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
            string privatepath = @"Component;ApiAssembly";
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

        static System.Timers.Timer timer;
        //
        static void StartListen()
        {
            timer = new System.Timers.Timer();
            timer.Interval = 5000;//1s
            timer.Elapsed += new System.Timers.ElapsedEventHandler(timer_Elapsed);
            timer.Start();
        }

        static void timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            try
            {
                timer.Enabled = false;
                Updater();
                timer.Enabled = true;
            }
            catch
            {
                timer.Enabled = true;
            }
        }

        static void Updater()
        {
            var updater = FSLib.App.SimpleUpdater.Updater.Instance;
            //当检查发生错误时,这个事件会触发
            //updater.Error += new EventHandler(updater_Error);
            //没有找到更新的事件
            //updater.NoUpdatesFound += new EventHandler(updater_NoUpdatesFound);
            //找到更新的事件.但在此实例中,找到更新会自动进行处理,所以这里并不需要操作
            //updater.UpdatesFound += new EventHandler(updater_UpdatesFound);
            //开始检查更新-这是最简单的模式.请现在 assemblyInfo.cs 中配置更新地址,参见对应的文件.
            FSLib.App.SimpleUpdater.Updater.CheckUpdateSimple("http://localhost:8810/FileStore/WebUpgrade/update.xml");
        }
    }
}
