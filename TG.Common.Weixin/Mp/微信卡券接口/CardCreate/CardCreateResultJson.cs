/*----------------------------------------------------------------
    Copyright (C) 2015 Senparc
    
    文件名：CardCreateResultJson.cs
    文件功能描述：创建卡券返回结果
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
    
    修改标识：Senparc - 20150323
    修改描述：添加上传logo返回结果
----------------------------------------------------------------*/

using System.Collections.Generic;

namespace TG.Common.Weixin.Mp.Datas
{
    /// <summary>
    /// 创建卡券返回结果
    /// </summary>
    public class CardCreateResultJson : JsonResult
    {
        /// <summary>
        /// 卡券ID
        /// </summary>
       public string card_id { get; set; } 
    }

    /// <summary>
    /// 获取颜色列表返回结果
    /// </summary>
    public class GetColorsResultJson : JsonResult
    {
        /// <summary>
        /// 颜色列表
        /// </summary>
        public List<Card_Color> colors { get; set; }
    }
    
    public class Card_Color
    {
        /// <summary>
        /// 可以填入的color 名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 对应的颜色数值
        /// </summary>
        public string value { get; set; }
    }
    /// <summary>
    /// 生成卡券二维码返回结果
    /// </summary>
    public class CreateQRResultJson : JsonResult
    {
        /// <summary>
        /// 获取的二维码ticket，凭借此ticket 可以在有效时间内换取二维码。
        /// </summary>
        public string ticket { get; set; }
    }

    /// <summary>
    /// 消耗code返回结果
    /// </summary>
    public class CardConsumeResultJson : JsonResult
    {
        public CardId card { get; set; }
        /// <summary>
        /// 用户openid
        /// </summary>
        public string openid { get; set; }
    }

    public class CardId
    {
        /// <summary>
        /// 卡券ID
        /// </summary>
        public string card_id { get; set; }
    }

    /// <summary>
    /// code 解码
    /// </summary>
    public class CardDecryptResultJson : JsonResult
    {
        public string code { get; set; }
    }

    /// <summary>
    /// 上传logo返回结果
    /// </summary>
    public class Card_UploadLogoResultJson : JsonResult
    {
        public string url { get; set; }
    }
}
