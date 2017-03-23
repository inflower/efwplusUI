using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace efwplusWebApi.Controllers
{
    public class MenuController : ApiController
    {
        [HttpGet]
        public object getmenu1()
        {
            return "菜单1";
        }

        [HttpGet]
        public object getmenu2()
        {
            return "菜单2";
        }

        [HttpGet]
        public object getmenu3()
        {
            return "菜单3";
        }

        [HttpGet]
        public object getmenu4()
        {
            return "菜单4";
        }
    }
}
