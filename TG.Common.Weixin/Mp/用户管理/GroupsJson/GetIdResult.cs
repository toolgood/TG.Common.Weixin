/*----------------------------------------------------------------
    Copyright (C) 2015 Senparc
    
    文件名：GetIdResult.cs
    文件功能描述：获取用户分组ID返回结果
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
----------------------------------------------------------------*/


namespace TG.Common.Weixin.Mp.Datas
{
    /// <summary>
    /// 获取用户分组ID返回结果
    /// </summary>
    public class GetGroupIdResult : JsonResult
    {
        public int groupid { get; set; }
    }
}
