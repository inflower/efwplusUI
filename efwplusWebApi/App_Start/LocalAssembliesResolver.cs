using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Dispatcher;

namespace efwplusWebApi.App_Start
{
    public class LocalAssembliesResolver : DefaultAssembliesResolver
    {
        public override ICollection<System.Reflection.Assembly> GetAssemblies()
        {
            List<System.Reflection.Assembly> list = new List<System.Reflection.Assembly>();

            string ApiAssembly = AppDomain.CurrentDomain.BaseDirectory + "ApiAssembly";
            DirectoryInfo Dir = new DirectoryInfo(ApiAssembly);
            if (Dir.Exists)
            {
                FileInfo[] dlls = Dir.GetFiles("*.dll", SearchOption.AllDirectories);
                foreach (var i in dlls)
                {
                    list.Add(System.Reflection.Assembly.Load(i.Name.Replace(".dll", "")));
                }
            }
            list.Add(System.Reflection.Assembly.Load("efwplusWebApi"));
            return list;
        }
    }
}
