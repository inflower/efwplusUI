using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

namespace efwplusWebApi.SSO
{
    /// <summary>
    /// 身份验证令牌管理
    /// </summary>
    public class TokenManager
    {
        private const int _TimerPeriod = 60000*20;//20分钟
        private static Timer thTimer;
        private static List<TokenInfo> tokenList = null;//令牌集合
        private static Object syncObj = new Object();//定义一个静态对象用于线程部份代码块的锁定，用于lock操作

        public static void Init()
        {
            tokenList = new List<TokenInfo>();
            //20分钟失效
            thTimer = new Timer(threadTimerCallback, null, _TimerPeriod, _TimerPeriod);
        }

        public static bool AddToken(TokenInfo entity)
        {
            lock (syncObj)
            {
                tokenList.Add(entity);
            }
            return true;
        }



        public static bool RemoveToken(string token)
        {
            TokenInfo tinfo = GetToken(token);
            if (tinfo != null)
            {
                lock (syncObj)
                {
                    tokenList.Remove(tinfo);
                }
            }
            return true;
        }

        public static AuthResult ValidateToken(string token)
        {
            AuthResult result = new AuthResult() { ErrorMsg = "Token不存在或已过期" };
            TokenInfo tinfo = GetToken(token);
            if (tinfo != null)
            {
                result.token = token;
                result.User = tinfo.userinfo;
                result.ErrorMsg = string.Empty;
            }
            return result;
        }


        public static TokenInfo GetToken(string token)
        {
            TokenInfo existToken = null;
            try
            {
                existToken = tokenList.Find(x => x.tokenId.ToString() == token);
            }
            catch { }
            return existToken;
        }

        private static void threadTimerCallback(Object state)
        {
            try
            {
                DateTime now = DateTime.Now;

                foreach (TokenInfo t in tokenList)
                {
                    if (((TimeSpan)(now - t.ActivityTime)).TotalMilliseconds > _TimerPeriod)
                    {
                        RemoveToken(t.tokenId.ToString());
                    }
                }
            }
            catch { }
        }
    }
}
