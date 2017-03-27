using System;
using Newtonsoft.Json;

namespace efwplusWebApi.SSO
{
    /// <summary>
    /// 单点登录辅助类
    /// </summary>
    public class SsoHelper
    {        

        public static void Start()
        {
            TokenManager.Init();
        }

        /// <summary>
        /// 登录
        /// 由中间件根节点进行登录
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="tokenid"></param>
        /// <returns></returns>
        public static bool SignIn(UserInfo userInfo, out string tokenid)
        {
            TokenInfo token = new TokenInfo()
            {
                tokenId = Guid.NewGuid(),
                IsValid = true,
                CreateTime = DateTime.Now,
                ActivityTime = DateTime.Now,
                userinfo = userInfo
            };
            tokenid = token.tokenId.ToString();
            return TokenManager.AddToken(token);
        }
        /// <summary>
        /// 注销
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static bool SignOut(string token)
        {
            return TokenManager.RemoveToken(token);
        }


        /// <summary>
        /// 定时触发登录码的活动时间，频率必须小于4分钟
        /// </summary>
        /// <param name="token"></param>
        public static void UserActivity(string token)
        {
            TokenInfo existToken = TokenManager.GetToken(token);
            if (existToken != null)
                existToken.ActivityTime = DateTime.Now;
        }

        /// <summary>
        /// 是否有效登录
        /// 直接验证当前中间件节点
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public static AuthResult ValidateToken(string token)
        {
            UserActivity(token);
            return TokenManager.ValidateToken(token);
        }
    }
}
