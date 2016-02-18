/*----------------------------------------------------------------
    Copyright (C) 2015 Senparc
    
    文件名：ThirdPartyAuthApi.cs
    文件功能描述：第三方应用授权接口
    
    
    创建标识：Senparc - 20150313
    
    修改标识：Senparc - 20150313
    修改描述：整理接口
 
    修改标识：Senparc - 20150313
    修改描述：开放代理请求超时时间
----------------------------------------------------------------*/

/*
    官方文档：http://qydev.weixin.qq.com/wiki/index.php?title=%E7%AC%AC%E4%B8%89%E6%96%B9%E5%BA%94%E7%94%A8%E6%8E%A5%E5%8F%A3%E8%AF%B4%E6%98%8E
 */

using TG.Common.Weixin.Qy;
using TG.Common.Weixin.Qy.Datas;

namespace TG.Common.Weixin.Qy
{
    public class ThirdPartyAuthApi : QyApi
    {
        public ThirdPartyAuthApi(WxCorpApi api) : base(api) { }
        /// <summary>
        /// 获取应用套件令牌
        /// </summary>
        /// <param name="suiteId">应用套件id</param>
        /// <param name="suiteSecret">应用套件secret</param>
        /// <param name="suiteTicket">微信后台推送的ticket</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public GetSuiteTokenResult GetSuiteToken(string suiteId, string suiteSecret, string suiteTicket, int timeOut = Config.TIME_OUT)
        {
            var url = "https://qyapi.weixin.qq.com/cgi-bin/service/get_suite_token";
            var data = new
            {
                suite_id = suiteId,
                suite_secret = suiteSecret,
                suite_ticket = suiteTicket
            };
            return Post<GetSuiteTokenResult>(url, data, timeOut);
        }

        /// <summary>
        /// 获取预授权码
        /// </summary>
        /// <param name="suiteAccessToken"></param>
        /// <param name="suiteId">应用套件id</param>
        /// <param name="appId">应用id，本参数选填，表示用户能对本套件内的哪些应用授权，不填时默认用户有全部授权权限</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public GetPreAuthCodeResult GetPreAuthCode(string suiteAccessToken, string suiteId, int[] appId, int timeOut = Config.TIME_OUT)
        {
            var url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/service/get_pre_auth_code?suite_access_token={0}", suiteAccessToken);
            var data = new
            {
                suite_id = suiteId,
                appid = appId
            };
            return Post<GetPreAuthCodeResult>(url, data, timeOut);
        }

        /// <summary>
        /// 获取企业号的永久授权码
        /// </summary>
        /// <param name="suiteAccessToken"></param>
        /// <param name="suiteId">应用套件id</param>
        /// <param name="authCode">临时授权码会在授权成功时附加在redirect_uri中跳转回应用提供商网站。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public  GetPermanentCodeResult GetPermanentCode(string suiteAccessToken, string suiteId, string authCode, int timeOut = Config.TIME_OUT)
        {
            var url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/service/get_permanent_code?suite_access_token={0}", suiteAccessToken);
            var data = new
            {
                suite_id = suiteId,
                auth_code = authCode
            };
            return Post<GetPermanentCodeResult>(url, data, timeOut);
        }

        /// <summary>
        /// 获取企业号的授权信息
        /// </summary>
        /// <param name="suiteAccessToken"></param>
        /// <param name="suiteId">应用套件id</param>
        /// <param name="authCorpId">授权方corpid</param>
        /// <param name="permanentCode">永久授权码，通过get_permanent_code获取</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public  GetAuthInfoResult GetAuthInfo(string suiteAccessToken, string suiteId, string authCorpId, string permanentCode, int timeOut = Config.TIME_OUT)
        {
            var url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/service/get_auth_info?suite_access_token={0}", suiteAccessToken);
            var data = new
            {
                suite_id = suiteId,
                auth_corpid = authCorpId,
                permanent_code = permanentCode
            };
            return Post<GetAuthInfoResult>(url, data);
        }

        /// <summary>
        /// 获取企业号应用
        /// </summary>
        /// <param name="suiteAccessToken"></param>
        /// <param name="suiteId">应用套件id</param>
        /// <param name="authCorpId">授权方corpid</param>
        /// <param name="permanentCode">永久授权码，从get_permanent_code接口中获取</param>
        /// <param name="agentId">授权方应用id</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public  GetAgentResult GetAgent(string suiteAccessToken, string suiteId, string authCorpId, string permanentCode, string agentId, int timeOut = Config.TIME_OUT)
        {
            var url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/service/get_agent?suite_access_token={0}", suiteAccessToken);
            var data = new
            {
                suite_id = suiteId,
                auth_corpid = authCorpId,
                permanent_code = permanentCode,
                agentid = agentId
            };
            return Post<GetAgentResult>(url, data);
        }

        /// <summary>
        /// 设置企业号应用
        /// </summary>
        /// <param name="suiteAccessToken"></param>
        /// <param name="suiteId">应用套件id</param>
        /// <param name="authCorpId">授权方corpid</param>
        /// <param name="permanentCode">永久授权码，从get_permanent_code接口中获取</param>
        /// <param name="agent">要设置的企业应用的信息</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public  JsonResult SetAgent(string suiteAccessToken, string suiteId, string authCorpId, string permanentCode, ThirdParty_AgentData agent, int timeOut = Config.TIME_OUT)
        {
            var url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/service/set_agent?suite_access_token={0}", suiteAccessToken);
            var data = new
            {
                suite_id = suiteId,
                auth_corpid = authCorpId,
                permanent_code = permanentCode,
                agent = agent
            };
            return Post<JsonResult>(url, data);
        }

        /// <summary>
        /// 获取企业号access_token
        /// </summary>
        /// <param name="suiteAccessToken"></param>
        /// <param name="suiteId">应用套件id</param>
        /// <param name="authCorpId">授权方corpid</param>
        /// <param name="permanentCode">永久授权码，通过get_permanent_code获取</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public  GetCorpTokenResult GetCorpToken(string suiteAccessToken, string suiteId, string authCorpId, string permanentCode, int timeOut = Config.TIME_OUT)
        {
            var url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/service/get_corp_token?suite_access_token={0}", suiteAccessToken);

            var data = new
            {
                suite_id = suiteId,
                auth_corpid = authCorpId,
                permanent_code = permanentCode,
            };
            return Post<GetCorpTokenResult>(url, data);
        }
    }
}
namespace TG.Common.Weixin
{
    partial class WxCorpApi
    {
        private ThirdPartyAuthApi _ThirdPartyAuthApi;
        /// <summary>
        /// 管理企业号应用
        /// </summary>
        public ThirdPartyAuthApi ThirdPartyAuthApi
        {
            get
            {
                if (_ThirdPartyAuthApi == null)
                {
                    _ThirdPartyAuthApi = new ThirdPartyAuthApi(this);
                }
                return _ThirdPartyAuthApi;
            }
        }
    }
}