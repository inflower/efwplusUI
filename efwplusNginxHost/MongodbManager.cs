using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace efwplusNginxHost
{
    /// <summary>
    /// Mongodb处理
    /// </summary>
    public class MongodbManager
    {
        public static Action<string> ShowMsg;
        static bool IsMongodb = false;
        /// <summary>
        /// 开启Mongodb
        /// </summary>
        public static void StartDB()
        {
            IsMongodb = ConfigurationSettings.AppSettings["mongodb"] == "true" ? true : false;
            if (IsMongodb == false) return;

            initpath();
            string config = String.Format(GetConfig_Temp(), AppDomain.CurrentDomain.BaseDirectory);
            SetConfig(config);

            string mongodExe = ConfigurationSettings.AppSettings["mongodb_binpath"] + @"\mongod.exe";
            string mongoConf = AppDomain.CurrentDomain.BaseDirectory + @"MongoDB\mongo.conf";

            System.Diagnostics.Process pro = new System.Diagnostics.Process();
            pro.StartInfo.FileName = mongodExe;
            pro.StartInfo.Arguments = "--config " + mongoConf;
            pro.StartInfo.UseShellExecute = false;
            //pro.StartInfo.RedirectStandardInput = true;
            //pro.StartInfo.RedirectStandardOutput = true;
            //pro.StartInfo.RedirectStandardError = true;
            pro.StartInfo.CreateNoWindow = true;
            pro.Start();
            //pro.WaitForExit();

            ShowMsg("MongoDB已启动");
        }
        /// <summary>
        /// 停止Mongodb
        /// </summary>
        public static void StopDB()
        {
            IsMongodb = ConfigurationSettings.AppSettings["mongodb"] == "true" ? true : false;
            if (IsMongodb == false) return;

            Process[] proc = Process.GetProcessesByName("mongod");//创建一个进程数组，把与此进程相关的资源关联。
            for (int i = 0; i < proc.Length; i++)
            {
                proc[i].Kill();  //逐个结束进程.
            }

            ShowMsg("MongoDB已停止");
        }

        private static string GetConfig_Temp()
        {
            string dbpath= ConfigurationSettings.AppSettings["mongodb_dbpath"];
            string mongoconf_temp = (dbpath == "" ? AppDomain.CurrentDomain.BaseDirectory : dbpath) + @"MongoDB\mongo_temp.conf";
            string conf = null;
            FileInfo file = new FileInfo(mongoconf_temp);
            if (file.Exists)
            {
                using (FileStream fsteam = file.OpenRead())
                {
                    byte[] buff = new byte[fsteam.Length];
                    fsteam.Read(buff, 0, buff.Length);
                    conf = Encoding.GetEncoding("gb2312").GetString(buff);
                }
            }
            return conf;
        }

        private static bool SetConfig(string conf)
        {
            string mongoconf = AppDomain.CurrentDomain.BaseDirectory + @"MongoDB\mongo.conf";
            FileInfo file = new FileInfo(mongoconf);
            if (file.Exists)
            {
                file.Delete();
                using (System.IO.StreamWriter sw = new System.IO.StreamWriter(mongoconf, true, Encoding.Default))
                {
                    sw.Write(conf);//直接追加文件末尾，不换行 
                }
                return true;
            }

            return false;
        }

        //初始化mongodb路径
        private static void initpath()
        {
            string db= AppDomain.CurrentDomain.BaseDirectory + @"MongoDB\data\db";
            string log = AppDomain.CurrentDomain.BaseDirectory + @"MongoDB\data\log";
            if (Directory.Exists(db) == false)
            {
                Directory.CreateDirectory(db);
            }

            if (Directory.Exists(log) == false)
            {
                Directory.CreateDirectory(log);
            }
        }
    }
}
