using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace efwplusWebApi.App_Start
{
    public class UserActionFilter: ActionFilterAttribute
    {
        private const string Key = "__action_duration__";
        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var stopWatch = new Stopwatch();
            actionContext.Request.Properties[Key] = stopWatch;
            stopWatch.Start();

            WebApiGlobal.ShowMsg("开始执行：" + actionContext.Request.RequestUri.LocalPath);

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
                actionExecutedContext.Response.Headers.Add("Access-Control-Allow-Headers", "authorization, origin, content-type, accept");
                actionExecutedContext.Response.Headers.Add("Access-Control-Allow-Origin", "*");
            }
            base.OnActionExecuted(actionExecutedContext);
        }
    }
}
