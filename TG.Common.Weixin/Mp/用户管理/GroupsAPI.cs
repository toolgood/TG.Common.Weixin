/*----------------------------------------------------------------
    Copyright (C) 2015 Senparc
    
    文件名：GroupsAPI.cs
    文件功能描述：用户组接口
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
 
    修改标识：Senparc - 20150312
    修改描述：开放代理请求超时时间
----------------------------------------------------------------*/

/* 
   API地址：http://mp.weixin.qq.com/wiki/0/56d992c605a97245eb7e617854b169fc.html
*/

using TG.Common.Weixin.Mp.Datas;
using TG.Common.Weixin.Mp;

namespace TG.Common.Weixin.Mp
{
    /// <summary>
    /// 用户组接口
    /// </summary>
    public class UserGroupsApi : MpApi
    {
        public UserGroupsApi(WxMpApi api) : base(api) { }
        /// <summary>
        /// 创建分组
        /// </summary>
        /// <param name="name">分组名字（30个字符以内）</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public CreateGroupResult Create(string name, int timeOut = Config.TIME_OUT)
        {
            var accessToken = _api.GetAccessToken();
            var url = string.Format("https://api.weixin.qq.com/cgi-bin/groups/create?access_token={0}", accessToken);
            var data = new
            {
                group = new
                {
                    name = name
                }
            };
            return Post<CreateGroupResult>(url, data, timeOut);
        }

        /// <summary>
        /// 获取所有分组
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <returns></returns>
        public GroupsJson Get()
        {
            var accessToken = _api.GetAccessToken();
            var urlFormat = "https://api.weixin.qq.com/cgi-bin/groups/get?access_token={0}";
            var url = string.Format(urlFormat, accessToken);
            return Get<GroupsJson>(url);
        }

        /// <summary>
        /// 获取用户分组
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="openId"></param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public GetGroupIdResult GetId(string openId, int timeOut = Config.TIME_OUT)
        {
            var accessToken = _api.GetAccessToken();
            var url = string.Format("https://api.weixin.qq.com/cgi-bin/groups/getid?access_token={0}", accessToken);
            var data = new { openid = openId };
            return Post<GetGroupIdResult>(url, data, timeOut);
        }

        /// <summary>
        /// 修改分组名
        /// </summary>
        /// <param name="id"></param>
        /// <param name="name">分组名字（30个字符以内）</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public JsonResult Update(int id, string name, int timeOut = Config.TIME_OUT)
        {
            var accessToken = _api.GetAccessToken();
            var url = string.Format("https://api.weixin.qq.com/cgi-bin/groups/update?access_token={0}", accessToken);
            var data = new
            {
                group = new
                {
                    id = id,
                    name = name
                }
            };
            return Post<JsonResult>(url, data, timeOut);
        }

        /// <summary>
        /// 移动用户分组
        /// </summary>
        /// <param name="openId"></param>
        /// <param name="toGroupId"></param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public JsonResult MemberUpdate(string openId, int toGroupId, int timeOut = Config.TIME_OUT)
        {
            var accessToken = _api.GetAccessToken();
            var url = string.Format("https://api.weixin.qq.com/cgi-bin/groups/members/update?access_token={0}", accessToken);
            var data = new
            {
                openid = openId,
                to_groupid = toGroupId
            };
            return Post<JsonResult>(url, data, timeOut);
        }

        /// <summary>
        /// 批量移动用户分组
        /// </summary>
        /// <param name="toGroupId">分组id</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <param name="openIds">用户唯一标识符openid的列表（size不能超过50）</param>
        /// <returns></returns>
        public  JsonResult BatchUpdate(int toGroupId, int timeOut = Config.TIME_OUT, params string[] openIds)
        {
            var accessToken = _api.GetAccessToken();
            var url = string.Format("https://api.weixin.qq.com/cgi-bin/groups/members/batchupdate?access_token={0}", accessToken);
            var data = new
            {
                openid_list = openIds,
                to_groupid = toGroupId
            };
            return Post<JsonResult>(url, data, timeOut);
        }

        /// <summary>
        /// 删除分组
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="groupId">分组id</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public JsonResult Delete(int groupId, int timeOut = Config.TIME_OUT)
        {
            var accessToken = _api.GetAccessToken();
            var url = string.Format("https://api.weixin.qq.com/cgi-bin/groups/delete?access_token={0}", accessToken);
            var data = new
            {
                group = new
                {
                    id = groupId
                }
            };
            return Post<JsonResult>(url, data, timeOut);
        }
    }
}
namespace TG.Common.Weixin
{
    partial class WxMpApi
    {
        private UserGroupsApi _UserGroupsApi;
        public UserGroupsApi GroupsApi
        {
            get
            {
                if (_UserGroupsApi==null)
                {
                    _UserGroupsApi= new UserGroupsApi(this);
                }
                return _UserGroupsApi;
            }
        }

    }
}