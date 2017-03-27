using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace TestApi.Controllers
{
    public class HelloController : ApiController
    {
        [HttpGet]
        public object test()
        {
            return "Hello World";
        }
    }
}
