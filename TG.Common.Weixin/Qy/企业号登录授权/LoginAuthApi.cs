/*----------------------------------------------------------------
    Copyright (C) 2015 Senparc
    
    文件名：LoginAuthApi.cs
    文件功能描述：企业号登录授权接口
    
    
    创建标识：Senparc - 20150325
----------------------------------------------------------------*/

/*
    接口文档：http://qydev.weixin.qq.com/wiki/index.php?title=%E7%99%BB%E5%BD%95%E6%8E%88%E6%9D%83%E6%B5%81%E7%A8%8B%E8%AF%B4%E6%98%8E
 */

using TG.Common.Weixin.Qy;
using TG.Common.Weixin.Qy.Datas;

namespace TG.Common.Weixin.Qy
{

    public class LoginAuthApi : QyApi
    {
        public LoginAuthApi(WxCorpApi api) : base(api) { }

        /// <summary>
        /// 服务商引导用户进入登录授权页
        /// 1、用户进入服务商网站 用户进入服务商网站，如www.ABC.com。
        /// 2、服务商引导用户进入登录授权页 服务可以在自己的网站首页中放置“微信企业号登录”的入口，引导用户（指企业号系统管理员者）进入登录授权页。网址为: https://qy.weixin.qq.com/cgi-bin/loginpage?corp_id=xxxx&redirect_uri=xxxxx&state=xxxx 服务商需要提供corp_id，跳转uri和state参数，其中uri需要经过一次urlencode作为参数，state用于服务商自行校验session，防止跨域攻击。
        /// 3、用户确认并同意授权 用户进入登录授权页后，需要确认并同意将自己的企业号和登录账号信息授权给服务商，完成授权流程。
        /// 4、授权后回调URI，得到授权码和过期时间 授权流程完成后，会进入回调URI，并在URL参数中返回授权码和过期时间(redirect_url?auth_code=xxx&expires_in=600)
        /// 5、利用授权码调用企业号的相关API 在得到授权码后，第三方可以使用授权码换取登录授权信息。
        /// </summary>
        /// <param name="redirectUrl">跳转url</param>
        /// <param name="state">用于服务商自行校验session</param>
        /// <returns></returns>
        public string GetLoginAuthUrl(string redirectUrl, string state)
        {
            var corpId = _api.GetAppid();
            return string.Format("https://qy.weixin.qq.com/cgi-bin/loginpage?corp_id={0}&redirect_uri={1}&state={2}",
                              corpId, redirectUrl, state);
        }

        /// <summary>
        /// 获取应用提供商凭证
        /// </summary>
        /// <param name="corpId"></param>
        /// <param name="providerSecret"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public ProviderTokenResult GetProviderToken( string providerSecret, int timeOut = Config.TIME_OUT)
        {
            var corpId = _api.GetAppid();
            var url = "https://qyapi.weixin.qq.com/cgi-bin/service/get_provider_token";
            var data = new
            {
                corpid = corpId,
                provider_secret = providerSecret
            };
            return Post<ProviderTokenResult>(url, data, timeOut);
        }

        /// <summary>
        /// 获取企业号管理员登录信息
        /// </summary>
        /// <param name="providerAccessToken">服务提供商的accesstoken</param>
        /// <param name="authCode">oauth2.0授权企业号管理员登录产生的code</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public GetLoginInfoResult GetLoginInfo(string providerAccessToken, string authCode, int timeOut = Config.TIME_OUT)
        {
            string url =string.Format( "https://qyapi.weixin.qq.com/cgi-bin/service/get_login_info?provider_access_token={0}", providerAccessToken);
            var data = new
            {
                auth_code = authCode
            };
            return Post<GetLoginInfoResult>(url, data, timeOut);
        }
    }
}
namespace TG.Common.Weixin
{
    partial class WxCorpApi
    {
        private LoginAuthApi _LoginAuthApi;
        /// <summary>
        /// 企业号登录授权
        /// </summary>
        public LoginAuthApi LoginAuthApi
        {
            get
            {
                if (_LoginAuthApi==null)
                {
                    _LoginAuthApi= new LoginAuthApi(this);
                }
                return _LoginAuthApi;
            }
        }

    }
}