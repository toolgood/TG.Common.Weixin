using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace TG.Common.Weixin
{
    public abstract partial class WxCorpApi
    {
        private static string _accessToken;
        private static DateTime _nextGetTime=DateTime.Parse("2000-1-1");
        private static string _jsapi_ticket;
        private static DateTime _next_jsapi_GetTime = DateTime.Parse("2000-1-1");

        public abstract int GetAgentId();
        public abstract string GetAppid();
        public abstract string GetSecret();
        /// <summary>
        /// 获取AccessToken
        /// </summary>
        /// <returns></returns>
        public virtual string GetAccessToken()
        {
            if (_nextGetTime < DateTime.Now)
            {
                return GetAccessTokenFormHttp();
            }
            else
            {
                return _accessToken;
            }
        }
        /// <summary>
        /// 获取 JsApiTicket
        /// </summary>
        /// <returns></returns>
        public virtual string GetJsApiTicket()
        {
            if (_next_jsapi_GetTime < DateTime.Now)
            {
                return GetJsApiTicketFormHttp();
            }
            else
            {
                return _jsapi_ticket;
            }
        }

        #region 01 私有方法
        private string WebPost(string url, string json)
        {
            WebClientEx web = new WebClientEx();
            web.ResetHeaders();
            //web.Headers.Add();
            web.Encoding = Encoding.UTF8;
            return web.UploadString(url, "POST", json);
        }
        private string WebPost(string url, object obj)
        {
            var json = JsonConvert.SerializeObject(obj);
            WebClientEx web = new WebClientEx();
            web.ResetHeaders();
            web.Encoding = Encoding.UTF8;
            return web.UploadString(url, "POST", json);
        }
        private string WebGet(string url)
        {
            WebClientEx web = new WebClientEx();
            web.ResetHeaders();
            web.Encoding = Encoding.UTF8;
            return web.DownloadString(url);
        }
        private string WebUpload(string url, string filePath)
        {
            WebClientEx web = new WebClientEx();
            web.ResetHeaders();
            web.Encoding = Encoding.UTF8;
            var bytes = web.UploadFile(url, "POST", filePath);
            return Encoding.UTF8.GetString(bytes);
        }


        private string GetAccessTokenFormHttp()
        {
            var url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/gettoken?corpid={0}&corpsecret={1}", GetAppid(), GetSecret());
            var json = WebGet(url);
            var dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
            try
            {
                _accessToken = dict["access_token"];
                _nextGetTime = DateTime.Now.AddSeconds(-1).AddSeconds(int.Parse(dict["expires_in"]));
                return _accessToken;
            }
            catch (Exception)
            {
                throw new Exception(dict["errmsg"]);
            }
        }
        private string GetJsApiTicketFormHttp()
        {
            var url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/get_jsapi_ticket?access_token={0}", GetAccessToken());
            var json = WebGet(url);
            var dict = JsonConvert.DeserializeObject<Dictionary<string, string>>(json);
            try
            {
                _jsapi_ticket = dict["ticket"];
                _next_jsapi_GetTime = DateTime.Now.AddSeconds(-1).AddSeconds(int.Parse(dict["expires_in"]));
                return _jsapi_ticket;
            }
            catch (Exception)
            {
                throw new Exception(dict["errmsg"]);
            }
        }
        #endregion
    }
}
