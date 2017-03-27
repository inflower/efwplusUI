using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using EFWCoreLib.WcfFrame;
using EFWCoreLib.WcfFrame.DataSerialize;
using efwplusWebApi.App_Start;
using TestApi.Models;

namespace TestApi.Controllers
{
    public class TestServicesController: EApiController
    {
        //获取所有服务
        [HttpGet]
        public Object GetAllServices()
        {
            try
            {
                List<dwPlugin> plist = ClientLinkManage.CreateConnection("Test").GetWcfServicesAllInfo();

                List<amazeuitreenode> tree = new List<amazeuitreenode>();
                foreach (var p in plist)
                {
                    amazeuitreenode nodep = new amazeuitreenode();
                    nodep.title = p.pluginname;
                    nodep.type = "folder";
                    nodep.childs = new List<amazeuitreenode>();
                    tree.Add(nodep);
                    foreach (var c in p.controllerlist)
                    {
                        amazeuitreenode nodec = new amazeuitreenode();
                        nodec.title = c.controllername;
                        nodec.type = "folder";
                        nodec.childs = new List<amazeuitreenode>();
                        nodep.childs.Add(nodec);

                        foreach (var m in c.methodlist)
                        {
                            amazeuitreenode nodem = new amazeuitreenode();
                            nodem.title = m;
                            nodem.type = "item";
                            nodem.attr = new Dictionary<string, string>();
                            nodem.attr.Add("plugin", p.pluginname);
                            nodem.attr.Add("controller", c.controllername);
                            nodem.attr.Add("method", m);
                            nodec.childs.Add(nodem);
                        }
                    }
                }
                return tree;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        //测试服务
        [HttpGet]
        public string TestServices(string plugin, string controller, string method, string para)
        {
            try
            {
                Action<ClientRequestData> requestAction = ((ClientRequestData request) =>
                {
                    request.Iscompressjson = false;
                    request.Isencryptionjson = false;
                    request.Serializetype = SerializeType.Newtonsoft;
                    //request.LoginRight = new CoreFrame.Business.SysLoginRight(1);
                    request.SetJsonData(para);
                });

                ServiceResponseData response = InvokeWcfService(plugin, controller, method, requestAction);
                return response.GetJsonData();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}
