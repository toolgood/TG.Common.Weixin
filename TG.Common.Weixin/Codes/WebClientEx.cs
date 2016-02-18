using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Runtime.Serialization.Formatters.Binary;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Text.RegularExpressions;

namespace System.Net
{
    [DesignTimeVisibleAttribute(false)]
    class WebClientEx : WebClient
    {
        private bool _useCookie = true;
        public CookieContainer Cookies;
        private WebProxy _proxy;
        private int? _timeout;
        private int? _readWriteTimeout;
        #region 01 构造函数
        public WebClientEx()
        {
            ServicePointManager.ServerCertificateValidationCallback = ValidateServerCertificate;
            Cookies = new CookieContainer();
        }
        private bool ValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }

        protected override WebRequest GetWebRequest(Uri address)
        {
            HttpWebRequest request = (HttpWebRequest)base.GetWebRequest(address);
            request.AutomaticDecompression = DecompressionMethods.Deflate | DecompressionMethods.GZip;
            if (_useCookie) request.CookieContainer = Cookies;
            request.AllowAutoRedirect = true;
            //request.KeepAlive = true;
            if (_proxy != null) request.Proxy = this.Proxy;
            if (_timeout != null) request.Timeout = (int)_timeout;
            if (_readWriteTimeout != null) request.Timeout = (int)_readWriteTimeout * 1000;
            return request;
        }
        #endregion

        #region 02 扩展 上传方法 和 下载图片方法
        public byte[] PostForm(string url, string formData)
        {
            this.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
            var postData = Encoding.ASCII.GetBytes(formData);
            return this.UploadData(url, "POST", postData);
        }
        public byte[] PostForm(string url, byte[] formData)
        {
            this.Headers.Add("Content-Type", "application/x-www-form-urlencoded");
            return this.UploadData(url, "POST", formData);
        }

        #endregion

        #region 03 加载 网页前操作
        public void ResetHeaders()
        {
            Headers.Clear();
            Headers.Add(HttpRequestHeader.Accept, "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8");
            Headers.Add(HttpRequestHeader.AcceptEncoding, "gzip,deflate");
            Headers.Add(HttpRequestHeader.AcceptLanguage, "zh-CN,zh;q=0.8");
            Headers.Add(HttpRequestHeader.UserAgent, "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; SV1; .NET CLR 1.1.4322; .NET CLR 2.0.50727)");
            //Headers.Add(HttpRequestHeader.UserAgent, "Baiduspider+(+http://www.baidu.com/search/spider.htm)");
        }
        public void SetTimeOut(int timeout)
        {
            this._timeout = timeout;
        }
        public void RemoveTimeOut()
        {
            this._timeout = null;
        }
        public void SetReadWriteTimeout(int timeout)
        {
            this._readWriteTimeout = timeout;
        }
        public void RemoveReadWriteTimeout()
        {
            this._readWriteTimeout = null;
        }

        public void SetReferer(string url)
        {
            if (url == "")
            {
                this.Headers.Remove(HttpRequestHeader.Referer);
            }
            else
            {
                this.Headers[HttpRequestHeader.Referer] = url;
            }
        }
        public void SetWebProxy(string ip, int proxy, string userName = "", string userPass = "")
        {
            ip = ip.Trim();
            userName = userName.Trim();
            userPass = userPass.Trim();
            _proxy = new WebProxy(ip, proxy);
            if (userName.Length != 0 && userPass.Length != 0)
                _proxy.Credentials = new NetworkCredential(userName, userPass);
        }
        public void RemoveWebProxy()
        {
            this._proxy = null;
        }
        #endregion

        #region 04 Cookie 操作
        public void CookieClose()
        {
            _useCookie = false;
        }
        public void CookieOpen()
        {
            _useCookie = true;
        }
        public string GetCookie(string url)
        {
            StringBuilder sb = new StringBuilder();
            try
            {
                Uri u = new Uri(url);
                foreach (Cookie cook in Cookies.GetCookies(u))
                {
                    sb.Append(cook.ToString());
                    sb.Append("; ");
                }
            }
            catch { }
            return sb.ToString();
        }
        public string GetCookieValue(string url, string key)
        {
            Uri u = new Uri(url);
            foreach (Cookie cook in Cookies.GetCookies(u))
            {
                if (cook.Name == key)
                {
                    return cook.Value;
                }
            }
            return null;
        }
        public void AddCookie(string url, string key, string value)
        {
            Cookies.Add(new Uri(url), new Cookie(key, value));
        }

        public byte[] GetCookieBytes()
        {
            using (MemoryStream ms = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(ms, Cookies);
                return ms.ToArray();
            }
        }
        public void SetCookieBytes(byte[] cookie)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                ms.Write(cookie, 0, cookie.Length);
                BinaryFormatter formatter = new BinaryFormatter();
                Cookies = (CookieContainer)formatter.Deserialize(ms);
            }
        }
        public void SaveCookies(string fileName)
        {
            using (Stream stream = File.Create(fileName))
            {
                try
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    formatter.Serialize(stream, Cookies);
                }
                catch (Exception) { }
            }
        }
        public void LoadCookies(string fileName)
        {
            try
            {
                using (Stream stream = File.Open(fileName, FileMode.Open))
                {
                    BinaryFormatter formatter = new BinaryFormatter();
                    Cookies = (CookieContainer)formatter.Deserialize(stream);
                }
            }
            catch (Exception)
            {
                Cookies = new CookieContainer();
            }
        }
        #endregion

        #region 05 EncodingHtml GetExtension
        public string EncodingHtml(string html)
        {
            Regex encodingRegex = new Regex(@"charset=([^ />]*)");
            var m = encodingRegex.Match(html);
            if (m.Success)
            {
                var encodingName = m.Groups[1].Value.Replace("\"", "").Replace("'", "");
                Encoding encoding = Encoding.GetEncoding(encodingName);
                if (encoding != this.Encoding)
                {
                    return encoding.GetString(this.Encoding.GetBytes(html));
                }
            }
            return html;
        }

        #endregion
    }
    internal class AcceptAllCertificatePolicy : ICertificatePolicy
    {
        public AcceptAllCertificatePolicy() { }

        public bool CheckValidationResult(ServicePoint sPoint, X509Certificate cert, WebRequest wRequest, int certProb)
        {
            return true;
        }
    } 
}
