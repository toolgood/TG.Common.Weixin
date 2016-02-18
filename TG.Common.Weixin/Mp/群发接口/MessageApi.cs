using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TG.Common.Weixin.Mp;
using TG.Common.Weixin.Mp.Datas;

namespace TG.Common.Weixin.Mp
{
    public class MessageApi : MpApi
    {
        public MessageApi(WxMpApi api) : base(api) { }
        private const string UrlFormat = "https://api.weixin.qq.com/cgi-bin/message/mass/sendall?access_token={0}";
        private const string UrlFormatUser = "https://api.weixin.qq.com/cgi-bin/message/mass/send?access_token={0}";
        private const string UrlFormatPreview = "https://api.weixin.qq.com/cgi-bin/message/mass/preview?access_token={0}";

        public SendResult SendText(string text, int timeOut = Config.TIME_OUT)
        {
            string url = string.Format(UrlFormat, _api.GetAccessToken());
            var data = new
            {
                filter = new { is_to_all = true },
                text = new { content = text },
                msgtype = "text"
            };
            return Post<SendResult>(url, data, timeOut);
        }
        public SendResult SendTextToGroup(int group, string text, int timeOut = Config.TIME_OUT)
        {
            string url = string.Format(UrlFormat, _api.GetAccessToken());
            var data = new
            {
                filter = new { is_to_all = false, group_id = group.ToString() },
                text = new { content = text },
                msgtype = "text"
            };
            return Post<SendResult>(url, data, timeOut);
        }
        public SendResult SendTextToUser(List<string> openIds, string text, int timeOut = Config.TIME_OUT)
        {
            string url = string.Format(UrlFormatUser, _api.GetAccessToken());
            var data = new
            {
                touser = openIds,
                text = new
                {
                    content = text
                },
                msgtype = "text"
            };
            return Post<SendResult>(url, data, timeOut);
        }
        public SendResult SendTextToPreview(string openId, string text, int timeOut = Config.TIME_OUT)
        {
            string url = string.Format(UrlFormatUser, _api.GetAccessToken());
            var data = new
            {
                touser = openId,
                text = new
                {
                    content = text
                },
                msgtype = "text"
            };
            return Post<SendResult>(url, data, timeOut);
        }

        public SendResult SendImage(string media_id, int timeOut = Config.TIME_OUT)
        {
            string url = string.Format(UrlFormat, _api.GetAccessToken());
            var data = new
            {
                filter = new { is_to_all = true },
                image = new { media_id = media_id },
                msgtype = "image"
            };
            return Post<SendResult>(url, data, timeOut);
        }
        public SendResult SendImageToGroup(int group, string media_id, int timeOut = Config.TIME_OUT)
        {
            string url = string.Format(UrlFormat, _api.GetAccessToken());
            var data = new
            {
                filter = new { is_to_all = false, group_id = group.ToString() },
                image = new { media_id = media_id },
                msgtype = "image"
            };
            return Post<SendResult>(url, data, timeOut);
        }
        public SendResult SendImageToUser(List<string> openIds, string media_id, int timeOut = Config.TIME_OUT)
        {
            string url = string.Format(UrlFormatUser, _api.GetAccessToken());
            var data = new
            {
                touser = openIds,
                image = new { media_id = media_id },
                msgtype = "image"
            };
            return Post<SendResult>(url, data, timeOut);
        }
        public SendResult SendImagePreview(string openId, string media_id, int timeOut = Config.TIME_OUT)
        {
            string url = string.Format(UrlFormatPreview, _api.GetAccessToken());
            var data = new
            {
                touser = openId,
                image = new { media_id = media_id },
                msgtype = "image"
            };
            return Post<SendResult>(url, data, timeOut);
        }

