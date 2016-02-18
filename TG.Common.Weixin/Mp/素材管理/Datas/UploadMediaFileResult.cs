/*----------------------------------------------------------------
    Copyright (C) 2015 Senparc
    
    文件名：UploadMediaFileResult.cs
    文件功能描述：上传媒体文件返回结果
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
    
    修改标识：Senparc - 20150320
    修改描述：修改结果类型（有临时和永久之分）
----------------------------------------------------------------*/


namespace TG.Common.Weixin.Mp.Datas
{
    /// <summary>
    /// 上传临时媒体文件返回结果
    /// </summary>
    public class UploadTemporaryMediaResult : JsonResult
    {
        public UploadMediaFileType type { get; set; }
        public string media_id { get; set; }
        /// <summary>
        /// 上传缩略图返回的meidia_id参数.
        /// </summary>
        public string thumb_media_id { get; set; }
        public long created_at { get; set; }
    }

    /// <summary>
    /// 上传永久媒体文件返回结果
    /// </summary>
    public class UploadForeverMediaResult : JsonResult
    {
        /// <summary>
        /// 新增的永久素材的media_id
        /// </summary>
        public string media_id { get; set; }

        /// <summary>
        /// 新增的图片素材的图片URL（仅新增图片素材时会返回该字段）
        /// </summary>
        public string url { get; set; }
    }

    /// <summary>
    /// 上传图文消息内的图片获取URL返回结果
    /// </summary>
    public class UploadImgResult : JsonResult
    {
        public string url { get; set; }
    }
    public enum UploadMediaFileType
    {
        /// <summary>
        /// 图片: 128K，支持JPG格式
        /// </summary>
        image,
        /// <summary>
        /// 语音：256K，播放长度不超过60s，支持AMR\MP3格式
        /// </summary>
        voice,
        /// <summary>
        /// 视频：1MB，支持MP4格式
        /// </summary>
        video,
        /// <summary>
        /// thumb：64KB，支持JPG格式
        /// </summary>
        thumb,
        /// <summary>
        /// 图文消息
        /// </summary>
        news
    }
}
