using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TG.Common.Weixin.Qy.Datas
{
    /// <summary>
    /// openid转换成userid接口返回的Json结果
    /// </summary>
    public class ConvertToUserIdResult : JsonResult
    {
        /// <summary>
        /// 该openid在企业号中对应的成员userid
        /// </summary>
        public string userid { get; set; }
    }
}
