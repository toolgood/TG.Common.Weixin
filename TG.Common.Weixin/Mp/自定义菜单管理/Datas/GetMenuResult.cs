using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TG.Common.Weixin.Mp.Datas
{
    public class GetMenuResult : JsonResult
    {
        public ButtonGroup menu { get; set; }

        public GetMenuResult()
        {
            menu = new ButtonGroup();
        }
    }
}
