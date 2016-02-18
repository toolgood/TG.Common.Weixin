using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TG.Common.Weixin.Datas
{
    public class WxReplyMsg
    {
        public WxReplyMsg()
        {
            CreateTime = GetTimeStamp();
        }

        public string ToUserName { get; set; }
        public string FromUserName { get; set; }
        public string CreateTime { get; set; }
        public string MsgType { get; set; }
        private string GetTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }
    }
    public class TextReplyMsg : WxReplyMsg
    {
        public TextReplyMsg() : base() { MsgType = "text"; }
        public TextReplyMsg(WxMsg msg) : base()
        {
            MsgType = "text";
            FromUserName = msg.ToUserName;
            ToUserName = msg.FromUserName;
        }
        public string Content { get; set; }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<xml>");
            sb.Append("<ToUserName><![CDATA[" + ToUserName + "]]></ToUserName>");
            sb.Append("<FromUserName><![CDATA[" + FromUserName + "]]></FromUserName>");
            sb.Append("<CreateTime>" + CreateTime + "</CreateTime>");
            sb.Append("<MsgType><![CDATA[" + MsgType + "]]></MsgType>");

            sb.Append("<Content><![CDATA[" + Content + "]]></Content>");

            sb.Append("</xml>");
            return sb.ToString();
        }
    }
    public class ImageReplyMsg : WxReplyMsg
    {
        public ImageReplyMsg() : base() { MsgType = "image"; }
        public ImageReplyMsg(WxMsg msg)
            : base()
        {
            MsgType = "image";
            FromUserName = msg.ToUserName;
            ToUserName = msg.FromUserName;
        }
        public string MediaId { get; set; }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<xml>");
            sb.Append("<ToUserName><![CDATA[" + ToUserName + "]]></ToUserName>");
            sb.Append("<FromUserName><![CDATA[" + FromUserName + "]]></FromUserName>");
            sb.Append("<CreateTime>" + CreateTime + "</CreateTime>");
            sb.Append("<MsgType><![CDATA[" + MsgType + "]]></MsgType>");

            sb.Append("<Image>");
            sb.Append("<MediaId><![CDATA[" + MediaId + "]]></MediaId>");
            sb.Append("</Image>");

            sb.Append("</xml>");
            return sb.ToString();
        }

    }
    public class VoiceReplyMsg : WxReplyMsg
    {
        public VoiceReplyMsg() : base() { MsgType = "voice"; }
        public VoiceReplyMsg(WxMsg msg)
            : base()
        {
            MsgType = "voice";
            FromUserName = msg.ToUserName;
            ToUserName = msg.FromUserName;
        }
        public string MediaId { get; set; }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<xml>");
            sb.Append("<ToUserName><![CDATA[" + ToUserName + "]]></ToUserName>");
            sb.Append("<FromUserName><![CDATA[" + FromUserName + "]]></FromUserName>");
            sb.Append("<CreateTime>" + CreateTime + "</CreateTime>");
            sb.Append("<MsgType><![CDATA[" + MsgType + "]]></MsgType>");

            sb.Append("<Voice>");
            sb.Append("<MediaId><![CDATA[" + MediaId + "]]></MediaId>");
            sb.Append("</Voice>");

            sb.Append("</xml>");
            return sb.ToString();
        }

    }
    public class VideoReplyMsg : WxReplyMsg
    {
        public VideoReplyMsg(WxMsg msg)
            : base()
        {
            MsgType = "video";
            FromUserName = msg.ToUserName;
            ToUserName = msg.FromUserName;
        }
        public VideoReplyMsg() : base() { MsgType = "video"; }
        public string MediaId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }



        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<xml>");
            sb.Append("<ToUserName><![CDATA[" + ToUserName + "]]></ToUserName>");
            sb.Append("<FromUserName><![CDATA[" + FromUserName + "]]></FromUserName>");
            sb.Append("<CreateTime>" + CreateTime + "</CreateTime>");
            sb.Append("<MsgType><![CDATA[" + MsgType + "]]></MsgType>");

            sb.Append("<Video>");
            sb.Append("<MediaId><![CDATA[" + MediaId + "]]></MediaId>");
            sb.Append("<Title><![CDATA[" + Title + "]]></Title>");
            sb.Append("<Description><![CDATA[" + Description + "]]></Description>");
            sb.Append("</Video>");

            sb.Append("</xml>");
            return sb.ToString();
        }
    }
    public class NewsReplyMsg : WxReplyMsg
    {
        public NewsReplyMsg(WxMsg msg)
            : base()
        {
            MsgType = "news";
            FromUserName = msg.ToUserName;
            ToUserName = msg.FromUserName;
            Articles = new List<Article>();
        }
        public NewsReplyMsg() : base() { MsgType = "news"; Articles = new List<Article>(); }
        public int ArticleCount { get { return Articles.Count; } }
        public List<Article> Articles { get; set; }

        public class Article
        {
            public string Title { get; set; }
            public string Description { get; set; }
            public string PicUrl { get; set; }
            public string Url { get; set; }
        }
        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<xml>");
            sb.Append("<ToUserName><![CDATA[" + ToUserName + "]]></ToUserName>");
            sb.Append("<FromUserName><![CDATA[" + FromUserName + "]]></FromUserName>");
            sb.Append("<CreateTime>" + CreateTime + "</CreateTime>");
            sb.Append("<MsgType><![CDATA[" + MsgType + "]]></MsgType>");
            sb.Append("<ArticleCount>" + ArticleCount + "</ArticleCount>");

            sb.Append("<Articles>");
            foreach (var item in Articles)
            {
                sb.Append("<item>");
                sb.Append("<Title><![CDATA[" + item.Title + "]]></Title>");
                sb.Append("<Description><![CDATA[" + item.Description + "]]></Description>");
                sb.Append("<PicUrl><![CDATA[" + item.PicUrl + "]]></PicUrl>");
                sb.Append("<Url><![CDATA[" + item.Url + "]]></Url>");
                sb.Append("</item>");
            }
            sb.Append("</Articles>");
            sb.Append("</xml>");
            return sb.ToString();
        }

    }
    public class MusicReplyMsg : WxReplyMsg
    {
        public MusicReplyMsg(WxMsg msg)
            : base()
        {
            MsgType = "music";
            FromUserName = msg.ToUserName;
            ToUserName = msg.FromUserName;
        }
        public MusicReplyMsg() : base() { MsgType = "music"; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string MusicURL { get; set; }
        public string HQMusicUrl { get; set; }
        public string ThumbMediaId { get; set; }



        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("<xml>");
            sb.Append("<ToUserName><![CDATA[" + ToUserName + "]]></ToUserName>");
            sb.Append("<FromUserName><![CDATA[" + FromUserName + "]]></FromUserName>");
            sb.Append("<CreateTime>" + CreateTime + "</CreateTime>");
            sb.Append("<MsgType><![CDATA[" + MsgType + "]]></MsgType>");

            sb.Append("<Music>");
            sb.Append("<Title><![CDATA[" + Title + "]]></Title>");
            sb.Append("<Description><![CDATA[" + Description + "]]></Description>");
            sb.Append("<MusicURL><![CDATA[" + MusicURL + "]]></MusicURL>");
            sb.Append("<HQMusicUrl><![CDATA[" + HQMusicUrl + "]]></HQMusicUrl>");
            sb.Append("<ThumbMediaId><![CDATA[" + ThumbMediaId + "]]></ThumbMediaId>");
            sb.Append("</Music>");

            sb.Append("</xml>");
            return sb.ToString();
        }
    }
}
