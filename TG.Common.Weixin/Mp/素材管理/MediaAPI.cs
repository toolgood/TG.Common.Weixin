/*----------------------------------------------------------------
    Copyright (C) 2015 Senparc
    
    文件名：MediaAPI.cs
    文件功能描述：素材管理接口（原多媒体文件接口）
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
 
    修改标识：Senparc - 20150312
    修改描述：开放代理请求超时时间
 
    修改标识：Senparc - 20150321
    修改描述：变更为素材管理接口
 
    修改标识：Senparc - 20150401
    修改描述：上传临时图文消息接口
 
    修改标识：Senparc - 20150407
    修改描述：上传永久视频接口修改
----------------------------------------------------------------*/

/*
    接口详见：http://mp.weixin.qq.com/wiki/index.php?title=%E4%B8%8A%E4%BC%A0%E4%B8%8B%E8%BD%BD%E5%A4%9A%E5%AA%92%E4%BD%93%E6%96%87%E4%BB%B6
 */

using TG.Common.Weixin.Mp;
using TG.Common.Weixin.Mp.Datas;
using System.Collections.Generic;
using System.IO;

namespace TG.Common.Weixin.Mp
{
    /// <summary>
    /// 素材管理接口（原多媒体文件接口）
    /// </summary>
    public class MediaApi : MpApi
    {
        public MediaApi(WxMpApi api) : base(api) { }

        #region 临时素材
        /// <summary>
        /// 新增临时素材（原上传媒体文件）
        /// </summary>
        /// <param name="type"></param>
        /// <param name="file"></param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public UploadTemporaryMediaResult UploadTemporaryMedia( UploadMediaFileType type, string file, int timeOut = Config.TIME_OUT)
        {
            var accessToken = _api.GetAccessToken();
            var url = string.Format("http://api.weixin.qq.com/cgi-bin/media/upload?access_token={0}&type={1}", accessToken, type.ToString());
            return PostFile<UploadTemporaryMediaResult>(url, file, timeOut);
        }

        /// <summary>
        /// 上传临时图文消息素材（原上传图文消息素材）
        /// </summary>
        /// <param name="news">图文消息组</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public UploadTemporaryMediaResult UploadTemporaryNews(int timeOut = Config.TIME_OUT, params NewsModel[] news)
        {
            var accessToken = _api.GetAccessToken();
            string url = string.Format("https://api.weixin.qq.com/cgi-bin/media/uploadnews?access_token={0}", accessToken);
            var data = new
            {
                articles = news
            };
            return Post<UploadTemporaryMediaResult>(url, data, timeOut);
        }

        /// <summary>
        /// 获取临时素材（原下载媒体文件）
        /// </summary>
        /// <param name="mediaId"></param>
        public DownloadFileInfo Get(string mediaId)
        {
            var accessToken = _api.GetAccessToken();
            var url = string.Format("http://api.weixin.qq.com/cgi-bin/media/get?access_token={0}&media_id={1}",
                accessToken, mediaId);
            return GetFile(url);
        }
        #endregion

        #region 永久素材
        /*
         1、新增的永久素材也可以在公众平台官网素材管理模块中看到
         2、永久素材的数量是有上限的，请谨慎新增。图文消息素材和图片素材的上限为5000，其他类型为1000
         3、调用该接口需https协议
         */

        /// <summary>
        /// 新增永久图文素材
        /// </summary>
        /// <param name="news">图文消息组</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public UploadForeverMediaResult UploadNews(int timeOut = Config.TIME_OUT, params NewsModel[] news)
        {
            var accessToken = _api.GetAccessToken();
            var url = string.Format("https://api.weixin.qq.com/cgi-bin/material/add_news?access_token={0}", accessToken);
            var data = new
            {
                articles = news
            };
            return Post<UploadForeverMediaResult>(url, data, timeOut);
        }

        /// <summary>
        /// 新增其他类型永久素材(图片（image）、语音（voice）和缩略图（thumb）)
        /// </summary>
        /// <param name="file">文件路径</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public UploadForeverMediaResult UploadForeverMedia(string file, int timeOut = Config.TIME_OUT)
        {
            var accessToken = _api.GetAccessToken();
            var url = string.Format("https://api.weixin.qq.com/cgi-bin/material/add_material?access_token={0}", accessToken);
            return PostFile<UploadForeverMediaResult>(url, file, timeOut);
        }

        /// <summary>
        /// 新增永久视频素材
        /// </summary>
        /// <param name="file">文件路径</param>
        /// <param name="title"></param>
        /// <param name="introduction"></param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public UploadForeverMediaResult UploadForeverVideo(string file, string title, string introduction, int timeOut = 40000)
        {
            var accessToken = _api.GetAccessToken();
            var url = string.Format("https://api.weixin.qq.com/cgi-bin/material/add_material?access_token={0}", accessToken);
            var data = new
            {
                media = Path.GetFileName(file),
                title = title,
                introduction = introduction,
            };
            return PostFile<UploadForeverMediaResult>(url, file, data, timeOut);
        }

