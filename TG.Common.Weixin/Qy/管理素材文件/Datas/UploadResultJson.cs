using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TG.Common.Weixin.Qy.Datas
{
    /// <summary>
    /// 上传临时媒体文件返回结果
    /// </summary>
    public class UploadTemporaryResultJson : JsonResult
    {
        public UploadMediaFileType type { get; set; }
        public string media_id { get; set; }
        public long created_at { get; set; }
    }

    /// <summary>
    /// 上传永久素材返回结果
    /// </summary>
    public class UploadForeverResultJson : JsonResult
    {
        public string media_id { get; set; }
    }
    public enum UploadMediaFileType
    {
        /// <summary>
        /// 图片: 1MB，支持JPG格式
        /// </summary>
        image,
        /// <summary>
        /// 语音：2MB，播放长度不超过60s，支持AMR格式
        /// </summary>
        voice,
        /// <summary>
        /// 视频：10MB，支持MP4格式
        /// </summary>
        video,
        /// <summary>
        /// 普通文件：10MB
        /// </summary>
        file
    }
}
