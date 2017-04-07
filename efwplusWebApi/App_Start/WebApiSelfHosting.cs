using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Dispatcher;
using System.Web.Http.SelfHost;
using EFWCoreLib.WcfFrame;

namespace efwplusWebApi.App_Start
{
    public class WebApiSelfHosting
    {
        private string WebApiUri;
        private HttpSelfHostServer server;

        public WebApiSelfHosting(string _WebApiUri)
        {
            //初始化操作
            if (_WebApiUri != null)
                WebApiUri = _WebApiUri;
            else
                WebApiUri = "http://localhost:8088";

            //初始化连接池,默认10分钟清理连接
            ClientLinkPoolCache.Init(true, 100, 30, 600, "webapi", 30);
        }

        ~WebApiSelfHosting()
        {
            StopHost();
        }

        public void StartHost()
        {
            var config = new HttpSelfHostConfiguration(WebApiUri);

            //格式化日期
            config.Formatters.JsonFormatter.SerializerSettings.Converters.Add(
                   new Newtonsoft.Json.Converters.IsoDateTimeConverter()
                   {
                       DateTimeFormat = "yyyy-MM-dd HH:mm:ss"
                   }
               );

            config.MaxBufferSize = 104857600;//最大缓存值100M
            config.MaxReceivedMessageSize = 104857600;
            //config.TransferMode = System.ServiceModel.TransferMode.Buffered;
            //
            config.Routes.MapHttpRoute(
                "efwplusApi",
                "efwApi/{controller}/{action}/{id}",
                new { id = RouteParameter.Optional });

            //指定程序集
            config.Services.Replace(typeof(IAssembliesResolver),
                                                   new LocalAssembliesResolver());
            //指定名称
            config.Services.Replace(typeof(IHttpControllerSelector),
                                                   new LocalHttpControllerSelector(config));
            //用户验证
            config.Filters.Add(new UserActionFilter());

            server = new HttpSelfHostServer(config);
            server.OpenAsync().Wait();
        }

        public void StopHost()
        {
            if (server != null)
                server.CloseAsync().Wait();
        }
    }
}
