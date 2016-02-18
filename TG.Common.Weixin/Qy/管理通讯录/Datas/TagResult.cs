using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TG.Common.Weixin.Qy.Datas
{
    /// <summary>
    /// 创建标签返回结果
    /// </summary>
    public class CreateTagResult : JsonResult
    {
        /// <summary>
        /// 标签id
        /// </summary>
        public int tagid { get; set; }
    }

    /// <summary>
    /// 获取标签成员返回结果
    /// </summary>
    public class GetTagMemberResult : JsonResult
    {
        /// <summary>
        /// 成员列表
        /// </summary>
        public List<Tag_UserList> userlist { get; set; }
        /// <summary>
        /// 部门列表
        /// </summary>
        public int[] partylist { get; set; }
    }

    public class Tag_UserList
    {
        /// <summary>
        /// 员工UserID
        /// </summary>
        public string userid { get; set; }
        /// <summary>
        /// 成员名称
        /// </summary>
        public string name { get; set; }
    }

    /// <summary>
    /// 添加标签成员返回结果
    /// a)正确时返回{"errcode": 0,"errmsg": "ok"}
    /// b)若部分userid非法，则返回{"errcode": 0,"errmsg": "invalid userlist failed","invalidlist"："usr1|usr2|usr"}
    /// c)当包含userid全部非法时返回{"errcode": 40070,"errmsg": "all list invalid "}
    /// </summary>
    public class AddTagMemberResult : JsonResult
    {
        public string invalidlist { get; set; }
    }

    /// <summary>
    /// 添加标签成员返回结果
    /// a)正确时返回{"errcode": 0,"errmsg": "ok"}
    /// b)若部分userid非法，则返回{"errcode": 0,"errmsg": "invalid userlist failed","invalidlist"："usr1|usr2|usr"}
    /// c)当包含userid全部非法时返回{"errcode": 40070,"errmsg": "all list invalid "}
    /// </summary>
    public class DelTagMemberResult : JsonResult
    {
        public string invalidlist { get; set; }
    }

    /// <summary>
    /// 获取标签列表返回结果
    /// </summary>
    public class GetTagListResult : JsonResult
    {
        public List<TagItem> taglist { get; set; }
    }

    public class TagItem
    {
        public string tagid { get; set; }
        public string tagname { get; set; }
    }
}
