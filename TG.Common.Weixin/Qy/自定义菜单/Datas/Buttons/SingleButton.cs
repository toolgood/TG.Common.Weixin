using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TG.Common.Weixin.Qy.Datas
{
    /// <summary>
    /// 所有单击按钮的基类（view，click等）
    /// </summary>
    public abstract class SingleButton : BaseButton
    {
        /// <summary>
        /// 按钮类型（click或view）
        /// </summary>
        public string type { get; set; }

        public SingleButton(string theType)
        {
            type = theType;
        }
    }
}
