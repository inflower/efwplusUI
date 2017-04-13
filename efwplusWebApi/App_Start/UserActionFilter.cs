using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using efwplusWebApi.SSO;

namespace efwplusWebApi.App_Start
{
    public class UserActionFilter: ActionFilterAttribute
    {
        private const string Key = "__action_duration__";
        private const string tokenKey = "__user_token__";
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var stopWatch = new Stopwatch();
            actionContext.Request.Properties[Key] = stopWatch;
            stopWatch.Start();

            WebApiGlobal.ShowMsg("开始执行：" + actionContext.Request.RequestUri.LocalPath);

            if (actionContext.Request.RequestUri.AbsolutePath.ToLower().IndexOf("/Login/submit".ToLower()) == -1 && WebApiGlobal.IsToken == true)
            {
                try
                {
                    string token = null;
                    string[] qs = actionContext.Request.RequestUri.Query.ToLower().Split(new char[] { '?', '&' });
                    foreach (var s in qs)
                    {
                        string[] kv = s.Split(new char[] { '=' });
                        if (kv.Length == 2 && kv[0] == "token")
                        {
                            token = kv[1];
                            break;
                        }
                    }

                    if ((token != null))
                    {
                        AuthResult result = SsoHelper.ValidateToken(token);
                        if (!string.IsNullOrEmpty(result.ErrorMsg))
                            throw new Exception(result.ErrorMsg);

                        actionContext.Request.Properties[tokenKey] = result.User;
                    }
                    else
                    {

                        throw new Exception("token is empty");
                    }
                }
                catch (Exception e)
                {
                    WebApiGlobal.ShowMsg("执行失败：token failed to " + actionContext.Request.RequestUri.LocalPath);
                    throw new Exception("token failed !" + e.Message);
                }
            }

            base.OnActionExecuting(actionContext);
        }
        public override void OnActionExecuted(HttpActionExecutedContext actionExecutedContext)
        {
            if (actionExecutedContext.Request.Properties.ContainsKey(Key))
            {
                var stopWatch = actionExecutedContext.Request.Properties[Key] as Stopwatch;
                if (stopWatch != null)
                {
                    stopWatch.Stop();
                    WebApiGlobal.ShowMsg("执行完成(耗时["+ stopWatch.Elapsed.TotalMilliseconds + "])："+ actionExecutedContext.Request.RequestUri.LocalPath);
                    //actionExecutedContext.Request.Properties.Remove(Key);
                }
            }

            if (actionExecutedContext.Response != null)
            {
                actionExecutedContext.Response.Headers.Add("Access-Control-Allow-Credentials", "true");
                actionExecutedContext.Response.Headers.Add("Access-Control-Allow-Methods", "get, put, post, delete, options");
                actionExecutedContext.Response.Headers.Add("Access-Control-Allow-Headers", "authorization, origin, content-type, accept, token");
                actionExecutedContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");
            }
            base.OnActionExecuted(actionExecutedContext);
        }
    }
}
