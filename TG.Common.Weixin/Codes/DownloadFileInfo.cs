using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;

namespace TG.Common.Weixin
{
    public class DownloadFileInfo
    {
        private string _md5;
        public byte[] Data { get; set; }
        public string Url { get; set; }
        public string ContentType { get; set; }
        public string FileName { get; set; }
        public string FileExtension { get; set; }
        public string Date { get; set; }
        public string ContentLength { get; set; }
        public string FileMd5
        {
            get
            {
                if (_md5 == null)
                {
                    _md5 = GetMd5String(Data);
                }
                return _md5;
            }
        }

        public DownloadFileInfo() { }
        internal DownloadFileInfo(WebClient web, string url)
        {
            Url = url;
            Data = web.DownloadData(url);

            var rhs = web.ResponseHeaders;
            ContentType = rhs[System.Net.HttpResponseHeader.ContentType];
            ContentLength = rhs[System.Net.HttpResponseHeader.ContentLocation];
            Date = rhs[System.Net.HttpResponseHeader.Date];

            FileName = rhs["Content-disposition"];
            Regex re = new Regex("\"([^\"]+)\"");
            var m = re.Match(FileName);
            FileName = HttpUtility.UrlDecode(m.Groups[1].Value);
            FileExtension = System.IO.Path.GetExtension(FileName);
        }
        private string GetMd5String(byte[] bs)
        {
            System.Security.Cryptography.MD5 md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
            byte[] retVal = md5.ComputeHash(bs);
            md5.Dispose();
            return BitConverter.ToString(retVal).Replace("-", "");
        }
    }
}
