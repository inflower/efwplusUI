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
        //获取文件列表
        [HttpGet]
        public Object GetFiles()
        {
            string[] names = new string[3] { "MNodeUpgrade", "WebUpgrade", "ClientUpgrade" };
            Dictionary<string, List<object>> filedic = new Dictionary<string, List<object>>();
            foreach (string name in names)
            {
                string filepath = WebApiGlobal.FileStore + @"\" + name;
                string[] files = Directory.GetFiles(filepath);
                List<object> fileinfolist = new List<object>();
                foreach (string f in files)
                {
                    FileInfo file = new FileInfo(f);
                    fileinfolist.Add(new { filename = file.Name, filesize = System.Math.Ceiling(file.Length / 1024.0) + " KB", filepath = file.FullName, id = name + "\\" + file.Name });
                }

                filedic.Add(name, fileinfolist);
            }
            return filedic;
        }

        //删除文件
        [HttpGet]
        public void DeleteFile(string name)
        {
            string filepath = WebApiGlobal.FileStore + @"\" + name;
            FileInfo file = new FileInfo(filepath);
            if (file.Exists)//如果存在则先删除
            {
                File.Delete(filepath);
            }
        }

        //返回上传后的文件名
        [HttpPost]
        public string Upload(string name)
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }

            string filepath = WebApiGlobal.FileStore + @"\" + name;
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
        public HttpResponseMessage Download(string name)
        {
            try
            {
                string filepath = WebApiGlobal.FileStore + @"\" + name;
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
