using System;
using System.Collections.Generic;
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
            list.Add(System.Reflection.Assembly.Load("efwplusWebApi"));
            return list;
        }
    }
}
