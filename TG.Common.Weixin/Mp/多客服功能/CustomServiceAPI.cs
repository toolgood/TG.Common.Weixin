/*----------------------------------------------------------------
    Copyright (C) 2015 Senparc
    
    文件名：CustomServiceAPI.cs
    文件功能描述：多客服接口
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
    
    修改标识：Senparc - 20150306
    修改描述：增加多客服接口
 
    修改标识：Senparc - 20150312
    修改描述：开放代理请求超时时间
----------------------------------------------------------------*/

/* 
    多客服接口聊天记录接口，官方API：http://mp.weixin.qq.com/wiki/index.php?title=%E8%8E%B7%E5%8F%96%E5%AE%A2%E6%9C%8D%E8%81%8A%E5%A4%A9%E8%AE%B0%E5%BD%95
*/

using TG.Common.Weixin.Mp;
using TG.Common.Weixin.Mp.Datas;
using System;
using System.Collections.Generic;
using System.IO;

namespace TG.Common.Weixin.Mp
{
    /// <summary>
    /// 多客服接口
    /// </summary>
    public class CustomServiceApi : MpApi
    {
        public CustomServiceApi(WxMpApi api) : base(api) { }
        /// <summary>
        /// 获取用户聊天记录
        /// </summary>
        /// <param name="startTime">查询开始时间，会自动转为UNIX时间戳</param>
        /// <param name="endTime">查询结束时间，会自动转为UNIX时间戳，每次查询不能跨日查询</param>
        /// <param name="pageSize">每页大小，每页最多拉取1000条</param>
        /// <param name="pageIndex">查询第几页，从1开始</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public GetRecordResult GetRecord(DateTime startTime, DateTime endTime, int pageSize = 10, int pageIndex = 1, int timeOut = Config.TIME_OUT)
        {
            var accessToken = _api.GetAccessToken();
            var url = string.Format("https://api.weixin.qq.com/customservice/msgrecord/getrecord?access_token={0}", accessToken);
            //规范页码
            pageSize = Math.Max(1, pageSize);
            pageSize = Math.Min(50, pageSize);

            //组装发送消息
            var data = new
            {
                starttime = GetWeixinDateTime(startTime),
                endtime = GetWeixinDateTime(endTime),
                pagesize = pageSize,
                pageindex = pageIndex
            };
            return Post<GetRecordResult>(url, data, timeOut);
        }
        private  long GetWeixinDateTime(DateTime dateTime)
        {
            return (dateTime.Ticks - new DateTime(1970, 1, 1).Ticks) / 10000000 - 8 * 60 * 60;
        }
        /// <summary>
        /// 获取客服基本信息
        /// </summary>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public CustomInfoJson GetCustomBasicInfo(int timeOut = Config.TIME_OUT)
        {
            var accessToken = _api.GetAccessToken();
            var url = string.Format("https://api.weixin.qq.com/cgi-bin/customservice/getkflist?access_token={0}", accessToken);
            return Get<CustomInfoJson>(url, timeOut);
        }

        /// <summary>
        /// 获取在线客服接待信息
        /// </summary>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public CustomOnlineJson GetCustomOnlineInfo(int timeOut = Config.TIME_OUT)
        {
            var accessToken = _api.GetAccessToken();
            var url = string.Format("https://api.weixin.qq.com/cgi-bin/customservice/getonlinekflist?access_token={0}", accessToken);
            return Get<CustomOnlineJson>(url, timeOut);
        }

        /// <summary>
        /// 添加客服账号
        /// </summary>
        /// <param name="kfAccount">完整客服账号，格式为：账号前缀@公众号微信号，账号前缀最多10个字符，必须是英文或者数字字符。如果没有公众号微信号，请前往微信公众平台设置。</param>
        /// <param name="nickName">客服昵称，最长6个汉字或12个英文字符</param>
        /// <param name="passWord">客服账号登录密码，格式为密码明文的32位加密MD5值</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public JsonResult AddCustom(string kfAccount, string nickName, string passWord, int timeOut = Config.TIME_OUT)
        {
            var accessToken = _api.GetAccessToken();
            var url = string.Format("https://api.weixin.qq.com/customservice/kfaccount/add?access_token={0}", accessToken);
            var data = new
            {
                kf_account = kfAccount,
                nickname = nickName,
                password = passWord
            };
            return Get<JsonResult>(url, timeOut);
        }

        /// <summary>
        /// 设置客服信息
        /// </summary>
        /// <param name="kfAccount">完整客服账号，格式为：账号前缀@公众号微信号，账号前缀最多10个字符，必须是英文或者数字字符。如果没有公众号微信号，请前往微信公众平台设置。</param>
        /// <param name="nickName">客服昵称，最长6个汉字或12个英文字符</param>
        /// <param name="passWord">客服账号登录密码，格式为密码明文的32位加密MD5值</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public JsonResult UpdateCustom(string kfAccount, string nickName, string passWord, int timeOut = Config.TIME_OUT)
        {
            var accessToken = _api.GetAccessToken();
            var url = string.Format("https://api.weixin.qq.com/customservice/kfaccount/update?access_token={0}", accessToken);
            var data = new
            {
                kf_account = kfAccount,
                nickname = nickName,
                password = passWord
            };
            return Post<JsonResult>(url, data, timeOut);
        }

