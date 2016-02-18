/*----------------------------------------------------------------
    Copyright (C) 2015 Senparc
    
    文件名：UploadResultJson.cs
    文件功能描述：上传媒体文件返回结果
    
    
    创建标识：Senparc - 20150313
    
    修改标识：Senparc - 20150313
    修改描述：整理接口
    
    修改标识：Senparc - 20150703
    修改描述：增加获取OpenId
----------------------------------------------------------------*/

/*
    官方文档：http://qydev.weixin.qq.com/wiki/index.php?title=OAuth2%E9%AA%8C%E8%AF%81%E6%8E%A5%E5%8F%A3
 */

using TG.Common.Weixin.Qy;
using TG.Common.Weixin.Qy.Datas;
using System.Web;

namespace TG.Common.Weixin.Qy
{
    /// <summary>
    /// 1、创建Url获取code参数
    /// 2、通过code参数获取 UserId 参数
    /// 3、通过UserId参数，MailListApi类GetMember方法
    /// 
    /// 
    /// </summary>
    public class OAuth2Api : QyApi
    {
        public OAuth2Api(WxCorpApi api) : base(api) { }

        /// <summary>
        /// 跳转到企业网页时带上员工的身份信息
        /// </summary>
        /// <param name="redirect_uri"></param>
        /// <param name="state"></param>
        /// <returns></returns>
        public string GetOAuthUrl(string redirect_uri = "", string state = "STATE")
        {
            if (string.IsNullOrEmpty(redirect_uri))
            {
                redirect_uri = HttpContext.Current.Request.Url.ToString();
            }
            var url = string.Format(
                "https://open.weixin.qq.com/connect/oauth2/authorize?appid={0}&redirect_uri={1}&response_type=code&snsapi_base=snsapi_base&state={2}#wechat_redirect"
                , _api.GetAppid(), HttpUtility.UrlEncode(redirect_uri), state
                );
            return url;
        }

        /// <summary>
        /// 企业获取code
        /// </summary>
        /// <param name="corpId">企业的CorpID</param>
        /// <param name="redirectUrl">授权后重定向的回调链接地址，请使用urlencode对链接进行处理</param>
        /// <param name="state">重定向后会带上state参数，企业可以填写a-zA-Z0-9的参数值</param>
        /// <param name="responseType">返回类型，此时固定为：code</param>
        /// <param name="scope">应用授权作用域，此时固定为：snsapi_base</param>
        /// #wechat_redirect 微信终端使用此参数判断是否需要带上身份信息
        /// 员工点击后，页面将跳转至 redirect_uri/?code=CODE&state=STATE，企业可根据code参数获得员工的userid。
        /// <returns></returns>
        public string GetCode(string redirectUrl, string state, string responseType = "code", string scope = "snsapi_base")
        {
            var corpId = _api.GetAppid();
            var url = string.Format("https://open.weixin.qq.com/connect/oauth2/authorize?appid={0}&redirect_uri={1}&response_type={2}&scope={3}&state={4}#wechat_redirect", corpId, redirectUrl, responseType, scope, state);

            return url;
        }

        /// <summary>
        /// 获取成员信息
        /// </summary>
        /// <param name="code">通过员工授权获取到的code，每次员工授权带上的code将不一样，code只能使用一次，5分钟未被使用自动过期</param>
        /// 权限说明：管理员须拥有agent的使用权限；agentid必须和跳转链接时所在的企业应用ID相同。
        /// <returns></returns>
        public GetUserInfoResult GetUserId(string code)
        {
            var accessToken = _api.GetAccessToken();

            var url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/user/getuserinfo?access_token={0}&code={1}", accessToken, code);

            return Get<GetUserInfoResult>(url);
        }

        /// <summary>
        /// userid转换成openid接口
        /// </summary>
        /// <param name="userId">企业号内的成员id</param>
        /// <param name="agentId">需要发送红包的应用ID，若只是使用微信支付和企业转账，则无需该参数</param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public ConvertToOpenIdResult ConvertToOpenId(string userId, string agentId = null, int timeOut = Config.TIME_OUT)
        {
            var accessToken = _api.GetAccessToken();
            var url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/user/convert_to_openid?access_token={0}", accessToken);
            var data = new { userid = userId, agentid = agentId };
            return Post<ConvertToOpenIdResult>(url, data, timeOut);
        }

        /// <summary>
        /// openid转换成userid接口
        /// </summary>
        /// <param name="openId"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public ConvertToUserIdResult ConvertToUserId(string openId, int timeOut = Config.TIME_OUT)
        {
            var accessToken = _api.GetAccessToken();

            var url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/user/convert_to_userid?access_token={0}", accessToken);

            var data = new { openid = openId };
            return Post<ConvertToUserIdResult>(url, data, timeOut);
        }

    }
}
namespace TG.Common.Weixin
{
    partial class WxCorpApi
    {
        private OAuth2Api _OAuth2Api;
        /// <summary>
        /// 管理通讯录
        /// </summary>
        public OAuth2Api OAuth2Api
        {
            get
            {
                if (_OAuth2Api==null)
                {
                    _OAuth2Api = new OAuth2Api(this);
                }
                return _OAuth2Api;
            }
        }

    }
}
