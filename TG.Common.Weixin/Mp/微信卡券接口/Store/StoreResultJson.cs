/*----------------------------------------------------------------
    Copyright (C) 2015 Senparc
    
    文件名：StoreResultJson.cs
    文件功能描述：卡券 门店相关接口返回结果
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
----------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TG.Common.Weixin.Mp.Datas
{
    /// <summary>
    /// 批量导入门店数据返回结果
    /// </summary>
    public class StoreResultJson : JsonResult
    {
        /// <summary>
        /// 门店ID。插入失败的门店返回数值“-1”，请核查必填字段后单独调用接口导入。
        /// </summary>
        public string location_id { get; set; }
    }

    /// <summary>
    /// 拉取门店列表返回结果
    /// </summary>
    public class StoreGetResultJson : JsonResult
    {
        public List<SingleStoreResult> location_list { get; set; }
        /// <summary>
        /// 拉取门店数量
        /// </summary>
        public int count { get; set; }
    }

    /// <summary>
    /// 单条门店返回结果
    /// </summary>
    public class SingleStoreResult
    {
        /// <summary>
        /// 门店ID
        /// </summary>
        public string location_id { get; set; }
        /// <summary>
        /// 门店名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 联系电话
        /// </summary>
        public string phone { get; set; }
        /// <summary>
        /// 详细地址
        /// </summary>
        public string address { get; set; }
        /// <summary>
        /// 经度
        /// </summary>
        public string longitude { get; set; }
        /// <summary>
        /// 纬度
        /// </summary>
        public string latitude { get; set; }
    }

    /// <summary>
    /// 卡券类型
    /// </summary>
    public enum CardType
    {
        /// <summary>
        /// 通用券
        /// </summary>
        GENERAL_COUPON = 0,
        /// <summary>
        /// 团购券
        /// </summary>
        GROUPON = 1,
        /// <summary>
        /// 折扣券
        /// </summary>
        DISCOUNT = 2,
        /// <summary>
        /// 礼品券
        /// </summary>
        GIFT = 3,
        /// <summary>
        /// 代金券
        /// </summary>
        CASH = 4,
        /// <summary>
        /// 会员卡
        /// </summary>
        MEMBER_CARD = 5,
        /// <summary>
        /// 门票
        /// </summary>
        SCENIC_TICKET = 6,
        /// <summary>
        /// 电影票
        /// </summary>
        MOVIE_TICKET = 7,
        /// <summary>
        /// 飞机票
        /// </summary>
        BOARDING_PASS = 8,
        /// <summary>
        /// 红包
        /// </summary>
        LUCKY_MONEY = 9,
        /// <summary>
        /// 会议门票
        /// </summary>
        MEETING_TICKET = 10,
    }
}