        /// <summary>
        /// 上传客服头像
        /// </summary>
        /// <param name="kfAccount">完整客服账号，格式为：账号前缀@公众号微信号</param>
        /// <param name="file">form-data中媒体文件标识，有filename、filelength、content-type等信息</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public JsonResult UploadCustomHeadimg(string kfAccount, string file, int timeOut = Config.TIME_OUT)
        {
            var accessToken = _api.GetAccessToken();
            var url = string.Format("http://api.weixin.qq.com/customservice/kfaccount/uploadheadimg?access_token={0}&kf_account={1}", accessToken, kfAccount);
            var data = new { media = Path.GetFileName(file) };
            return PostFile<JsonResult>(url, file, timeOut);
        }

        /// <summary>
        /// 删除客服账号
        /// </summary>
        /// <param name="kfAccount">完整客服账号，格式为：账号前缀@公众号微信号</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public JsonResult DeleteCustom(string kfAccount, int timeOut = Config.TIME_OUT)
        {
            var accessToken = _api.GetAccessToken();
            var url = string.Format("https://api.weixin.qq.com/customservice/kfaccount/del?access_token={0}&kf_account={1}", accessToken, kfAccount);
            return Get<JsonResult>(url);
        }

        /// <summary>
        /// 创建会话
        /// </summary>
        /// <param name="openId">客户openid</param>
        /// <param name="kfAccount">完整客服账号，格式为：账号前缀@公众号微信号</param>
        /// <param name="text">附加信息，文本会展示在客服人员的多客服客户端(非必须)</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public JsonResult CreateSession(string openId, string kfAccount, string text = null, int timeOut = Config.TIME_OUT)
        {
            var accessToken = _api.GetAccessToken();
            var url = string.Format("https://api.weixin.qq.com/customservice/kfsession/create?access_token={0}", accessToken);
            var data = new
            {
                openid = openId,
                kf_account = kfAccount,
                text = text
            };
            return Post<JsonResult>(url, data, timeOut);
        }

        /// <summary>
        /// 关闭会话
        /// </summary>
        /// <param name="openId">客户openid</param>
        /// <param name="kfAccount">完整客服账号，格式为：账号前缀@公众号微信号</param>
        /// <param name="text">附加信息，文本会展示在客服人员的多客服客户端(非必须)</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public JsonResult CloseSession(string openId, string kfAccount, string text = null, int timeOut = Config.TIME_OUT)
        {
            var accessToken = _api.GetAccessToken();
            var url = string.Format("https://api.weixin.qq.com/customservice/kfsession/close?access_token={0}", accessToken);
            var data = new
            {
                openid = openId,
                kf_account = kfAccount,
                text = text
            };
            return Post<JsonResult>(url, data, timeOut);
        }

        /// <summary>
        /// 获取客户的会话状态
        /// </summary>
        /// <param name="openId">客户openid</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public GetSessionStateResultJson GetSessionState(string openId, int timeOut = Config.TIME_OUT)
        {
            var accessToken = _api.GetAccessToken();
            var url = string.Format("https://api.weixin.qq.com/customservice/kfsession/getsession?access_token={0}&openid={1}", accessToken, openId);
            return Get<GetSessionStateResultJson>(url, timeOut);
        }

        /// <summary>
        /// 获取客服的会话列表
        /// </summary>
        /// <param name="kfAccount">完整客服账号，格式为：账号前缀@公众号微信号，账号前缀最多10个字符，必须是英文或者数字字符。</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public GetSessionListResultJson GetSessionList(string kfAccount, int timeOut = Config.TIME_OUT)
        {
            var accessToken = _api.GetAccessToken();
            var url = string.Format("https://api.weixin.qq.com/customservice/kfsession/getsessionlist?access_token={0}&kf_account={1}", accessToken, kfAccount);
            return Get<GetSessionListResultJson>(url, timeOut);
        }

        /// <summary>
        /// 获取未接入会话列表
        /// </summary>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public GetWaitCaseResultJson GetWaitCase(int timeOut = Config.TIME_OUT)
        {
            var accessToken = _api.GetAccessToken();
            var url = string.Format("https://api.weixin.qq.com/customservice/kfsession/getwaitcase?access_token={0}", accessToken);
            return Get<GetWaitCaseResultJson>(url, timeOut);
        }
    }
}
namespace TG.Common.Weixin
{
    partial class WxMpApi
    {
        private CustomServiceApi _CustomServiceApi;
        /// <summary>
        /// 多客服接口
        /// </summary>
        public CustomServiceApi CustomServiceApi
        {
            get
            {
                if (_CustomServiceApi==null)
                {
                    _CustomServiceApi= new CustomServiceApi(this);
                }
                return _CustomServiceApi;
            }
        }

    }
}