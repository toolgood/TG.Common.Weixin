using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TG.Common.Weixin.Qy.Datas
{
    /// <summary>
    /// 创建部门返回结果
    /// </summary>
    public class CreateDepartmentResult : JsonResult
    {
        /// <summary>
        /// 创建的部门id
        /// </summary>
        public int id { get; set; }
    }

    public class GetDepartmentListResult : JsonResult
    {
        public List<DepartmentList> department { get; set; }
    }

    public class DepartmentList
    {
        /// <summary>
        /// 部门id
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// 部门名称
        /// </summary>
        public string name { get; set; }
        /// <summary>
        /// 上级部门id
        /// </summary>
        public int parentid { get; set; }
        /// <summary>
        /// 在父部门中的次序值。order值小的排序靠前。
        /// </summary>
        public int order { get; set; }
    }
}
