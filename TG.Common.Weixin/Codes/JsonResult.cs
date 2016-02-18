using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TG.Common.Weixin
{
    public class JsonResult
    {
        public int errcode { get; set; }
        public string errmsg { get; set; }
        public virtual object P2PData { get; set; }
    }
}
