using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml.Serialization;

namespace TG.Common.Weixin.Codes
{
    public abstract class WebApi
    {
        protected T Get<T>(string url, int timeout = Config.TIME_OUT) where T : class
        {
            WebClientEx web = new WebClientEx();
            web.ResetHeaders();
            web.Encoding = Encoding.UTF8;
            web.SetTimeOut(timeout);
            var json = web.DownloadString(url);
            return Deserialize<T>(json);
        }
        protected T Post<T>(string url, string post, int timeout = Config.TIME_OUT) where T : class
        {
            WebClientEx web = new WebClientEx();
            web.ResetHeaders();
            web.Encoding = Encoding.UTF8;
            web.SetTimeOut(timeout);
            var json = web.UploadString(url, "POST", post);
            return Deserialize<T>(json);
        }
        protected T Post<T>(string url, object obj, int timeout = Config.TIME_OUT) where T : class
        {
            var post = JsonConvert.SerializeObject(obj);
            WebClientEx web = new WebClientEx();
            web.ResetHeaders();
            web.Encoding = Encoding.UTF8;
            web.SetTimeOut(timeout);
            var json = web.UploadString(url, "POST", post);
            return Deserialize<T>(json);
        }
        protected DownloadFileInfo GetFile(string url, int timeout = Config.TIME_OUT)
        {
            WebClientEx web = new WebClientEx();
            web.ResetHeaders();
            return new DownloadFileInfo(web, url);
        }

        protected T PostFile<T>(string url, string file, int timeout = Config.TIME_OUT) where T : class
        {
            WebClientEx web = new WebClientEx();
            web.ResetHeaders();
            web.Encoding = Encoding.UTF8;
            web.SetTimeOut(timeout);
            var bytes = web.UploadFile(url, file);
            var json = Encoding.UTF8.GetString(bytes);
            return Deserialize<T>(json);
        }
        protected T PostFile<T>(string url, string file, object obj, int timeout = Config.TIME_OUT) where T : class
        {
            WebClientEx web = new WebClientEx();
            web.ResetHeaders();
            web.Encoding = Encoding.UTF8;
            web.SetTimeOut(timeout);
            var bytes = web.UploadFile(url, file);
            var json = Encoding.UTF8.GetString(bytes);
            return Deserialize<T>(json);
        }


        private T Deserialize<T>(string text) where T : class
        {
            if (string.IsNullOrEmpty(text)) return null;
            if (text[0] == '<')
            {
                return XmlDeserialize<T>(text);
            }
            return JsonConvert.DeserializeObject<T>(text);
        }

        private T XmlDeserialize<T>(string xmlString) where T : class
        {
            var bytes = Encoding.UTF8.GetBytes(xmlString);

            using (MemoryStream ms = new MemoryStream(bytes))
            {
                XmlSerializer xml = new XmlSerializer(typeof(T));
                return xml.Deserialize(ms) as T;
            }
        }
    }
}
