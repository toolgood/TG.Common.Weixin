using TG.Common.Weixin.Qy;
using TG.Common.Weixin.Qy.Datas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace TG.Common.Weixin.Qy
{
    public class JsApi : QyApi
    {
        public JsApi(WxCorpApi api) : base(api) { }

        private string GetTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }
        private string GetNonceStr()
        {
            Random r = new Random();
            r.Next();
            return r.Next(0, int.MaxValue).ToString();
        }
        private string GetSignature(string noncestr, string timestamp)
        {
            var jsapi_ticket = _api.GetJsApiTicket();
            var url = HttpContext.Current.Request.Url.ToString();
            url = url.Split('#')[0];
            string text = string.Format("jsapi_ticket={0}&noncestr={1}&timestamp={2}&url={3}", jsapi_ticket, noncestr, timestamp, url);
            return SHA1_Hash(text);
        }

        private string SHA1_Hash(string str)
        {
            System.Security.Cryptography.SHA1CryptoServiceProvider osha1 = new System.Security.Cryptography.SHA1CryptoServiceProvider();
            byte[] retVal = osha1.ComputeHash(Encoding.ASCII.GetBytes(str));
            osha1.Dispose();
            return BitConverter.ToString(retVal).Replace("-", "").ToLower();
        }

        private string GetJsApiConfig(bool inScript = false)
        {
            StringBuilder sb = new StringBuilder();
            if (inScript)
            {
                sb.Append("<script src=\"http://res.wx.qq.com/open/js/jweixin-1.1.0.js\"></script>");
            }
            sb.Append("wx.config({");
            sb.Append("debug: false,");
            sb.AppendFormat("appId: '{0}',", _api.GetAppid());
            var timestamp = GetTimeStamp();
            var nonceStr = GetNonceStr();
            sb.AppendFormat("timestamp:{0},", timestamp);
            sb.AppendFormat("nonceStr:'{0}',", nonceStr);
            sb.AppendFormat("signature:'{0}',", GetSignature(nonceStr, timestamp));
            sb.Append("jsApiList: [");
            sb.Append("'checkJsApi','onMenuShareTimeline',");
            sb.Append("'onMenuShareAppMessage','onMenuShareQQ','onMenuShareWeibo','hideMenuItems',");
            sb.Append("'showMenuItems','hideAllNonBaseMenuItem','showAllNonBaseMenuItem','translateVoice',");
            sb.Append("'startRecord','stopRecord','onRecordEnd','playVoice',");
            sb.Append("'pauseVoice','stopVoice','uploadVoice','downloadVoice',");
            sb.Append("'chooseImage','previewImage','uploadImage','downloadImage',");
            sb.Append("'getNetworkType','openLocation','getLocation','hideOptionMenu',");
            sb.Append("'showOptionMenu','closeWindow','scanQRCode','chooseWXPay',");
            sb.Append("'openProductSpecificView','addCard','chooseCard','openCard'");
            sb.Append("]");
            sb.Append("});");
            if (inScript)
            {
                return string.Format("<script>{0}</script>", sb.ToString());
            }
            return sb.ToString();
        }

        public HtmlString GetJsApiConfigHtml(bool inScript = true)
        {
            return new HtmlString(GetJsApiConfig(inScript));
        }
    }
}
namespace TG.Common.Weixin
{
    partial class WxCorpApi
    {
        private JsApi _JsApi;
        /// <summary>
        /// Js
        /// </summary>
        public JsApi JsApi
        {
            get
            {
                if (_JsApi==null)
                {
                    _JsApi= new JsApi(this);
                }
                return _JsApi;
            }
        }

    }
}
