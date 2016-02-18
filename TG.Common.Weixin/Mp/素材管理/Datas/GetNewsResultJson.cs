/*----------------------------------------------------------------
    Copyright (C) 2015 Senparc
    
    文件名：GetNewsResultJson.cs
    文件功能描述：获取图文类型永久素材返回结果
    
    
    创建标识：Senparc - 20150324
----------------------------------------------------------------*/

using System.Collections.Generic;

namespace TG.Common.Weixin.Mp.Datas
{
    /// <summary>
    /// 获取图文类型永久素材返回结果
    /// </summary>
    public class GetNewsResultJson : JsonResult
    {
        public List<ForeverNewsItem> news_item { get; set; }
    }

    public class ForeverNewsItem : NewsModel
    {
        /// <summary>
        /// 图文页的URL
        /// </summary>
        public string url { get; set; }
    }
}
