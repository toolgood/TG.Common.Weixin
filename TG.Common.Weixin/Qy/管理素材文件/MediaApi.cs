using TG.Common.Weixin.Qy;
using TG.Common.Weixin.Qy.Datas;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TG.Common.Weixin.Qy
{
    public class MediaApi : QyApi
    {
        public MediaApi(WxCorpApi api) : base(api) { }
        /// <summary>
        /// 上传临时媒体文件
        /// </summary>
        /// <param name="type">媒体文件类型，分别有图片（image）、语音（voice）、视频（video），普通文件(file)</param>
        /// <param name="filePath">form-data中媒体文件标识，有filename、filelength、content-type等信息</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public UploadTemporaryResultJson Upload(UploadMediaFileType type, string filePath, int timeOut = Config.TIME_OUT)
        {
            var accessToken = _api.GetAccessToken();
            var url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/media/upload?access_token={0}&type={1}", accessToken, type.ToString());
            return PostFile<UploadTemporaryResultJson>(url, filePath, timeOut);
        }

        /// <summary>
        /// 获取临时媒体文件
        /// </summary>
        /// <param name="mediaId"></param>
        /// <param name="stream"></param>
        public DownloadFileInfo GetMedia(string mediaId)
        {
            var accessToken = _api.GetAccessToken();
            var url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/media/get?access_token={0}&media_id={1}",
                accessToken, mediaId);
            return GetFile(url, 30 * 1000);
        }

        /// <summary>
        /// 上传永久图文素材
        /// </summary>
        /// <param name="agentId"></param>
        /// <param name="timeOut"></param>
        /// <param name="mpNewsArticles"></param>
        /// <returns></returns>
        public UploadForeverResultJson AddMpNews(int timeOut = Config.TIME_OUT, params MpNewsArticle[] mpNewsArticles)
        {
            var accessToken = _api.GetAccessToken();
            var agentId = _api.GetAgentId();
            var url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/material/add_mpnews?access_token={0}", accessToken);
            var data = new
            {
                agentid = agentId,
                mpnews = new
                {
                    articles = mpNewsArticles
                }
            };
            return Post<UploadForeverResultJson>(url, data, timeOut);
        }

        /// <summary>
        /// 上传其他类型永久素材
        /// </summary>
        /// <param name="type"></param>
        /// <param name="agentId"></param>
        /// <param name="media"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public UploadForeverResultJson AddMaterial(UploadMediaFileType type, string media, int timeOut = Config.TIME_OUT)
        {
            var agentId = _api.GetAgentId();
            var accessToken = _api.GetAccessToken();
            var url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/material/add_material?agentid={1}&type={2}&access_token={0}", accessToken, agentId, type);
            return PostFile<UploadForeverResultJson>(url, media, timeOut);
        }

        /// <summary>
        /// 获取永久图文素材
        /// </summary>
        /// <param name="agentId"></param>
        /// <param name="mediaId"></param>
        /// <returns></returns>
        public GetForeverMpNewsResult GetForeverMpNews( string mediaId)
        {
            var agentId = _api.GetAgentId();
            var accessToken = _api.GetAccessToken();
            var url =
                string.Format(
                    "https://qyapi.weixin.qq.com/cgi-bin/material/get?access_token={0}&media_id={1}&agentid={2}",
                    accessToken, mediaId, agentId);
            return Get<GetForeverMpNewsResult>(url);
        }

        /// <summary>
        /// 获取临时媒体文件
        /// </summary>
        /// <param name="agentId"></param>
        /// <param name="mediaId"></param>
        /// <param name="stream"></param>
        public DownloadFileInfo GetForeverMaterial( string mediaId)
        {
            var agentId = _api.GetAgentId();
            var accessToken = _api.GetAccessToken();
            var url =
                string.Format(
                    "https://qyapi.weixin.qq.com/cgi-bin/material/get?access_token={0}&media_id={1}&agentid={2}",
                    accessToken, mediaId, agentId);
            return GetFile(url, 30 * 1000);
        }

        /// <summary>
        /// 删除永久素材
        /// </summary>
        /// <param name="agentId"></param>
        /// <param name="mediaId"></param>
        /// <returns></returns>
        public JsonResult DeleteForeverMaterial( string mediaId)
        {
            var agentId = _api.GetAgentId();
            var accessToken = _api.GetAccessToken();
            var url =
                string.Format(
                    "https://qyapi.weixin.qq.com/cgi-bin/material/del?access_token={0}&agentid={1}&media_id={2}}",
                    accessToken, agentId, mediaId);
            return Get<JsonResult>(url, 30 * 1000);
        }

        /// <summary>
        /// 修改永久图文素材
        /// </summary>
        /// <param name="mediaId"></param>
        /// <param name="agentId"></param>
        /// <param name="timeOut"></param>
        /// <param name="mpNewsArticles"></param>
        /// <returns></returns>
        public UploadForeverResultJson UpdateMpNews(string mediaId,  int timeOut = Config.TIME_OUT, params MpNewsArticle[] mpNewsArticles)
        {
            var agentId = _api.GetAgentId();
            var accessToken = _api.GetAccessToken();
            var url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/material/update_mpnews?access_token={0}", accessToken);
            var data = new
            {
                agentid = agentId,
                media_id = mediaId,
                mpnews = new
                {
                    articles = mpNewsArticles
                }
            };
            return Post<UploadForeverResultJson>(url, data, timeOut);
        }

        /// <summary>
        /// 获取素材总数
        /// </summary>
        /// <param name="agentId"></param>
        /// <returns></returns>
        public GetCountResult GetCount()
        {
            var agentId = _api.GetAgentId();
            var accessToken = _api.GetAccessToken();
            var url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/material/get_count?access_token={0}&agentid={1}", accessToken, agentId);
            return Get<GetCountResult>(url, 30 * 1000);
        }

        /// <summary>
        /// 获取素材列表
        /// </summary>
        /// <param name="type"></param>
        /// <param name="agentId"></param>
        /// <param name="offset"></param>
        /// <param name="count"></param>
        /// <param name="timeOut"></param>
        /// <returns></returns>
        public BatchGetMaterialResult BatchGetMaterial(UploadMediaFileType type, int offset, int count, int timeOut = Config.TIME_OUT)
        {
            var agentId = _api.GetAgentId();
            var accessToken = _api.GetAccessToken();
            var url = string.Format("https://qyapi.weixin.qq.com/cgi-bin/material/batchget?access_token={0}", accessToken);

            var data = new
            {
                type = type.ToString(),
                agentid = agentId,
                offset = offset,
                count = count,
            };
            return Post<BatchGetMaterialResult>(url, data, timeOut);
        }


    }
}
namespace TG.Common.Weixin
{
    partial class WxCorpApi
    {
        private MediaApi _MediaApi;
        /// <summary>
        /// 管理素材文件
        /// </summary>
        public MediaApi MediaApi
        {
            get
            {
                if (_MediaApi == null)
                {
                    _MediaApi = new MediaApi(this);
                }
                return _MediaApi;
            }
        }
    }
}
