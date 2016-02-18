using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace TG.Common.Weixin.Datas
{
    public class WxMsg
    {
        /// <summary>
        /// 企业号CorpID
        /// </summary>
        public string ToUserName { get; set; }
        /// <summary>
        /// 成员UserID
        /// </summary>
        public string FromUserName { get; set; }
        /// <summary>
        /// 消息创建时间（整型）
        /// </summary>
        public long CreateTime { get; set; }
        /// <summary>
        /// 消息类型
        /// </summary>
        public string MsgType { get; set; }
        /// <summary>
        /// 消息id，64位整型
        /// </summary>
        public long MsgId { get; set; }
        /// <summary>
        /// 企业应用的id，整型。可在应用的设置页面查看
        /// </summary>
        public long AgentID { get; set; }
        public class xml : WxMsg { }
        public static WxMsg Deserialize(string msg)
        {
            xml xml = Deserialize(msg, typeof(xml)) as xml;
            switch (xml.MsgType.ToLower())
            {
                case "text": return TextMsg.Deserialize(msg);
                case "image": return ImageMsg.Deserialize(msg);
                case "voice": return VoiceMsg.Deserialize(msg);
                case "video": return VideoMsg.Deserialize(msg);
                case "shortvideo": return VoiceMsg.Deserialize(msg);
                case "location": return LocationMsg.Deserialize(msg);
                case "link": return LinkMsg.Deserialize(msg);
                case "event": return WxEventMsg.Deserialize(msg);
                default: break;
            }
            return xml;
        }
        protected static object Deserialize(string xml, Type type)
        {
            using (MemoryStream reader = new MemoryStream(Encoding.UTF8.GetBytes(xml)))
            {
                System.Xml.Serialization.XmlSerializer xmlSerializer = new System.Xml.Serialization.XmlSerializer(type);
                var result = xmlSerializer.Deserialize(reader);
                return result;
            }
        }

    }
    /// <summary>
    /// 消息类型，此时固定为：text
    /// </summary>
    public class TextMsg : WxMsg
    {
        /// <summary>
        /// 文本消息内容
        /// </summary>
        public string Content { get; set; }
        public class xml : TextMsg { }
        public static TextMsg Deserialize(string msg)
        {
            xml xml = Deserialize(msg, typeof(xml)) as xml;
            return xml;
        }

    }

    /// <summary>
    ///  消息类型，此时固定为：image
    /// </summary>
    public class ImageMsg : WxMsg
    {
        /// <summary>
        /// 图片链接
        /// </summary>
        public string PicUrl { get; set; }
        /// <summary>
        /// 图片媒体文件id，可以调用获取媒体文件接口拉取数据
        /// </summary>
        public string MediaId { get; set; }

        public class xml : ImageMsg { }
        public static ImageMsg Deserialize(string msg)
        {
            xml xml = Deserialize(msg, typeof(xml)) as xml;
            return xml;
        }

    }
    /// <summary>
    /// 消息类型，此时固定为：voice
    /// </summary>
    public class VoiceMsg : WxMsg
    {
        /// <summary>
        /// 语音媒体文件id，可以调用获取媒体文件接口拉取数据
        /// </summary>
        public string MediaId { get; set; }
        /// <summary>
        /// 语音格式，如amr，speex等
        /// </summary>
        public string Format { get; set; }

        public class xml : VoiceMsg { }
        public static VoiceMsg Deserialize(string msg)
        {
            xml xml = Deserialize(msg, typeof(xml)) as xml;
            return xml;
        }
    }
    /// <summary>
    /// 消息类型，此时固定为：video
    /// </summary>
    public class VideoMsg : WxMsg
    {
        public string MediaId { get; set; }
        public string ThumbMediaId { get; set; }

        public class xml : VideoMsg { }
        public static VideoMsg Deserialize(string msg)
        {
            xml xml = Deserialize(msg, typeof(xml)) as xml;
            return xml;
        }

    }

    /// <summary>
    /// 消息类型，此时固定为：location
    /// </summary>
    public class LocationMsg : WxMsg
    {
        public double Location_X { get; set; }
        public double Location_Y { get; set; }
        public int Scale { get; set; }
        public string Label { get; set; }

        public class xml : LocationMsg { }
        public static LocationMsg Deserialize(string msg)
        {
            xml xml = Deserialize(msg, typeof(xml)) as xml;
            return xml;
        }


    }

    /// <summary>
    /// 成员关注/取消关注事件
    /// 事件类型，subscribe(订阅)、unsubscribe(取消订阅)
    /// 成员进入应用的事件推送
    /// 事件类型，enter_agent
    /// </summary>
    public class WxEventMsg : WxMsg
    {
        /// <summary>
        /// 事件类型，subscribe(订阅)、unsubscribe(取消订阅) enter_agent(成员进入应用的事件推送)
        /// </summary>
        public string Event { get; set; }

        public class xml : WxEventMsg { }
        public new static WxEventMsg Deserialize(string msg)
        {
            xml xml = Deserialize(msg, typeof(xml)) as xml;
            switch (xml.Event.ToLower())
            {
                case "location": return LocationEventMsg.Deserialize(msg);
                case "click": return ClickEventMsg.Deserialize(msg);
                case "view": return ViewEventMsg.Deserialize(msg);
                case "scancode_push": return ScancodePushEventMsg.Deserialize(msg);
                case "scancode_waitmsg": return ScancodeWaitmsgEventMsg.Deserialize(msg);

                case "pic_sysphoto": return PicEventMsg.Deserialize(msg);
                case "pic_photo_or_album": return PicEventMsg.Deserialize(msg);
                case "pic_weixin": return PicEventMsg.Deserialize(msg);

                case "location_select": return LocationSelectEventMsg.Deserialize(msg);
                case "batch_job_result": return BatchJobResultEventMsg.Deserialize(msg);
                case "subscribe": return SubscribeEventMsg.Deserialize(msg);
                case "unsubscribe": return UnSubscribeEventMsg.Deserialize(msg);
                case "enter_agent": return EnterEventMsg.Deserialize(msg);
                case "scan": return ScanEventMsg.Deserialize(msg);
                case "media_id": return EnterEventMsg.Deserialize(msg);
                case "view_limited": return EnterEventMsg.Deserialize(msg);
                case "MASSSENDJOBFINISH":return MasssEndjobFinishEventMsg.Deserialize(msg);
                case "TEMPLATESENDJOBFINISH": return TemplateSendJobfinishEventMsg.Deserialize(msg);

                default: break;
            }
            return xml;
        }
    }
    /// <summary>
    /// 事件类型，subscribe(订阅)
    /// </summary>
    public class SubscribeEventMsg : WxEventMsg
    {
        public class xml : SubscribeEventMsg { }
        public static SubscribeEventMsg Deserialize(string msg)
        {
            xml xml = Deserialize(msg, typeof(xml)) as xml;
            return xml;
        }
    }

    /// <summary>
    /// 事件类型，unsubscribe(取消订阅)
    /// </summary>
    public class UnSubscribeEventMsg : WxEventMsg
    {
        public class xml : UnSubscribeEventMsg { }
        public static UnSubscribeEventMsg Deserialize(string msg)
        {
            xml xml = Deserialize(msg, typeof(xml)) as xml;
            return xml;
        }
    }
    /// <summary>
    /// enter_agent(成员进入应用的事件推送)
    /// </summary>
    public class EnterEventMsg : WxEventMsg
    {
        public class xml : EnterEventMsg { }
        public static EnterEventMsg Deserialize(string msg)
        {
            xml xml = Deserialize(msg, typeof(xml)) as xml;
            return xml;
        }
    }


    /// <summary>
    /// 上报地理位置事件
    /// 消息类型，此时固定为：event 事件类型，此时固定为：LOCATION
    /// </summary>
    public class LocationEventMsg : WxEventMsg
    {
        /// <summary>
        /// 地理位置纬度
        /// </summary>
        public double Latitude { get; set; }
        /// <summary>
        /// 地理位置经度
        /// </summary>
        public double Longitude { get; set; }
        /// <summary>
        /// 地理位置精度
        /// </summary>
        public double Precision { get; set; }

        public class xml : LocationEventMsg { }
        public static LocationEventMsg Deserialize(string msg)
        {
            xml xml = Deserialize(msg, typeof(xml)) as xml;
            return xml;
        }
    }
    /// <summary>
    /// 点击菜单拉取消息的事件推送
    /// 消息类型，此时固定为：
    /// event 事件类型，此时固定为：CLICK
    /// </summary>
    public class ClickEventMsg : WxEventMsg
    {
        /// <summary>
        /// 事件KEY值，与自定义菜单接口中KEY值对应
        /// </summary>
        public string EventKey { get; set; }

        public class xml : ClickEventMsg { }
        public static ClickEventMsg Deserialize(string msg)
        {
            xml xml = Deserialize(msg, typeof(xml)) as xml;
            return xml;
        }
    }
    /// <summary>
    /// 点击菜单跳转链接的事件推送
    /// 事件类型，VIEW
    /// </summary>
    public class ViewEventMsg : WxEventMsg
    {
        /// <summary>
        /// 事件KEY值，设置的跳转URL
        /// </summary>
        public string EventKey { get; set; }

        public class xml : ViewEventMsg { }
        public static ViewEventMsg Deserialize(string msg)
        {
            xml xml = Deserialize(msg, typeof(xml)) as xml;
            return xml;
        }
    }
    /// <summary>
    /// 扫码推事件的事件推送
    /// 事件类型，scancode_push
    /// </summary>
    public class ScancodePushEventMsg : WxEventMsg
    {
        /// <summary>
        /// 事件KEY值，由开发者在创建菜单时设定
        /// </summary>
        public string EventKey { get; set; }
        /// <summary>
        /// 扫描信息
        /// </summary>
        public string ScanCodeInfo { get; set; }
        /// <summary>
        /// 扫描类型，一般是qrcode
        /// </summary>
        public string ScanType { get; set; }
        /// <summary>
        /// 扫描结果，即二维码对应的字符串信息
        /// </summary>
        public string ScanResult { get; set; }

        public class xml : ScancodePushEventMsg { }
        public static ScancodePushEventMsg Deserialize(string msg)
        {
            xml xml = Deserialize(msg, typeof(xml)) as xml;
            return xml;
        }
    }
    /// <summary>
    /// 扫码推事件且弹出“消息接收中”提示框的事件推送
    /// 事件类型，scancode_waitmsg
    /// </summary>
    public class ScancodeWaitmsgEventMsg : WxEventMsg
    {
        /// <summary>
        /// 事件KEY值，由开发者在创建菜单时设定
        /// </summary>
        public string EventKey { get; set; }
        /// <summary>
        /// 扫描信息
        /// </summary>
        public string ScanCodeInfo { get; set; }
        /// <summary>
        /// 扫描类型，一般是qrcode
        /// </summary>
        public string ScanType { get; set; }
        /// <summary>
        /// 扫描结果，即二维码对应的字符串信息
        /// </summary>
        public string ScanResult { get; set; }

        public class xml : ScancodeWaitmsgEventMsg { }
        public static ScancodeWaitmsgEventMsg Deserialize(string msg)
        {
            xml xml = Deserialize(msg, typeof(xml)) as xml;
            return xml;
        }
    }
    /// <summary>
    /// 弹出系统拍照发图的事件推送
    /// 事件类型，pic_sysphoto
    /// 弹出拍照或者相册发图的事件推送
    /// 事件类型，pic_photo_or_album
    /// 弹出微信相册发图器的事件推送
    /// 事件类型，pic_weixin
    /// </summary>
    public class PicEventMsg : WxEventMsg
    {
        /// <summary>
        /// 事件KEY值，由开发者在创建菜单时设定
        /// </summary>
        public string EventKey { get; set; }
        /// <summary> 
        /// 发送的图片信息
        /// </summary>
        public SendPicsInfoClass SendPicsInfo { get; set; }
        public class xml : PicEventMsg { }
        public static PicEventMsg Deserialize(string msg)
        {
            xml xml = Deserialize(msg, typeof(xml)) as xml;
            return xml;
        }
        public class SendPicsInfoClass
        {
            /// <summary>
            /// 发送的图片数量
            /// </summary>
            public int Count { get; set; }
            /// <summary>
            /// 图片列表
            /// </summary>
            public PicListClass PicList { get; set; }

            public class PicListClass
            {
                public List<item1> item { get; set; }
                public class item1
                {
                    /// <summary>
                    /// 图片的MD5值，开发者若需要，可用于验证接收到图片
                    /// </summary>
                    public string PicMd5Sum { get; set; }
                }
            }
        }
    }
    /// <summary>
    /// 弹出地理位置选择器的事件推送
    /// 事件类型，location_select
    /// </summary>
    public class LocationSelectEventMsg : WxEventMsg
    {
        /// <summary>
        /// 事件KEY值，由开发者在创建菜单时设定
        /// </summary>
        public string EventKey { get; set; }
        /// <summary>
        /// 发送的位置信息
        /// </summary>
        public SendLocationInfoClass SendLocationInfo { get; set; }

        public class SendLocationInfoClass
        {
            /// <summary>
            /// X坐标信息
            /// </summary>
            public string Location_X { get; set; }
            /// <summary>
            /// Y坐标信息
            /// </summary>
            public string Location_Y { get; set; }
            /// <summary>
            /// 精度，可理解为精度或者比例尺、越精细的话 scale越高
            /// </summary>
            public string Scale { get; set; }
            /// <summary>
            /// 地理位置的字符串信息
            /// </summary>
            public string Label { get; set; }
            /// <summary>
            /// 朋友圈POI的名字，可能为空
            /// </summary>
            public string Poiname { get; set; }

        }

        public class xml : LocationSelectEventMsg { }
        public static LocationSelectEventMsg Deserialize(string msg)
        {
            xml xml = Deserialize(msg, typeof(xml)) as xml;
            return xml;
        }
    }
    /// <summary>
    /// 异步任务完成事件推送
    /// 事件的类型，此时固定为batch_job_result
    /// </summary>
    public class BatchJobResultEventMsg : WxEventMsg
    {
        public List<BatchJobClass> BatchJob { get; set; }

        public class BatchJobClass
        {
            /// <summary>
            /// 异步任务id，最大长度为64字符
            /// </summary>
            public string JobId { get; set; }
            /// <summary>
            /// 操作类型，字符串，目前分别有：
            /// 1. sync_user(增量更新成员)
            /// 2. replace_user(全量覆盖成员)
            /// 3. invite_user(邀请成员关注)
            /// 4. replace_party(全量覆盖部门)
            /// </summary>
            public string JobType { get; set; }
            /// <summary>
            /// 返回码
            /// </summary>
            public int ErrCode { get; set; }
            /// <summary>
            /// 对返回码的文本描述内容
            /// </summary>
            public string ErrMsg { get; set; }
        }

        public class xml : BatchJobResultEventMsg { }
        public static BatchJobResultEventMsg Deserialize(string msg)
        {
            xml xml = Deserialize(msg, typeof(xml)) as xml;
            return xml;
        }
    }


    /// <summary>
    /// link
    /// </summary>
    public class LinkMsg : WxMsg
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }

        public class xml : LinkMsg { }
        public static LinkMsg Deserialize(string msg)
        {
            xml xml = Deserialize(msg, typeof(xml)) as xml;
            return xml;
        }
    }

    public class ScanEventMsg : WxEventMsg
    {


        public class xml : ScanEventMsg { }
        public static ScanEventMsg Deserialize(string msg)
        {
            xml xml = Deserialize(msg, typeof(xml)) as xml;
            return xml;
        }
    }
    /// <summary>
    /// 事件推送群发结果
    /// </summary>
    public class MasssEndjobFinishEventMsg : WxEventMsg
    {
        public string Status { get; set; }
        public int TotalCount { get; set; }
        public int FilterCount { get; set; }
        public int SentCount { get; set; }
        public int ErrorCount { get; set; }
    }

    /// <summary>
    /// 在模版消息发送任务完成后，微信服务器会将是否送达成功作为通知，发送到开发者中心中填写的服务器配置地址中。
    /// </summary>
    public class TemplateSendJobfinishEventMsg: WxEventMsg
    {
        public string Status { get; set; }
    }



}