        public SendResult SendVoice(string media_id, int timeOut = Config.TIME_OUT)
        {
            string url = string.Format(UrlFormat, _api.GetAccessToken());
            var data = new
            {
                filter = new { is_to_all = true },
                voice = new { media_id = media_id },
                msgtype = "voice"
            };
            return Post<SendResult>(url, data, timeOut);
        }
        public SendResult SendVoiceToGroup(int group, string media_id, int timeOut = Config.TIME_OUT)
        {
            string url = string.Format(UrlFormat, _api.GetAccessToken());
            var data = new
            {
                filter = new { is_to_all = false, group_id = group.ToString() },
                voice = new { media_id = media_id },
                msgtype = "voice"
            };
            return Post<SendResult>(url, data, timeOut);
        }
        public SendResult SendVoiceToUser(List<string> openIds, string media_id, int timeOut = Config.TIME_OUT)
        {
            string url = string.Format(UrlFormatUser, _api.GetAccessToken());
            var data = new
            {
                touser = openIds,
                voice = new { media_id = media_id },
                msgtype = "voice"
            };
            return Post<SendResult>(url, data, timeOut);
        }
        public SendResult SendVoiceToPreview(string openId, string media_id, int timeOut = Config.TIME_OUT)
        {
            string url = string.Format(UrlFormatPreview, _api.GetAccessToken());
            var data = new
            {
                touser = openId,
                voice = new { media_id = media_id },
                msgtype = "voice"
            };
            return Post<SendResult>(url, data, timeOut);
        }

        private VideoMediaIdResult GetVideoMediaIdResult(string mediaId, string title, string description, int timeOut = Config.TIME_OUT)
        {
            var accessToken = _api.GetAccessToken();
            string url = string.Format("https://file.api.weixin.qq.com/cgi-bin/media/uploadvideo?access_token={0}", accessToken);
            var data = new
            {
                media_id = mediaId,
                title = title,
                description = description
            };
            return Post<VideoMediaIdResult>(url, data, timeOut);
        }
        public SendResult SendVideo(string media_id, string title, string description, int timeOut = Config.TIME_OUT)
        {
            var msg = GetVideoMediaIdResult(media_id, title, description);
            if (msg.errcode != 0) return new SendResult() { errcode = msg.errcode, errmsg = msg.errmsg, };

            string url = string.Format(UrlFormat, _api.GetAccessToken());
            var data = new
            {
                filter = new { is_to_all = true },
                mpvideo = new { media_id = msg.media_id },
                msgtype = "mpvideo"
            };
            return Post<SendResult>(url, data, timeOut);
        }
        public SendResult SendVideoToGroup(int group, string title, string description, string media_id, int timeOut = Config.TIME_OUT)
        {
            var msg = GetVideoMediaIdResult(media_id, title, description);
            if (msg.errcode != 0) return new SendResult() { errcode = msg.errcode, errmsg = msg.errmsg, };

            string url = string.Format(UrlFormat, _api.GetAccessToken());
            var data = new
            {
                filter = new { is_to_all = false, group_id = group.ToString() },
                mpvideo = new { media_id = msg.media_id },
                msgtype = "mpvideo"
            };
            return Post<SendResult>(url, data, timeOut);
        }
        public SendResult SendVideoToUser(List<string> openIds, string media_id, string title, string description, int timeOut = Config.TIME_OUT)
        {
            var msg = GetVideoMediaIdResult(media_id, title, description);
            if (msg.errcode != 0) return new SendResult() { errcode = msg.errcode, errmsg = msg.errmsg, };

            string url = string.Format(UrlFormatUser, _api.GetAccessToken());
            var data = new
            {
                touser = openIds,
                video = new { media_id = msg.media_id, title = title, description = description },
                msgtype = "video"
            };
            return Post<SendResult>(url, data, timeOut);
        }
        public SendResult SendVideoToPreview(string openId, string media_id, string title, string description, int timeOut = Config.TIME_OUT)
        {
            var msg = GetVideoMediaIdResult(media_id, title, description);
            if (msg.errcode != 0) return new SendResult() { errcode = msg.errcode, errmsg = msg.errmsg, };

            string url = string.Format(UrlFormatPreview, _api.GetAccessToken());
            var data = new
            {
                touser = openId,
                video = new { media_id = msg.media_id, title = title, description = description },
                msgtype = "video"
            };
            return Post<SendResult>(url, data, timeOut);
        }

