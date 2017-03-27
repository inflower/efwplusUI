using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using efwplusWebApi.App_Start;
using efwplusWebApi.Models;

namespace efwplusWebApi.Controllers
{
    public class LoginController : ApiController
    {
        //提交登录
        [HttpPost]
        public object submit([FromBody] simpleUser siuser)
        {
            DBInit();//数据库初始化

            string token;
            MongoHelper<User> helperUser = new MongoHelper<User>();
            User user = helperUser.FindAll(x => x.usercode == siuser.usercode).First();
            if (user != null)
            {
                //验证密码
                if (user.pwd == DESEncryptor.DesEncrypt(siuser.password))
                {
                    SSO.SsoHelper.SignIn(new SSO.UserInfo { UserCode = siuser.usercode, UserName = user.username, CreateDate = DateTime.Now }, out token);
                    return new { flag = true, username = user.username, token = token };
                }
            }

            return new { flag = false, username = "", token = "" };
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

    
}
