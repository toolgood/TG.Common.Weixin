using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace TG.Common.Weixin.Qy.Datas
{
    /// <summary>
    /// 设置企业号应用需要Post的数据
    /// </summary>
    public class GetAppListResult : JsonResult
    {
        public List<GetAppList_AppInfo> agentlist { get; set; }
    }

    public class GetAppList_AppInfo
    {
        /// <summary>
        /// 企业应用id
        /// </summary>
        public string agentid { get; set; }
        /// <summary>
        /// 企业应用名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 企业应用方形头像
        /// </summary>
        public string square_logo_url { get; set; }
        /// <summary>
        /// 企业应用圆形头像
        /// </summary>
        public string round_logo_url { get; set; }
    }
}
