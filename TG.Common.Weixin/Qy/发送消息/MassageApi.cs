using TG.Common.Weixin.Qy;
using TG.Common.Weixin.Qy.Datas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TG.Common.Weixin.Qy
{
    public class MassageApi : QyApi
    {
        public MassageApi(WxCorpApi api) : base(api) { }
        private const string URL_FORMAT = "https://qyapi.weixin.qq.com/cgi-bin/message/send?access_token={0}";

        /// <summary>
        /// 发送文本信息
        /// </summary>
        /// <param name="content">消息内容</param>
        /// <param name="safe">表示是否是保密消息，0表示否，1表示是，默认0</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public MassageResult SendTextToAll(string content, int safe = 0, int timeOut = Config.TIME_OUT)
        {
           return SendText("@all", "", "", content, safe, timeOut);
        }
        /// <summary>
        /// 发送图片消息
        /// </summary>
        /// <param name="mediaId">媒体资源文件ID</param>
        /// <param name="safe">表示是否是保密消息，0表示否，1表示是，默认0</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public MassageResult SendImageToAll(string mediaId, int safe = 0, int timeOut = Config.TIME_OUT)
        {
            return SendImage("@all", "", "", mediaId, safe, timeOut);
        }
        /// <summary>
        /// 发送语音消息
        /// </summary>
        /// <param name="mediaId">媒体资源文件ID</param>
        /// <param name="safe">表示是否是保密消息，0表示否，1表示是，默认0</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public MassageResult SendVoiceToAll(string mediaId, int safe = 0, int timeOut = Config.TIME_OUT)
        {
            return SendVoice("@all", "", "", mediaId, safe, timeOut);
        }
        /// <summary>
        /// 发送视频消息
        /// </summary>
        /// <param name="mediaId">媒体资源文件ID</param>
        /// <param name="title">视频消息的标题</param>
        /// <param name="description">视频消息的描述</param>
        /// <param name="safe">表示是否是保密消息，0表示否，1表示是，默认0</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public MassageResult SendVideoToAll(string mediaId, string title, string description, int safe = 0, int timeOut = Config.TIME_OUT)
        {
            return SendVideo("@all", "", "", mediaId, title, description, safe, timeOut);
        }
        /// <summary>
        /// 发送文件消息
        /// </summary>
        /// <param name="mediaId">媒体资源文件ID</param>
        /// <param name="safe">表示是否是保密消息，0表示否，1表示是，默认0</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public MassageResult SendFileToAll( string mediaId, int safe = 0, int timeOut = Config.TIME_OUT)
        {
            return SendFile("@all", "", "", mediaId,   safe, timeOut);
        }
        /// <summary>
        /// 发送图文消息
        /// </summary>
        /// <param name="articles">图文信息内容，包括title（标题）、description（描述）、url（点击后跳转的链接。企业可根据url里面带的code参数校验员工的真实身份）和picurl（图文消息的图片链接，支持JPG、PNG格式，较好的效果为大图640*320，小图80*80。如不填，在客户端不显示图片）</param>
        /// <param name="safe">表示是否是保密消息，0表示否，1表示是，默认0</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public MassageResult SendNewsToAll( List<Article> articles, int safe = 0, int timeOut = Config.TIME_OUT)
        {
            return SendNews("@all", "", "", articles, safe, timeOut);
        }

        /// <summary>
        /// 发送mpnews消息
        /// 注：mpnews消息与news消息类似，不同的是图文消息内容存储在微信后台，并且支持保密选项。
        /// </summary>
        /// <param name="articles"></param>
        /// <param name="safe">表示是否是保密消息，0表示否，1表示是，默认0</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public MassageResult SendMpNewsToAll(List<MpNewsArticle> articles, int safe = 0, int timeOut = Config.TIME_OUT)
        {
            return SendMpNews("@all", "", "", articles, safe, timeOut);
        }


        /// <summary>
        /// 发送文本信息
        /// </summary>
        /// <param name="toUser">UserID列表（消息接收者，多个接收者用‘|’分隔）。特殊情况：指定为@all，则向关注该企业应用的全部成员发送</param>
        /// <param name="toParty">PartyID列表，多个接受者用‘|’分隔。当touser为@all时忽略本参数</param>
        /// <param name="toTag">TagID列表，多个接受者用‘|’分隔。当touser为@all时忽略本参数</param>
        /// <param name="content">消息内容</param>
        /// <param name="safe">表示是否是保密消息，0表示否，1表示是，默认0</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public MassageResult SendText(string toUser, string toParty, string toTag, string content, int safe = 0, int timeOut = Config.TIME_OUT)
        {
            var accessToken = _api.GetAccessToken();
            var url = string.Format(URL_FORMAT, accessToken);
            var data = new
            {
                touser = toUser,
                toparty = toParty,
                totag = toTag,
                msgtype = "text",
                agentid = _api.GetAgentId().ToString(),
                text = new
                {
                    content = content
                },
                safe = safe
            };
            return Post<MassageResult>(url, data, timeOut);
        }

        /// <summary>
        /// 发送图片消息
        /// </summary>
        /// <param name="toUser">UserID列表（消息接收者，多个接收者用‘|’分隔）。特殊情况：指定为@all，则向关注该企业应用的全部成员发送</param>
        /// <param name="toParty">PartyID列表，多个接受者用‘|’分隔。当touser为@all时忽略本参数</param>
        /// <param name="toTag"></param>
        /// <param name="mediaId">媒体资源文件ID</param>
        /// <param name="safe">表示是否是保密消息，0表示否，1表示是，默认0</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public MassageResult SendImage(string toUser, string toParty, string toTag,string mediaId, int safe = 0, int timeOut = Config.TIME_OUT)
        {
            var accessToken = _api.GetAccessToken();
            var url = string.Format(URL_FORMAT, accessToken);
            var data = new
            {
                touser = toUser,
                toparty = toParty,
                totag = toTag,
                msgtype = "image",
                agentid = _api.GetAgentId().ToString(),
                image = new
                {
                    media_id = mediaId
                },
                safe = safe
            };
            return Post<MassageResult>(url, data, timeOut);
        }

        /// <summary>
        /// 发送语音消息
        /// </summary>
        /// <param name="toUser">UserID列表（消息接收者，多个接收者用‘|’分隔）。特殊情况：指定为@all，则向关注该企业应用的全部成员发送</param>
        /// <param name="toParty">PartyID列表，多个接受者用‘|’分隔。当touser为@all时忽略本参数</param>
        /// <param name="toTag">TagID列表，多个接受者用‘|’分隔。当touser为@all时忽略本参数</param>
        /// <param name="mediaId">媒体资源文件ID</param>
        /// <param name="safe">表示是否是保密消息，0表示否，1表示是，默认0</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public MassageResult SendVoice(string toUser, string toParty, string toTag, string mediaId, int safe = 0, int timeOut = Config.TIME_OUT)
        {
            var accessToken = _api.GetAccessToken();
            var url = string.Format(URL_FORMAT, accessToken);
            var data = new
            {
                touser = toUser,
                toparty = toParty,
                totag = toTag,
                msgtype = "voice",
                agentid = _api.GetAgentId().ToString(),
                voice = new
                {
                    media_id = mediaId
                },
                safe = safe
            };
            return Post<MassageResult>(url, data, timeOut);
        }

        /// <summary>
        /// 发送视频消息
        /// </summary>
        /// <param name="toUser">UserID列表（消息接收者，多个接收者用‘|’分隔）。特殊情况：指定为@all，则向关注该企业应用的全部成员发送</param>
        /// <param name="toParty">PartyID列表，多个接受者用‘|’分隔。当touser为@all时忽略本参数</param>
        /// <param name="toTag">TagID列表，多个接受者用‘|’分隔。当touser为@all时忽略本参数</param>
        /// <param name="mediaId">媒体资源文件ID</param>
        /// <param name="title">视频消息的标题</param>
        /// <param name="description">视频消息的描述</param>
        /// <param name="safe">表示是否是保密消息，0表示否，1表示是，默认0</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public MassageResult SendVideo(string toUser, string toParty, string toTag, string mediaId, string title, string description, int safe = 0, int timeOut = Config.TIME_OUT)
        {
            var accessToken = _api.GetAccessToken();
            var url = string.Format(URL_FORMAT, accessToken);


            var data = new
            {
                touser = toUser,
                toparty = toParty,
                totag = toTag,
                msgtype = "video",
                agentid = _api.GetAgentId().ToString(),
                video = new
                {
                    media_id = mediaId,
                    title = title,
                    description = description,
                },
                safe = safe
            };
            return Post<MassageResult>(url, data, timeOut);
        }

        /// <summary>
        /// 发送文件消息
        /// </summary>
        /// <param name="toUser">UserID列表（消息接收者，多个接收者用‘|’分隔）。特殊情况：指定为@all，则向关注该企业应用的全部成员发送</param>
        /// <param name="toParty">PartyID列表，多个接受者用‘|’分隔。当touser为@all时忽略本参数</param>
        /// <param name="toTag">TagID列表，多个接受者用‘|’分隔。当touser为@all时忽略本参数</param>
        /// <param name="mediaId">媒体资源文件ID</param>
        /// <param name="safe">表示是否是保密消息，0表示否，1表示是，默认0</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public MassageResult SendFile(string toUser, string toParty, string toTag,string mediaId, int safe = 0, int timeOut = Config.TIME_OUT)
        {
            var accessToken = _api.GetAccessToken();
            var url = string.Format(URL_FORMAT, accessToken);


            var data = new
            {
                touser = toUser,
                toparty = toParty,
                totag = toTag,
                msgtype = "file",
                agentid = _api.GetAgentId().ToString(),
                file = new
                {
                    media_id = mediaId
                },
                safe = safe
            };
            return Post<MassageResult>(url, data, timeOut);
        }

        /// <summary>
        /// 发送图文消息
        /// </summary>
        /// <param name="toUser">UserID列表（消息接收者，多个接收者用‘|’分隔）。特殊情况：指定为@all，则向关注该企业应用的全部成员发送</param>
        /// <param name="toParty">PartyID列表，多个接受者用‘|’分隔。当touser为@all时忽略本参数</param>
        /// <param name="toTag">TagID列表，多个接受者用‘|’分隔。当touser为@all时忽略本参数</param>
        /// <param name="articles">图文信息内容，包括title（标题）、description（描述）、url（点击后跳转的链接。企业可根据url里面带的code参数校验员工的真实身份）和picurl（图文消息的图片链接，支持JPG、PNG格式，较好的效果为大图640*320，小图80*80。如不填，在客户端不显示图片）</param>
        /// <param name="safe">表示是否是保密消息，0表示否，1表示是，默认0</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public MassageResult SendNews(string toUser, string toParty, string toTag, List<Article> articles, int safe = 0, int timeOut = Config.TIME_OUT)
        {
            var accessToken = _api.GetAccessToken();
            var url = string.Format(URL_FORMAT, accessToken);

            var data = new
            {
                touser = toUser,
                toparty = toParty,
                totag = toTag,
                msgtype = "news",
                agentid = _api.GetAgentId().ToString(),
                news = new
                {
                    articles = articles.Select(z => new
                    {
                        title = z.Title,
                        description = z.Description,
                        url = z.Url,
                        picurl = z.PicUrl//图文消息的图片链接，支持JPG、PNG格式，较好的效果为大图640*320，小图80*80
                    }).ToList()
                }
            };
            return Post<MassageResult>(url, data, timeOut);
        }

        /// <summary>
        /// 发送mpnews消息
        /// 注：mpnews消息与news消息类似，不同的是图文消息内容存储在微信后台，并且支持保密选项。
        /// </summary>
        /// <param name="toUser">UserID列表（消息接收者，多个接收者用‘|’分隔）。特殊情况：指定为@all，则向关注该企业应用的全部成员发送</param>
        /// <param name="toParty">PartyID列表，多个接受者用‘|’分隔。当touser为@all时忽略本参数</param>
        /// <param name="toTag">TagID列表，多个接受者用‘|’分隔。当touser为@all时忽略本参数</param>
        /// <param name="articles"></param>
        /// <param name="safe">表示是否是保密消息，0表示否，1表示是，默认0</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public MassageResult SendMpNews(string toUser, string toParty, string toTag,  List<MpNewsArticle> articles, int safe = 0, int timeOut = Config.TIME_OUT)
        {
            var accessToken = _api.GetAccessToken();
            var url = string.Format(URL_FORMAT, accessToken);

            var data = new
            {
                touser = toUser,
                toparty = toParty,
                totag = toTag,
                msgtype = "mpnews",
                agentid = _api.GetAgentId().ToString(),
                mpnews = new
                {
                    articles = articles.Select(z => new
                    {
                        title = z.title,
                        thumb_media_id = z.thumb_media_id,
                        author = z.author,
                        content_source_url = z.content_source_url,
                        content = z.content,
                        digest = z.digest,
                        show_cover_pic = z.show_cover_pic
                    }).ToList(),
                },
                safe = safe
            };
            return Post<MassageResult>(url, data, timeOut);
        }

    }
}
namespace TG.Common.Weixin
{
    partial class WxCorpApi
    {
        private MassageApi _MassageApi;
        /// <summary>
        /// 管理企业号应用
        /// </summary>
        public MassageApi MassageApi
        {
            get
            {
                if (_MassageApi==null)
                {
                    _MassageApi= new MassageApi(this);
                }
                return _MassageApi;
            }
        }
    }
}