using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TG.Common.Weixin.Datas;

namespace TG.Common.Weixin
{
    public abstract class WxCallbackApi
    {
        /// <summary>
        /// 响应文本消息
        /// </summary>
        public event EventHandler<MsgReplyEventArgs<TextMsg>> OnTextMsg;
        /// <summary>
        /// 响应图片消息
        /// </summary>
        public event EventHandler<MsgReplyEventArgs<ImageMsg>> OnImageMsg;
        /// <summary>
        /// 响应语音消息
        /// </summary>
        public event EventHandler<MsgReplyEventArgs<VoiceMsg>> OnVoiceMsg;
        /// <summary>
        /// 响应视频消息
        /// </summary>
        public event EventHandler<MsgReplyEventArgs<VideoMsg>> OnVideoMsg;
        /// <summary>
        /// 响应坐标消息
        /// </summary>
        public event EventHandler<MsgReplyEventArgs<LocationMsg>> OnLocationMsg;//location
        /// <summary>
        /// 响应链接消息
        /// </summary>
        public event EventHandler<MsgReplyEventArgs<LinkMsg>> OnLinkMsg;
        /// <summary>
        /// 响应未知消息
        /// </summary>
        public event EventHandler<MsgReplyEventArgs<WxMsg>> OnUnknownMsg;

        /// <summary>
        /// 响应订阅事件
        /// </summary>
        public event EventHandler<MsgReplyEventArgs<SubscribeEventMsg>> OnSubscribeEventMsg;//订阅
        /// <summary>
        /// 响应取消订阅事件
        /// </summary>
        public event EventHandler<MsgEventArgs<UnSubscribeEventMsg>> OnUnSubscribeEventMsg;//取消订阅
        /// <summary>
        /// 成员进入应用的事件推送
        /// </summary>
        public event EventHandler<MsgReplyEventArgs<EnterEventMsg>> OnEnterEventMsg;//成员进入应用的事件推送
        /// <summary>
        /// 上报地理位置事件
        /// </summary>
        public event EventHandler<MsgReplyEventArgs<LocationEventMsg>> OnLocationEventMsg;//上报地理位置事件
        /// <summary>
        /// 点击菜单拉取消息的事件推送
        /// </summary>
        public event EventHandler<MsgReplyEventArgs<ClickEventMsg>> OnClickEventMsg;//点击菜单拉取消息的事件推送
        /// <summary>
        /// 点击菜单跳转链接的事件推送
        /// </summary>
        public event EventHandler<MsgReplyEventArgs<ViewEventMsg>> OnViewEventMsg;//点击菜单跳转链接的事件推送
        /// <summary>
        /// 扫码推事件的事件推送
        /// </summary>
        public event EventHandler<MsgReplyEventArgs<ScancodePushEventMsg>> OnScancodePushEventMsg;//扫码推事件的事件推送
        /// <summary>
        /// 扫码推事件且弹出“消息接收中”提示框的事件推送
        /// </summary>
        public event EventHandler<MsgReplyEventArgs<ScancodeWaitmsgEventMsg>> OnScancodeWaitmsgEventMsg;//扫码推事件且弹出“消息接收中”提示框的事件推送
        /// <summary>
        /// 弹出系统拍照发图的事件推送
        /// 事件类型，pic_sysphoto
        /// 弹出拍照或者相册发图的事件推送
        /// 事件类型，pic_photo_or_album
        /// 弹出微信相册发图器的事件推送
        /// 事件类型，pic_weixin
        /// </summary>
        public event EventHandler<MsgReplyEventArgs<PicEventMsg>> OnPicEventMsg;//弹出拍照或者相册发图的事件推送
        /// <summary>
        /// 弹出地理位置选择器的事件推送
        /// </summary>
        public event EventHandler<MsgReplyEventArgs<LocationSelectEventMsg>> OnLocationSelectEventMsg;//弹出地理位置选择器的事件推送
        /// <summary>
        /// 异步任务完成事件推送
        /// </summary>
        public event EventHandler<MsgEventArgs<BatchJobResultEventMsg>> OnBatchJobResultEventMsg;//异步任务完成事件推送
        /// <summary>
        /// 事件推送群发结果
        /// </summary>
        public event EventHandler<MsgEventArgs<MasssEndjobFinishEventMsg>> OnMasssEndjobFinishEventMsg;//事件推送群发结果
        /// <summary>
        /// 在模版消息发送任务完成后
        /// </summary>
        public event EventHandler<MsgEventArgs<TemplateSendJobfinishEventMsg>> OnTemplateSendJobfinishEventMsg;//在模版消息发送任务完成后，微信服务器会将是否送达成功作为通知，发送到开发者中心中填写的服务器配置地址中。
        /// <summary>
        /// 未知事件
        /// </summary>
        public event EventHandler<MsgReplyEventArgs<WxEventMsg>> OnUnknownEventMsg;//


