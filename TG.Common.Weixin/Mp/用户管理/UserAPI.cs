/*----------------------------------------------------------------
    Copyright (C) 2015 Senparc
    
    文件名：UserAPI.cs
    文件功能描述：用户接口
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
    
    修改标识：jsionr - 20150322
    修改描述：添加修改关注者备注信息接口
    
    修改标识：Senparc - 20150325
    修改描述：修改关注者备注信息开放代理请求超时时间
----------------------------------------------------------------*/

/*
    接口详见：http://mp.weixin.qq.com/wiki/index.php?title=%E8%8E%B7%E5%8F%96%E7%94%A8%E6%88%B7%E5%9F%BA%E6%9C%AC%E4%BF%A1%E6%81%AF
 */

using TG.Common.Weixin.Mp;
using TG.Common.Weixin.Mp.Datas;
using System.Collections.Generic;

namespace TG.Common.Weixin.Mp
{
    /// <summary>
    /// 用户接口
    /// </summary>
    public class UserApi : MpApi
    {
        public UserApi(WxMpApi api) : base(api) { }

        /// <summary>
        /// 获取用户信息
        /// </summary>
        /// <param name="openId">普通用户的标识，对当前公众号唯一</param>
        /// <param name="lang">返回国家地区语言版本，zh_CN 简体，zh_TW 繁体，en 英语</param>
        /// <returns></returns>
        public UserInfoJson Info( string openId, string lang = "zh_CN")
        {
            var accessToken = _api.GetAccessToken();
            string url = string.Format("https://api.weixin.qq.com/cgi-bin/user/info?access_token={0}&openid={1}&lang={2}",
                accessToken, openId, lang.ToString());
            return Get<UserInfoJson>(url);
        }

        /// <summary>
        /// 获取关注者OpenId信息
        /// </summary>
        /// <param name="nextOpenId"></param>
        /// <returns></returns>
        public OpenIdResultJson Get(string nextOpenId)
        {
            var accessToken = _api.GetAccessToken();
            string url = string.Format("https://api.weixin.qq.com/cgi-bin/user/get?access_token={0}", accessToken);
            if (!string.IsNullOrEmpty(nextOpenId))
            {
                url += "&next_openid=" + nextOpenId;
            }
            return Get<OpenIdResultJson>(url);
        }

        /// <summary>
        /// 修改关注者备注信息
        /// </summary>
        /// <param name="openId">普通用户的标识，对当前公众号唯一</param>
        /// <param name="remark">新的备注名，长度必须小于30字符</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public JsonResult UpdateRemark(string openId, string remark, int timeOut = Config.TIME_OUT)
        {
            var accessToken = _api.GetAccessToken();
            string url = string.Format("https://api.weixin.qq.com/cgi-bin/user/info/updateremark?access_token={0}", accessToken);
            var data = new
            {
                openid = openId,
                remark = remark
            };
            return Post<JsonResult>(url, data, timeOut);
        }

        /// <summary>
        /// 批量获取用户基本信息
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="userList"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public BatchGetUserInfoJson BatchGetUserInfo( List<BatchGetUserInfoData> userList, int timeOut = Config.TIME_OUT)
        {
            var accessToken = _api.GetAccessToken();
            string url = string.Format("https://api.weixin.qq.com/cgi-bin/user/info/batchget?access_token={0}",        accessToken);
            var data = new
            {
                user_list = userList,
            };
            return Post<BatchGetUserInfoJson>(url, data);
        }
    }
}
namespace TG.Common.Weixin
{
    partial class WxMpApi
    {
        private UserApi _UserApi;
        public UserApi UserApi
        {
            get
            {
                if (_UserApi==null)
                {
                    _UserApi= new UserApi(this);
                }
                return _UserApi;
            }
        }

    }
}