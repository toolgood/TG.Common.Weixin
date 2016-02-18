using TG.Common.Weixin.Qy;
using TG.Common.Weixin.Qy.Datas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TG.Common.Weixin.Qy
{
    public class AppApi : QyApi
    {
        public AppApi(WxCorpApi api) : base(api) { }
        /// <summary>
        /// 获取企业号应用信息
        /// </summary>
        /// <returns></returns>
        public GetAppInfoResult GetAppInfo(int timeOut = Config.TIME_OUT)
        {
            var accessToken = _api.GetAccessToken();
            var agentId = _api.GetAgentId();
            string url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/agent/get?access_token={0}&agentid={1}", accessToken, agentId);
            return Get<GetAppInfoResult>(url, timeOut);
        }
        /// <summary>
        /// 设置企业号应用
        /// 此App只能修改现有的并且有权限管理的应用，无法创建新应用（因为新应用没有权限）
        /// </summary>
        /// <returns></returns>
        public JsonResult SetApp(SetAppPostData data, int timeOut = Config.TIME_OUT)
        {
            var accessToken = _api.GetAccessToken();
            string url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/agent/set?access_token={0}", accessToken);
            return Post<JsonResult>(url, data, timeOut);
        }
        /// <summary>
        /// 获取应用概况列表
        /// </summary>
        /// <returns></returns>
        public GetAppListResult GetAppList(int timeOut = Config.TIME_OUT)
        {
            var accessToken = _api.GetAccessToken();
            string url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/agent/list?access_token={0}", accessToken);

            return Get<GetAppListResult>(url, timeOut);
        }


    }
}
namespace TG.Common.Weixin
{
    partial class WxCorpApi
    {
        private AppApi _AppApi;
        /// <summary>
        /// 管理企业号应用
        /// </summary>
        public AppApi AppApi
        {
            get
            {
                if (_AppApi==null)
                {
                    _AppApi= new AppApi(this); 
                }
                return _AppApi;
            }
        }


    }
}