        protected abstract bool HasEncryption();
        protected abstract string GetAppID();
        protected abstract string GetToken();
        protected abstract string GetEncodingAESKey();

        #region 私有变量
        protected string DecryptMsg(string msg)
        {
            var Request = System.Web.HttpContext.Current.Request;
            string signature = Request.QueryString["msg_signature"];//企业号的 msg_signature
            string timestamp = Request.QueryString["timestamp"];
            string nonce = Request.QueryString["nonce"];
            Tencent.WXBizMsgCrypt wxcpt = new Tencent.WXBizMsgCrypt(GetToken(), GetEncodingAESKey(), GetAppID());
            string smag = "";
            wxcpt.DecryptMsg(signature, timestamp, nonce, msg, ref smag);
            return smag;
        }
        protected string EncryptMsg(string msg)
        {
            Random rand = new Random();
            var sReqNonce = rand.Next(99999999).ToString();
            var sReqTimeStamp = GetTimeStamp();

            Tencent.WXBizMsgCrypt wxcpt = new Tencent.WXBizMsgCrypt(GetToken(), GetEncodingAESKey(), GetAppID());
            string sEncryptMsg = ""; //xml格式的密文
            wxcpt.EncryptMsg(msg, sReqTimeStamp, sReqNonce, ref sEncryptMsg);
            return sEncryptMsg;
        }
        protected string GetTimeStamp()
        {
            TimeSpan ts = DateTime.UtcNow - new DateTime(1970, 1, 1, 0, 0, 0, 0);
            return Convert.ToInt64(ts.TotalSeconds).ToString();
        }
        #endregion

        protected virtual string Auth()
        {
            var Request = System.Web.HttpContext.Current.Request;
            if (HasEncryption())
            {
                string echoString = Request.QueryString["echoStr"];
                string signature = Request.QueryString["msg_signature"];//企业号的 msg_signature
                string timestamp = Request.QueryString["timestamp"];
                string nonce = Request.QueryString["nonce"];

                Tencent.WXBizMsgCrypt wxcpt = new Tencent.WXBizMsgCrypt(GetToken(), GetEncodingAESKey(), GetAppID());
                string decryptEchoString = "";
                wxcpt.VerifyURL(signature, timestamp, nonce, echoString, ref decryptEchoString);
                return decryptEchoString;
            }
            else
            {
                string echoStr = Request.QueryString["echoStr"];
                return echoStr;
            }
        }

        protected virtual string ReadContent()
        {
            if (HasEncryption())
            {
                Stream s = System.Web.HttpContext.Current.Request.InputStream;
                StreamReader sr = new StreamReader(s, Encoding.UTF8);
                var content = DecryptMsg(sr.ReadToEnd());
                sr.Dispose();
                return content;
            }
            Stream s2 = System.Web.HttpContext.Current.Request.InputStream;
            StreamReader sr2 = new StreamReader(s2, Encoding.UTF8);
            var content2 = sr2.ReadToEnd();
            sr2.Dispose();
            return content2;
        }

