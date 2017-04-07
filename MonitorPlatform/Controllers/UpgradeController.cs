using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using efwplusWebApi;

namespace MonitorPlatform.Controllers
{
    /// <summary>
    /// 上传文件，单个文件上传和下载
    /// </summary>
    public class UpgradeController : ApiController
    {
        [HttpPost]
        public string UploadMNode_updatexml()
        {
            return Upload("MNodeUpgrade\\update.xml");
        }
        [HttpPost]
        public string UploadMNode_updatezip()
        {
            return Upload("MNodeUpgrade\\update.zip");
        }

        [HttpPost]
        public string UploadWeb_updatexml()
        {
            return Upload("WebUpgrade\\update.xml");
        }
        [HttpPost]
        public string UploadWeb_updatezip()
        {
            return Upload("WebUpgrade\\update.zip");
        }

        [HttpPost]
        public string UploadWin_updatexml()
        {
            return Upload("ClientUpgrade\\update.xml");
        }
        [HttpPost]
        public string UploadWin_updatezip()
        {
            return Upload("ClientUpgrade\\update.zip");
        }


        //返回上传后的文件名
        //[HttpPost]
        private string Upload(string upgradename)
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            string filepath = WebApiGlobal.FileStore + @"\" + upgradename;
            FileInfo file = new FileInfo(filepath);
            if (file.Exists)//如果存在则先删除
            {
                File.Delete(filepath);
            }
            var provider = new CustomMultipartFormDataStreamProvider(file.DirectoryName, file.Name);
            try
            {
                var task = Request.Content.ReadAsMultipartAsync(provider);
                task.Wait();
                return filepath;
            }
            catch
            {
                throw new HttpResponseException(HttpStatusCode.InternalServerError);
            }
        }


        //下载文件
        [HttpGet]
        public HttpResponseMessage Download(string upgradename)
        {
            try
            {
                string filepath = WebApiGlobal.FileStore + @"\" + upgradename;
                FileInfo fileInfo = new FileInfo(filepath);
                if (fileInfo.Exists == false)
                    return new HttpResponseMessage(HttpStatusCode.NotFound);

                var stream = new FileStream(filepath, FileMode.Open);
                HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
                response.Content = new StreamContent(stream);
                response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
                response.Content.Headers.ContentDisposition = new ContentDispositionHeaderValue("attachment")
                {
                    FileName = fileInfo.Name
                };
                return response;
            }
            catch
            {
                return new HttpResponseMessage(HttpStatusCode.NoContent);
            }
        }
    }

    public class CustomMultipartFormDataStreamProvider : MultipartFormDataStreamProvider
    {
        private string _filename;
        public CustomMultipartFormDataStreamProvider(string path,string filename)
            : base(path)
        {
            _filename = filename;
        }

        public override string GetLocalFileName(HttpContentHeaders headers)
        {
            //var sb = new StringBuilder((headers.ContentDisposition.FileName ?? DateTime.Now.Ticks.ToString()).Replace("\"", "").Trim().Replace(" ", "_"));
            //Array.ForEach(Path.GetInvalidFileNameChars(), invalidChar => sb.Replace(invalidChar, '-'));
            //return sb.ToString();
            return _filename ?? DateTime.Now.Ticks.ToString();
        }
    }
}
