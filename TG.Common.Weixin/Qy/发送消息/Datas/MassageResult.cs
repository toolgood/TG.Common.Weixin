using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TG.Common.Weixin.Qy.Datas
{
    public class MassageResult : JsonResult
    {
        public string invaliduser { get; set; }
        public string invalidparty { get; set; }
        public string invalidtag { get; set; }
    }
}