        public string Start()
        {
            var Request = System.Web.HttpContext.Current.Request;
            if (Request.RequestType.ToUpper() == "GET") return Auth();
            var text = ReadContent();
            return Execute(text);
        }
        protected virtual string Execute(string content)
        {
            var t = WxMsg.Deserialize(content);
            WxReplyMsg replyMsg = null;
            switch (t.GetType().Name)
            {
                case "TextMsg": replyMsg = DoMsg(OnTextMsg, (TextMsg)t); break;
                case "ImageMsg": replyMsg = DoMsg(OnImageMsg, (ImageMsg)t); break;
                case "VoiceMsg": replyMsg = DoMsg(OnVoiceMsg, (VoiceMsg)t); break;
                case "VideoMsg": replyMsg = DoMsg(OnVideoMsg, (VideoMsg)t); break;
                case "LocationMsg": replyMsg = DoMsg(OnLocationMsg, (LocationMsg)t); break;
                case "LinkMsg": replyMsg = DoMsg(OnLinkMsg, (LinkMsg)t); break;

                case "SubscribeEventMsg": replyMsg = DoMsg(OnSubscribeEventMsg, (SubscribeEventMsg)t); break;
                case "UnSubscribeEventMsg": replyMsg = DoMsg(OnUnSubscribeEventMsg, (UnSubscribeEventMsg)t); break;
                case "EnterEventMsg": replyMsg = DoMsg(OnEnterEventMsg, (EnterEventMsg)t); break;
                case "LocationEventMsg": replyMsg = DoMsg(OnLocationEventMsg, (LocationEventMsg)t); break;
                case "ClickEventMsg": replyMsg = DoMsg(OnClickEventMsg, (ClickEventMsg)t); break;
                case "ViewEventMsg": replyMsg = DoMsg(OnViewEventMsg, (ViewEventMsg)t); break;
                case "ScancodePushEventMsg": replyMsg = DoMsg(OnScancodePushEventMsg, (ScancodePushEventMsg)t); break;
                case "ScancodeWaitmsgEventMsg": replyMsg = DoMsg(OnScancodeWaitmsgEventMsg, (ScancodeWaitmsgEventMsg)t); break;
                case "PicEventMsg": replyMsg = DoMsg(OnPicEventMsg, (PicEventMsg)t); break;
                case "LocationSelectEventMsg": replyMsg = DoMsg(OnLocationSelectEventMsg, (LocationSelectEventMsg)t); break;
                case "BatchJobResultEventMsg": replyMsg = DoMsg(OnBatchJobResultEventMsg, (BatchJobResultEventMsg)t); break;
                case "MasssEndjobFinishEventMsg": replyMsg = DoMsg(OnMasssEndjobFinishEventMsg, (MasssEndjobFinishEventMsg)t); break;
                case "TemplateSendJobfinishEventMsg": replyMsg = DoMsg(OnTemplateSendJobfinishEventMsg, (TemplateSendJobfinishEventMsg)t); break;

                case "WxMsg": replyMsg = DoMsg(OnUnknownMsg, (WxMsg)t); break;
                case "WxEventMsg": replyMsg = DoMsg(OnUnknownEventMsg, (WxEventMsg)t); break;
                default: break;
            }

            if (replyMsg == null) return "";
            return EncryptMsg(replyMsg.ToString());
        }

        protected virtual WxReplyMsg DoMsg<T>(EventHandler<MsgReplyEventArgs<T>> handle, T msg) where T : WxMsg
        {
            if (handle != null)
            {
                var eventagrs = new MsgReplyEventArgs<T>(msg);
                handle(this, eventagrs);
                return eventagrs.ReplyMsg;
            }
            return null;
        }
        protected virtual WxReplyMsg DoMsg<T>(EventHandler<MsgEventArgs<T>> handle, T msg) where T : WxMsg
        {
            if (handle != null)
            {
                var eventagrs = new MsgEventArgs<T>(msg);
                handle(this, eventagrs);
            }
            return null;
        }
    }
    public class MsgReplyEventArgs<T> : EventArgs where T : WxMsg
    {
        public T Msg { get; private set; }
        public WxReplyMsg ReplyMsg { get; set; }
        public MsgReplyEventArgs(T t)
        {
            Msg = t;
        }
    }
    public class MsgEventArgs<T> : EventArgs where T : WxMsg
    {
        public T Msg { get; private set; }
        public MsgEventArgs(T t)
        {
            Msg = t;
        }
    }


}
