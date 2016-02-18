using TG.Common.Weixin.Qy;
using TG.Common.Weixin.Qy.Datas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TG.Common.Weixin.Qy
{
    public class MailListApi : QyApi
    {
        public MailListApi(WxCorpApi api) : base(api) { }

        /// <summary>
        /// 创建部门
        /// </summary>
        /// <param name="name">部门名称。长度限制为1~64个字符</param>
        /// <param name="parentId">父亲部门id。根部门id为1 </param>
        /// <param name="order">在父部门中的次序。从1开始，数字越大排序越靠后</param>
        /// <param name="id">部门ID。用指定部门ID新建部门，不指定此参数时，则自动生成</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public CreateDepartmentResult CreateDepartment(string name, int parentId, int order = 1, int? id = null, int timeOut = Config.TIME_OUT)
        {
            var accessToken = _api.GetAccessToken();
            var url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/department/create?access_token={0}", accessToken);
            var data = new
            {
                name = name,
                parentid = parentId,
                order = order,
                id = id
            };
            return Post<CreateDepartmentResult>(url, data, timeOut);
        }

        /// <summary>
        /// 更新部门
        /// </summary>
        /// <param name="id">部门id</param>
        /// <param name="name">更新的部门名称。长度限制为0~64个字符。修改部门名称时指定该参数</param>
        /// <param name="parentId">父亲部门id。根部门id为1 </param>
        /// <param name="order">在父部门中的次序。从1开始，数字越大排序越靠后</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public JsonResult UpdateDepartment(string id, string name, int parentId, int order = 1, int timeOut = Config.TIME_OUT)
        {
            var accessToken = _api.GetAccessToken();
            var url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/department/update?access_token={0}", accessToken);
            var data = new
            {
                id = id,
                name = name,
                parentid = parentId,
                order = order
            };
            return Post<JsonResult>(url, data, timeOut);
        }

        /// <summary>
        /// 删除部门
        /// </summary>
        /// <param name="id">部门id。（注：不能删除根部门；不能删除含有子部门、成员的部门）</param>
        /// <returns></returns>
        public JsonResult DeleteDepartment(string id)
        {
            var accessToken = _api.GetAccessToken();
            var url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/department/delete?access_token={0}&id={1}", accessToken, id);
            return Get<JsonResult>(url);
        }

        /// <summary>
        /// 获取部门列表
        /// </summary>
        /// <param name="id">部门ID。获取指定部门ID下的子部门</param>
        /// <returns></returns>
        public GetDepartmentListResult GetDepartmentList(int? id = null)
        {
            var accessToken = _api.GetAccessToken();
            var url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/department/list?access_token={0}", accessToken);
            if (id.HasValue)
            {
                url += string.Format("&id={0}", id.Value);
            }
            return Get<GetDepartmentListResult>(url);
        }

        /// <summary>
        /// 创建成员(mobile/weixinid/email三者不能同时为空)
        /// </summary>
        /// <param name="userId">员工UserID。必须企业内唯一</param>
        /// <param name="name">成员名称。长度为1~64个字符</param>
        /// <param name="department">成员所属部门id列表。注意，每个部门的直属员工上限为1000个</param>
        /// <param name="position">职位信息。长度为0~64个字符</param>
        /// <param name="mobile">手机号码。必须企业内唯一</param>
        /// <param name="tel">办公电话。长度为0~64个字符</param>
        /// <param name="email">邮箱。长度为0~64个字符。必须企业内唯一</param>
        /// <param name="weixinId">微信号。必须企业内唯一</param>
        /// <param name="gender">性别。gender=0表示男，=1表示女。默认gender=0</param>
        /// <param name="avatarMediaid"></param>
        /// <param name="extattr">扩展属性。扩展属性需要在WEB管理端创建后才生效，否则忽略未知属性的赋值</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// accessToken、userId和name为必须的参数，其余参数不是必须的，可以传入null
        /// <returns></returns>
        public JsonResult CreateMember(string userId, string name, int[] department = null,
            string position = null, string mobile = null, string email = null, string weixinId = null, /*string tel = null,*/
            int gender = 0, string avatarMediaid = null, Extattr extattr = null, int timeOut = Config.TIME_OUT)
        {
            var accessToken = _api.GetAccessToken();
            var url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/user/create?access_token={0}", accessToken);

            var data = new
            {
                userid = userId,
                name = name,
                department = department,
                position = position,
                mobile = mobile,
                gender = gender,
                //tel = tel,

                email = email,
                weixinid = weixinId,
                avatar_mediaid = avatarMediaid,
                extattr = extattr
            };
            return Post<JsonResult>(url, data, timeOut);
        }

        ///// <param name="tel">办公电话。长度为0~64个字符</param>
        /// <summary>
        /// 更新成员(mobile/weixinid/email三者不能同时为空)
        /// </summary>
        /// <param name="userId">员工UserID。必须企业内唯一</param>
        /// <param name="name">成员名称。长度为1~64个字符</param>
        /// <param name="department">成员所属部门id列表。注意，每个部门的直属员工上限为1000个</param>
        /// <param name="position">职位信息。长度为0~64个字符</param>
        /// <param name="mobile">手机号码。必须企业内唯一</param>
        /// <param name="email">邮箱。长度为0~64个字符。必须企业内唯一</param>
        /// <param name="weixinId">微信号。必须企业内唯一</param>
        /// <param name="enable">启用/禁用成员。1表示启用成员，0表示禁用成员</param>
        /// <param name="gender">性别。gender=0表示男，=1表示女。默认gender=0</param>
        /// <param name="avatarMediaid"></param>
        /// <param name="extattr">扩展属性。扩展属性需要在WEB管理端创建后才生效，否则忽略未知属性的赋值</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// accessToken和userId为必须的参数，其余参数不是必须的，可以传入null
        /// <returns></returns>
        public JsonResult UpdateMember(string userId, string name = null, int[] department = null, string position = null,
            string mobile = null, string email = null, string weixinId = null, int enable = 1, /*string tel = null,*/
            int gender = 0, string avatarMediaid = null, Extattr extattr = null, int timeOut = Config.TIME_OUT)
        {
            var accessToken = _api.GetAccessToken();


            var url = "https://qyapi.weixin.qq.com/cgi-bin/user/update?access_token={0}";

            var data = new
            {
                userid = userId,
                name = name,
                department = department,
                position = position,
                mobile = mobile,

                //最新的接口中去除了以下两个字段
                gender = gender,
                //tel = tel,

                email = email,
                weixinid = weixinId,
                enable = enable,
                avatar_mediaid = avatarMediaid,
                extattr = extattr
            };
            return Post<JsonResult>(url, data, timeOut);
        }

        /// <summary>
        /// 删除成员
        /// </summary>
        /// <param name="userId">员工UserID</param>
        /// <returns></returns>
        public JsonResult DeleteMember(string userId)
        {
            var accessToken = _api.GetAccessToken();
            var url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/user/delete?access_token={0}&userid={1}", accessToken, userId);
            return Get<JsonResult>(url);
        }

        /// <summary>
        /// 批量删除成员
        /// </summary>
        /// <param name="userIds"></param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public JsonResult BatchDeleteMember(string[] userIds, int timeOut = Config.TIME_OUT)
        {
            var accessToken = _api.GetAccessToken();
            var url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/user/batchdelete?access_token={0}", accessToken);
            var data = new
            {
                useridlist = userIds
            };
            return Post<JsonResult>(url, data, timeOut);
        }

        /// <summary>
        /// 获取成员
        /// </summary>
        /// <param name="userId">员工UserID</param>
        /// <returns></returns>
        public GetMemberResult GetMember(string userId)
        {
            var accessToken = _api.GetAccessToken();
            var url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/user/get?access_token={0}&userid={1}", accessToken, userId);
            return Get<GetMemberResult>(url);
        }

        /// <summary>
        /// 获取部门成员
        /// </summary>
        /// <param name="departmentId">获取的部门id</param>
        /// <param name="fetchChild">1/0：是否递归获取子部门下面的成员</param>
        /// <param name="status">0获取全部员工，1获取已关注成员列表，2获取禁用成员列表，4获取未关注成员列表。status可叠加</param>
        /// <returns></returns>
        public GetDepartmentMemberResult GetDepartmentMember(int departmentId, int fetchChild, int status)
        {
            var accessToken = _api.GetAccessToken();
            var url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/user/simplelist?access_token={0}&department_id={1}&fetch_child={2}&status={3}", accessToken, departmentId, fetchChild, status);
            return Get<GetDepartmentMemberResult>(url);
        }

        /// <summary>
        /// 获取部门成员(详情)
        /// </summary>
        /// <param name="departmentId">获取的部门id</param>
        /// <param name="fetchChild">1/0：是否递归获取子部门下面的成员</param>
        /// <param name="status">0获取全部员工，1获取已关注成员列表，2获取禁用成员列表，4获取未关注成员列表。status可叠加</param>
        /// <returns></returns>
        public GetDepartmentMemberInfoResult GetDepartmentMemberInfo(int departmentId, int fetchChild, int status)
        {
            var accessToken = _api.GetAccessToken();
            var url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/user/list?access_token={0}&department_id={1}&fetch_child={2}&status={3}", accessToken, departmentId, fetchChild, status);
            return Get<GetDepartmentMemberInfoResult>(url);
        }

        /// <summary>
        /// 邀请成员关注
        /// 认证号优先使用微信推送邀请关注，如果没有weixinid字段则依次对手机号，邮箱绑定的微信进行推送，全部没有匹配则通过邮件邀请关注。 邮箱字段无效则邀请失败。 非认证号只通过邮件邀请关注。邮箱字段无效则邀请失败。 已关注以及被禁用用户不允许发起邀请关注请求。
        /// 测试发现同一个邮箱只发送一封邀请关注邮件，第二次再对此邮箱发送微信会提示系统错误
        /// </summary>
        /// <param name="userId">用户的userid</param>
        /// <param name="inviteTips">推送到微信上的提示语（只有认证号可以使用）。当使用微信推送时，该字段默认为“请关注XXX企业号”，邮件邀请时，该字段无效。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public InviteMemberResult InviteMember(string userId, int timeOut = Config.TIME_OUT)
        {
            var accessToken = _api.GetAccessToken();
            var url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/invite/send?access_token={0}", accessToken);
            var data = new
            {
                userid = userId,
            };
            return Post<InviteMemberResult>(url, data, timeOut);
        }


        /// <summary>
        /// 创建标签
        /// </summary>
        /// <param name="tagName">标签名称。长度为1~64个字符，标签不可与其他同组的标签重名，也不可与全局标签重名</param>
        /// <param name="tagId">标签id，整型，指定此参数时新增的标签会生成对应的标签id，不指定时则以目前最大的id自增。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public CreateTagResult CreateTag(string tagName, int? tagId = null, int timeOut = Config.TIME_OUT)
        {
            var accessToken = _api.GetAccessToken();
            var url =string.Format( "https://qyapi.weixin.qq.com/cgi-bin/tag/create?access_token={0}", accessToken);
            var data = new
            {
                tagname = tagName,
                tagid = tagId
            };
            return Post<CreateTagResult>(url, data, timeOut);
        }

        /// <summary>
        /// 更新标签名字
        /// </summary>
        /// <param name="tagId">标签ID</param>
        /// <param name="tagName">标签名称。长度为0~64个字符</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public JsonResult UpdateTag(int tagId, string tagName, int timeOut = Config.TIME_OUT)
        {
            var accessToken = _api.GetAccessToken();
            var url =string.Format( "https://qyapi.weixin.qq.com/cgi-bin/tag/update?access_token={0}",accessToken);
            var data = new
            {
                tagid = tagId,
                tagname = tagName
            };
            return Post<JsonResult>(url, data, timeOut);
        }

        /// <summary>
        /// 删除标签
        /// </summary>
        /// <param name="tagId">标签ID</param>
        /// <returns></returns>
        public JsonResult DeleteTag(int tagId)
        {
            var accessToken = _api.GetAccessToken();
            var url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/tag/delete?access_token={0}&tagid={1}", accessToken, tagId);
            return Get<JsonResult>(url);
        }

        /// <summary>
        /// 获取标签成员
        /// </summary>
        /// <param name="tagId">标签ID</param>
        /// <returns></returns>
        public GetTagMemberResult GetTagMember(int tagId)
        {
            var accessToken = _api.GetAccessToken();
            var url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/tag/get?access_token={0}&tagid={1}", accessToken, tagId);
            return Get<GetTagMemberResult>(url);
        }

        /// <summary>
        /// 增加标签成员
        /// </summary>
        /// <param name="tagId">标签ID</param>
        /// <param name="userList">企业成员ID列表，注意：userlist、partylist不能同时为空</param>
        /// <param name="partyList">企业部门ID列表，注意：userlist、partylist不能同时为空</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public AddTagMemberResult AddTagMember(int tagId, string[] userList = null, int[] partyList = null, int timeOut = Config.TIME_OUT)
        {
            var accessToken = _api.GetAccessToken();
            var url =string.Format( "https://qyapi.weixin.qq.com/cgi-bin/tag/addtagusers?access_token={0}",accessToken);
            var data = new
            {
                tagid = tagId,
                userlist = userList,
                partylist = partyList
            };
            return Post<AddTagMemberResult>(url, data, timeOut);
        }

        /// <summary>
        /// 删除标签成员
        /// </summary>
        /// <param name="tagId">标签ID</param>
        /// <param name="userList">企业员工ID列表</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public DelTagMemberResult DelTagMember(int tagId, string[] userList, int timeOut = Config.TIME_OUT)
        {
            var accessToken = _api.GetAccessToken();
            var url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/tag/deltagusers?access_token={0}", accessToken);
            var data = new
            {
                tagid = tagId,
                userlist = userList
            };
            return Post<DelTagMemberResult>(url, data, timeOut);
        }

        /// <summary>
        /// 获取标签列表
        /// </summary>
        /// <returns></returns>
        public GetTagListResult GetTagList()
        {
            var accessToken = _api.GetAccessToken();
            var url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/tag/list?access_token={0}", accessToken);
            return Get<GetTagListResult>(url);
        }

    }
}
namespace TG.Common.Weixin
{
    partial class WxCorpApi
    {
        private MailListApi _MailListApi;
        /// <summary>
        /// 管理通讯录
        /// </summary>
        public MailListApi MailListApi
        {
            get
            {
                if (_MailListApi==null)
                {
                    _MailListApi = new MailListApi(this);
                }
                return _MailListApi;
            }
        }


    }
}