        /// <summary>
        /// 获取永久图文素材
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="mediaId"></param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public GetNewsResultJson GetForeverNews(string mediaId, int timeOut = Config.TIME_OUT)
        {
            var accessToken = _api.GetAccessToken();
            string url = string.Format("https://api.weixin.qq.com/cgi-bin/material/get_material?access_token={0}", accessToken);
            var data = new
            {
                media_id = mediaId
            };
            return Post<GetNewsResultJson>(url, data, timeOut);
        }

        /// <summary>
        /// 获取永久素材(除了图文)
        /// </summary>
        /// <param name="accessToken"></param>
        /// <param name="mediaId"></param>
        /// <param name="stream"></param>
        public string GetForeverMedia(string mediaId)
        {
            var accessToken = _api.GetAccessToken();
            var url = string.Format("https://api.weixin.qq.com/cgi-bin/material/get_material?access_token={0}", accessToken);
            var data = new
            {
                media_id = mediaId
            };
            return Post<string>(url, data);
        }

        /// <summary>
        /// 删除永久素材
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="mediaId"></param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public JsonResult DeleteForeverMedia(string mediaId, int timeOut = Config.TIME_OUT)
        {
            var accessToken = _api.GetAccessToken();
            string url = string.Format("https://api.weixin.qq.com/cgi-bin/material/del_material?access_token={0}", accessToken);
            var data = new
            {
                media_id = mediaId
            };
            return Post<JsonResult>(url, data, timeOut);
        }

        /// <summary>
        /// 修改永久图文素材
        /// </summary>
        /// <param name="mediaId">要修改的图文消息的id</param>
        /// <param name="index">要更新的文章在图文消息中的位置（多图文消息时，此字段才有意义），第一篇为0</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <param name="news">图文素材</param>
        /// <returns></returns>
        public  JsonResult UpdateForeverNews( string mediaId, int? index, NewsModel news, int timeOut = Config.TIME_OUT)
        {
            var accessToken = _api.GetAccessToken();
            string url = string.Format("https://api.weixin.qq.com/cgi-bin/material/update_news?access_token={0}", accessToken);
            var data = new
            {
                media_id = mediaId,
                index = index,
                articles = news
            };
            return Post<JsonResult>(url, data, timeOut);
        }

        /// <summary>
        /// 获取素材总数
        /// 永久素材的总数，也会计算公众平台官网素材管理中的素材
        /// 图片和图文消息素材（包括单图文和多图文）的总数上限为5000，其他素材的总数上限为1000
        /// </summary>
        /// <returns></returns>
        public GetMediaCountResultJson GetMediaCount( )
        {
            var accessToken = _api.GetAccessToken();
            string url = string.Format("https://api.weixin.qq.com/cgi-bin/material/get_materialcount?access_token={0}", accessToken);
            return Get<GetMediaCountResultJson>(url);
        }

        /// <summary>
        /// 获取图文素材列表
        /// </summary>
        /// <param name="offset">从全部素材的该偏移位置开始返回，0表示从第一个素材 返回</param>
        /// <param name="count">返回素材的数量，取值在1到20之间</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public  MediaList_NewsResult GetNewsMediaList( int offset, int count, int timeOut = Config.TIME_OUT)
        {
            var accessToken = _api.GetAccessToken();
            string url = string.Format("https://api.weixin.qq.com/cgi-bin/material/batchget_material?access_token={0}", accessToken);
            var date = new
            {
                type = "news",
                offset = offset,
                count = count
            };
            return Post<MediaList_NewsResult>(url, date, timeOut);
        }

        /// <summary>
        /// 获取图片、视频、语音素材列表
        /// </summary>
        /// <param name="type">素材的类型，图片（image）、视频（video）、语音 （voice）</param>
        /// <param name="offset"></param>
        /// <param name="count"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public  MediaList_OthersResult GetOthersMediaList( UploadMediaFileType type, int offset, int count, int timeOut = Config.TIME_OUT)
        {
            var accessToken = _api.GetAccessToken();
            string url = string.Format("https://api.weixin.qq.com/cgi-bin/material/batchget_material?access_token={0}",                                accessToken);
            var date = new
            {
                type = type.ToString(),
                offset = offset,
                count = count
            };
            return Post<MediaList_OthersResult>(url, date, timeOut);
        }

        /// <summary>
        /// 上传图文消息内的图片获取URL
        /// </summary>
        /// <param name="accessTokenOrAppId"></param>
        /// <param name="file"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public UploadImgResult UploadImg(string file, int timeOut = Config.TIME_OUT)
        {
            var accessToken = _api.GetAccessToken();
            var url = string.Format("https://api.weixin.qq.com/cgi-bin/media/uploadimg?access_token={0}", accessToken);
            var data = new
            {
                media = Path.GetFileName(file)
            };
            return PostFile<UploadImgResult>(url, file, data, timeOut);
        }

        #endregion
    }
}


namespace TG.Common.Weixin
{
    partial class WxMpApi
    {
        private MediaApi _MediaApi;
        public MediaApi MediaApi
        {
            get
            {
                if (_MediaApi==null)
                {
                    _MediaApi= new MediaApi(this);
                }
                return _MediaApi;
            }
        }

    }
}