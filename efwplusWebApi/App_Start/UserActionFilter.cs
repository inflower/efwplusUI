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

            //if (actionContext.Request.RequestUri.AbsolutePath.ToLower().IndexOf("/Login/submit".ToLower()) == -1)
            //{
            //    try
            //    {
            //        List<string> tokenlist = actionContext.Request.Headers.GetValues("token").ToList();
            //        string token = tokenlist.Count == 0 ? "" : tokenlist[0];
            //        AuthResult result = SsoHelper.ValidateToken(token);
            //        if (result.ErrorMsg != null)
            //            throw new Exception(result.ErrorMsg);

            //        actionContext.Request.Properties[tokenKey] = result.User;
            //    }
            //    catch (Exception e)
            //    {
            //        throw new Exception("token failed" + e.Message);
            //    }
            //}

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