        public SendResult SendMpNews(string media_id, int timeOut = Config.TIME_OUT)
        {
            string url = string.Format(UrlFormat, _api.GetAccessToken());
            var data = new
            {
                filter = new { is_to_all = true },
                mpnews = new { media_id = media_id },
                msgtype = "mpnews"
            };
            return Post<SendResult>(url, data, timeOut);
        }
        public SendResult SendMpNewsToGroup(int group, string media_id, int timeOut = Config.TIME_OUT)
        {
            string url = string.Format(UrlFormat, _api.GetAccessToken());
            var data = new
            {
                filter = new { is_to_all = false, group_id = group.ToString() },
                mpnews = new { media_id = media_id },
                msgtype = "mpnews"
            };
            return Post<SendResult>(url, data, timeOut);
        }
        public SendResult SendMpNewsToUser(List<string> openIds, string media_id, int timeOut = Config.TIME_OUT)
        {
            string url = string.Format(UrlFormatUser, _api.GetAccessToken());
            var data = new
            {
                touser = openIds,
                mpnews = new { media_id = media_id },
                msgtype = "mpnews"
            };
            return Post<SendResult>(url, data, timeOut);
        }
        public SendResult SendMpNewsToPreview(string openId, string media_id, int timeOut = Config.TIME_OUT)
        {
            string url = string.Format(UrlFormatPreview, _api.GetAccessToken());
            var data = new
            {
                touser = openId,
                mpnews = new { media_id = media_id },
                msgtype = "mpnews"
            };
            return Post<SendResult>(url, data, timeOut);
        }

        public SendResult SendCard(string card_id, int timeOut = Config.TIME_OUT)
        {
            string url = string.Format(UrlFormat, _api.GetAccessToken());
            var data = new
            {
                filter = new { is_to_all = true },
                wxcard = new { card_id = card_id },
                msgtype = "wxcard"
            };
            return Post<SendResult>(url, data, timeOut);
        }
        public SendResult SendCardToGroup(int group, string card_id, int timeOut = Config.TIME_OUT)
        {
            string url = string.Format(UrlFormat, _api.GetAccessToken());
            var data = new
            {
                filter = new { is_to_all = false, group_id = group.ToString() },
                wxcard = new { card_id = card_id },
                msgtype = "wxcard"
            };
            return Post<SendResult>(url, data, timeOut);
        }
        public SendResult SendCardToUser(List<string> openIds, string card_id, int timeOut = Config.TIME_OUT)
        {
            string url = string.Format(UrlFormatUser, _api.GetAccessToken());
            var data = new
            {
                touser = openIds,
                wxcard = new { card_id = card_id },
                msgtype = "wxcard"
            };
            return Post<SendResult>(url, data, timeOut);
        }
        public SendResult SendCardToPreview(string openId, string code, string timestamp, string signature, string card_id, int timeOut = Config.TIME_OUT)
        {
            string url = string.Format(UrlFormatPreview, _api.GetAccessToken());
            var data = new
            {
                touser = openId,
                wxcard = new { card_id = card_id },
                card_ext = string.Format("\"code\":\"{0}\",\"openid\":\"{1}\",\"timestamp\":\"{2}\",\"signature\":\"{3}\"", code, openId, timestamp, signature),
                msgtype = "wxcard"
            };
            return Post<SendResult>(url, data, timeOut);
        }

        /// <summary>
        /// 删除群发消息
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="msgId">发送出去的消息ID</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public JsonResult DeleteSendMessage(string msgId, int timeOut = Config.TIME_OUT)
        {
            var accessToken = _api.GetAccessToken();
            string url = string.Format("https://api.weixin.qq.com/cgi-bin/message/mass/delete?access_token={0}", accessToken);
            var data = new { msgid = msgId };
            return Post<JsonResult>(url, data, timeOut);
        }


        /// <summary>
        /// 查询群发消息发送状态【订阅号与服务号认证后均可用】
        /// </summary>
        /// <param name="msgId">群发消息后返回的消息id</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public GetSendResult GetMessageResult(string msgId, int timeOut = Config.TIME_OUT)
        {
            var accessToken = _api.GetAccessToken();
            string url = string.Format("https://api.weixin.qq.com/cgi-bin/message/mass/get?access_token={0}", accessToken);
            var data = new
            {
                msg_id = msgId
            };
            return Post<GetSendResult>(url, data, timeOut);
        }

    }
}
namespace TG.Common.Weixin
{
    partial class WxMpApi
    {
        MessageApi _MessageApi;
        /// <summary>
        /// 自定义菜单管理
        /// </summary>
        public MessageApi MessageApi
        {
            get
            {
                if (_MessageApi == null)
                {
                    _MessageApi = new MessageApi(this);
                }
                return _MessageApi;
            }
        }

    }
}