using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace efwplusWebApi.Controllers
{
    public class LoginController : ApiController
    {
        //提交登录
        [HttpPost]
        public object submit([FromBody] simpleUser siuser)
        {
            return new { flag = true, username = siuser.usercode };
        }
    }

    public class simpleUser
    {
        private string _usercode;
        /// <summary>
        /// 用户名
        /// </summary>
        public string usercode
        {
            get { return _usercode; }
            set { _usercode = value; }
        }

        private string _password;
        /// <summary>
        /// 密码
        /// </summary>
        public string password
        {
            get { return _password; }
            set { _password = value; }
        }
    }
}
