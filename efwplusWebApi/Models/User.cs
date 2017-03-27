using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using efwplusWebApi.App_Start;

namespace efwplusWebApi.Models
{
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

    public class User : AbstractMongoModel
    {
        public string usercode { get; set; }
        public string pwd { get; set; }
        public string username { get; set; }
        public string email { get; set; }
        public string phone { get; set; }
        /// <summary>
        /// 停用标记 0正常 1停用
        /// </summary>
        public int flag { get; set; }
    }
}
