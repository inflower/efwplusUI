using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using efwplusWebApi;
using efwplusWebApi.App_Start;
using efwplusWebApi.SSO;
using MonitorPlatform.Models;

namespace MonitorPlatform.Controllers
{
    public class LoginController : ApiController
    {
        //提交登录
        [HttpPost]
        public object submit([FromBody] simpleUser siuser)
        {
            DBInit();//数据库初始化

            string token;
            MongoHelper<User> helperUser = new MongoHelper<User>("MonitorPlatform");
            User user = helperUser.FindAll(x => x.usercode == siuser.usercode).First();
            if (user != null)
            {
                //验证密码
                if (user.pwd == DESEncryptor.DesEncrypt(siuser.password))
                {
                    SsoHelper.SignIn(new UserInfo { UserCode = siuser.usercode, UserName = user.username, CreateDate = DateTime.Now }, out token);
                    return new { flag = true, username = user.username, token = token };
                }
            }

            return new { flag = false, username = "", token = "" };
        }

        //验证token
        [HttpGet]
        public object validatetoken(string token)
        {
            if (string.IsNullOrEmpty(token) == false)
            {
                AuthResult ar = SsoHelper.ValidateToken(token);
                if (string.IsNullOrEmpty(ar.ErrorMsg))
                    return new { flag = true, username = ar.User.UserName,token=ar.token };
                return new { flag = false, username = "" };
            }
            return new { flag = false, username = "" };
        }

        /// <summary>
        /// 数据库初始化
        /// </summary>
        private void DBInit()
        {
            MongoHelper<User> helperUser = new MongoHelper<User>();
            if (helperUser.Count() == 0)
            {
                User user;
                user = new User();
                user.usercode = "admin";
                user.pwd = DESEncryptor.DesEncrypt("123456");
                user.email = "343588387@qq.com";
                user.username = "卡卡棵";
                helperUser.Insert(user);
            }
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
