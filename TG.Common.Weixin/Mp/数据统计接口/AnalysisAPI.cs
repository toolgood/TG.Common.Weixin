/*----------------------------------------------------------------
    Copyright (C) 2015 Senparc
    
    文件名：AnalysisAPI.cs
    文件功能描述：分析数据接口
    
    
    创建标识：Senparc - 20150211
    
    修改标识：Senparc - 20150303
    修改描述：整理接口
 
    修改标识：Senparc - 20150312
    修改描述：开放代理请求超时时间
----------------------------------------------------------------*/

/*
    图文分析数据接口详见：http://mp.weixin.qq.com/wiki/8/c0453610fb5131d1fcb17b4e87c82050.html
    接口分析数据接口详见：http://mp.weixin.qq.com/wiki/8/30ed81ae38cf4f977194bf1a5db73668.html
    消息分析数据接口详见：http://mp.weixin.qq.com/wiki/12/32d42ad542f2e4fc8a8aa60e1bce9838.html
    用户分析数据接口详见：http://mp.weixin.qq.com/wiki/3/ecfed6e1a0a03b5f35e5efac98e864b7.html
 */


using TG.Common.Weixin.Mp;
using TG.Common.Weixin.Mp.Datas;

namespace TG.Common.Weixin.Mp
{
    /// <summary>
    /// 分析数据接口
    /// 最大时间跨度是指一次接口调用时最大可获取数据的时间范围，如最大时间跨度为7是指最多一次性获取7天的数据。
    /// 注意：所有的日期请使用【yyyy-MM-dd】的格式
    /// </summary>
    public class AnalysisApi : MpApi
    {
        public AnalysisApi(WxMpApi api) : base(api) { }
        /// <summary>
        /// 获取图文群发每日数据（getarticlesummary）
        /// 最大时间跨度 1
        /// </summary>
        /// <param name="beginDate">获取数据的起始日期，begin_date和end_date的差值需小于“最大时间跨度”（比如最大时间跨度为1时，begin_date和end_date的差值只能为0，才能小于1），否则会报错</param>
        /// <param name="endDate">获取数据的结束日期，end_date允许设置的最大值为昨日</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public AnalysisResultJson<ArticleSummaryItem> GetArticleSummary(string beginDate, string endDate, int timeOut = Config.TIME_OUT)
        {
            var accessToken = _api.GetAccessToken();
            string url = string.Format("https://api.weixin.qq.com/datacube/getarticlesummary?access_token={0}", accessToken);
            var data = new
            {
                begin_date = beginDate,
                end_date = endDate
            };
            return Post<AnalysisResultJson<ArticleSummaryItem>>(url, data, timeOut);
        }

        /// <summary>
        /// 获取图文群发总数据（getarticletotal）
        /// 请注意，details中，每天对应的数值为该文章到该日为止的总量（而不是当日的量）
        /// 最大时间跨度 1
        /// </summary>
        /// <param name="beginDate">获取数据的起始日期，begin_date和end_date的差值需小于“最大时间跨度”（比如最大时间跨度为1时，begin_date和end_date的差值只能为0，才能小于1），否则会报错</param>
        /// <param name="endDate">获取数据的结束日期，end_date允许设置的最大值为昨日</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public AnalysisResultJson<ArticleTotalItem> GetArticleTotal(string beginDate, string endDate, int timeOut = Config.TIME_OUT)
        {
            var accessToken = _api.GetAccessToken();
            string url = string.Format("https://api.weixin.qq.com/datacube/getarticletotal?access_token={0}", accessToken);
            var data = new
            {
                begin_date = beginDate,
                end_date = endDate
            };
            return Post<AnalysisResultJson<ArticleTotalItem>>(url, data, timeOut);
        }

        /// <summary>
        /// 获取图文统计数据（getuserread）
        /// 最大时间跨度 3
        /// </summary>
        /// <param name="beginDate">获取数据的起始日期，begin_date和end_date的差值需小于“最大时间跨度”（比如最大时间跨度为1时，begin_date和end_date的差值只能为0，才能小于1），否则会报错</param>
        /// <param name="endDate">获取数据的结束日期，end_date允许设置的最大值为昨日</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public AnalysisResultJson<UserReadItem> GetUserRead(string beginDate, string endDate, int timeOut = Config.TIME_OUT)
        {
            var accessToken = _api.GetAccessToken();
            string url = string.Format("https://api.weixin.qq.com/datacube/getuserread?access_token={0}", accessToken);
            var data = new
            {
                begin_date = beginDate,
                end_date = endDate
            };
            return Post<AnalysisResultJson<UserReadItem>>(url, data, timeOut);
        }

        /// <summary>
        /// 获取图文统计分时数据（getuserreadhour）
        /// 最大时间跨度 1
        /// </summary>
        /// <param name="beginDate">获取数据的起始日期，begin_date和end_date的差值需小于“最大时间跨度”（比如最大时间跨度为1时，begin_date和end_date的差值只能为0，才能小于1），否则会报错</param>
        /// <param name="endDate">获取数据的结束日期，end_date允许设置的最大值为昨日</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public AnalysisResultJson<UserReadHourItem> GetUserReadHour(string beginDate, string endDate, int timeOut = Config.TIME_OUT)
        {
            var accessToken = _api.GetAccessToken();
            string url = string.Format("https://api.weixin.qq.com/datacube/getuserreadhour?access_token={0}", accessToken);
            var data = new
            {
                begin_date = beginDate,
                end_date = endDate
            };
            return Post<AnalysisResultJson<UserReadHourItem>>(url, data, timeOut);
        }

        /// <summary>
        /// 获取图文分享转发数据（getusershare）
        /// 最大时间跨度 7
        /// </summary>
        /// <param name="beginDate">获取数据的起始日期，begin_date和end_date的差值需小于“最大时间跨度”（比如最大时间跨度为1时，begin_date和end_date的差值只能为0，才能小于1），否则会报错</param>
        /// <param name="endDate">获取数据的结束日期，end_date允许设置的最大值为昨日</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public AnalysisResultJson<UserShareItem> GetUserShare(string beginDate, string endDate, int timeOut = Config.TIME_OUT)
        {
            var accessToken = _api.GetAccessToken();
            string url = string.Format("https://api.weixin.qq.com/datacube/getusershare?access_token={0}", accessToken);
            var data = new
            {
                begin_date = beginDate,
                end_date = endDate
            };
            return Post<AnalysisResultJson<UserShareItem>>(url, data, timeOut);
        }

        /// <summary>
        /// 获取图文分享转发分时数据（getusersharehour）
        /// 最大时间跨度 1
        /// </summary>
        /// <param name="beginDate">获取数据的起始日期，begin_date和end_date的差值需小于“最大时间跨度”（比如最大时间跨度为1时，begin_date和end_date的差值只能为0，才能小于1），否则会报错</param>
        /// <param name="endDate">获取数据的结束日期，end_date允许设置的最大值为昨日</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public AnalysisResultJson<UserShareHourItem> GetUserShareHour(string beginDate, string endDate, int timeOut = Config.TIME_OUT)
        {
            var accessToken = _api.GetAccessToken();
            string url = string.Format("https://api.weixin.qq.com/datacube/getusersharehour?access_token={0}", accessToken);
            var data = new
            {
                begin_date = beginDate,
                end_date = endDate
            };
            return Post<AnalysisResultJson<UserShareHourItem>>(url, data, timeOut);
        }

        /// <summary>
        /// 获取接口分析数据（getinterfacesummary）
        /// 最大时间跨度 30
        /// </summary>
        /// <param name="beginDate">获取数据的起始日期，begin_date和end_date的差值需小于“最大时间跨度”（比如最大时间跨度为1时，begin_date和end_date的差值只能为0，才能小于1），否则会报错</param>
        /// <param name="endDate">获取数据的结束日期，end_date允许设置的最大值为昨日</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public AnalysisResultJson<InterfaceSummaryItem> GetInterfaceSummary(string beginDate, string endDate, int timeOut = Config.TIME_OUT)
        {
            var accessToken = _api.GetAccessToken();
            string url = string.Format("https://api.weixin.qq.com/datacube/getinterfacesummary?access_token={0}", accessToken);
            var data = new
            {
                begin_date = beginDate,
                end_date = endDate
            };
            return Post<AnalysisResultJson<InterfaceSummaryItem>>(url, data, timeOut);
        }

        /// <summary>
        /// 获取接口分析分时数据（getinterfacesummaryhour）
        /// 最大时间跨度 1
        /// </summary>
        /// <param name="beginDate">获取数据的起始日期，begin_date和end_date的差值需小于“最大时间跨度”（比如最大时间跨度为1时，begin_date和end_date的差值只能为0，才能小于1），否则会报错</param>
        /// <param name="endDate">获取数据的结束日期，end_date允许设置的最大值为昨日</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public AnalysisResultJson<InterfaceSummaryHourItem> GetInterfaceSummaryHour(string beginDate, string endDate, int timeOut = Config.TIME_OUT)
        {
            var accessToken = _api.GetAccessToken();
            string url = string.Format("https://api.weixin.qq.com/datacube/getinterfacesummaryhour?access_token={0}", accessToken);
            var data = new
            {
                begin_date = beginDate,
                end_date = endDate
            };
            return Post<AnalysisResultJson<InterfaceSummaryHourItem>>(url, data, timeOut);
        }
        /// <summary>
        /// 获取消息发送概况数据（getupstreammsg）
        /// 最大时间跨度 7
        /// </summary>
        /// <param name="beginDate">获取数据的起始日期，begin_date和end_date的差值需小于“最大时间跨度”（比如最大时间跨度为1时，begin_date和end_date的差值只能为0，才能小于1），否则会报错</param>
        /// <param name="endDate">获取数据的结束日期，end_date允许设置的最大值为昨日</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public AnalysisResultJson<UpStreamMsgItem> GetUpStreamMsg(string beginDate, string endDate, int timeOut = Config.TIME_OUT)
        {
            var accessToken = _api.GetAccessToken();
            string url = string.Format("https://api.weixin.qq.com/datacube/getupstreammsg?access_token={0}", accessToken);
            var data = new
            {
                begin_date = beginDate,
                end_date = endDate
            };
            return Post<AnalysisResultJson<UpStreamMsgItem>>(url, data, timeOut);
        }

        /// <summary>
        /// 获取消息分送分时数据（getupstreammsghour）
        /// 最大时间跨度 1
        /// </summary>
        /// <param name="beginDate">获取数据的起始日期，begin_date和end_date的差值需小于“最大时间跨度”（比如最大时间跨度为1时，begin_date和end_date的差值只能为0，才能小于1），否则会报错</param>
        /// <param name="endDate">获取数据的结束日期，end_date允许设置的最大值为昨日</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public AnalysisResultJson<UpStreamMsgHourItem> GetUpStreamMsgHour(string beginDate, string endDate, int timeOut = Config.TIME_OUT)
        {
            var accessToken = _api.GetAccessToken();
            string url = string.Format("https://api.weixin.qq.com/datacube/getupstreammsghour?access_token={0}", accessToken);
            var data = new
            {
                begin_date = beginDate,
                end_date = endDate
            };
            return Post<AnalysisResultJson<UpStreamMsgHourItem>>(url, data, timeOut);
        }

        /// <summary>
        /// 获取消息发送周数据（getupstreammsgweek）
        /// 最大时间跨度 30
        /// </summary>
        /// <param name="beginDate">获取数据的起始日期，begin_date和end_date的差值需小于“最大时间跨度”（比如最大时间跨度为1时，begin_date和end_date的差值只能为0，才能小于1），否则会报错</param>
        /// <param name="endDate">获取数据的结束日期，end_date允许设置的最大值为昨日</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public AnalysisResultJson<UpStreamMsgWeekItem> GetUpStreamMsgWeek(string beginDate, string endDate, int timeOut = Config.TIME_OUT)
        {
            var accessToken = _api.GetAccessToken();
            string url = string.Format("https://api.weixin.qq.com/datacube/getupstreammsgweek?access_token={0}", accessToken);
            var data = new
            {
                begin_date = beginDate,
                end_date = endDate
            };
            return Post<AnalysisResultJson<UpStreamMsgWeekItem>>(url, data, timeOut);
        }

        /// <summary>
        /// 获取消息发送月数据（getupstreammsgmonth）
        /// 最大时间跨度 30
        /// </summary>
        /// <param name="beginDate">获取数据的起始日期，begin_date和end_date的差值需小于“最大时间跨度”（比如最大时间跨度为1时，begin_date和end_date的差值只能为0，才能小于1），否则会报错</param>
        /// <param name="endDate">获取数据的结束日期，end_date允许设置的最大值为昨日</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public AnalysisResultJson<UpStreamMsgMonthItem> GetUpStreamMsgMonth(string beginDate, string endDate, int timeOut = Config.TIME_OUT)
        {
            var accessToken = _api.GetAccessToken();
            string url = string.Format("https://api.weixin.qq.com/datacube/getupstreammsgmonth?access_token={0}", accessToken);
            var data = new
            {
                begin_date = beginDate,
                end_date = endDate
            };
            return Post<AnalysisResultJson<UpStreamMsgMonthItem>>(url, data, timeOut);
        }

        /// <summary>
        /// 获取消息发送分布数据（getupstreammsgdist）
        /// 最大时间跨度 15
        /// </summary>
        /// <param name="beginDate">获取数据的起始日期，begin_date和end_date的差值需小于“最大时间跨度”（比如最大时间跨度为1时，begin_date和end_date的差值只能为0，才能小于1），否则会报错</param>
        /// <param name="endDate">获取数据的结束日期，end_date允许设置的最大值为昨日</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public AnalysisResultJson<UpStreamMsgDistItem> GetUpStreamMsgDist(string beginDate, string endDate, int timeOut = Config.TIME_OUT)
        {
            var accessToken = _api.GetAccessToken();
            string url = string.Format("https://api.weixin.qq.com/datacube/getupstreammsgdist?access_token={0}", accessToken);
            var data = new
            {
                begin_date = beginDate,
                end_date = endDate
            };
            return Post<AnalysisResultJson<UpStreamMsgDistItem>>(url, data, timeOut);
        }

        /// <summary>
        /// 获取消息发送分布周数据（getupstreammsgdistweek）
        /// 最大时间跨度 30
        /// </summary>
        /// <param name="beginDate">获取数据的起始日期，begin_date和end_date的差值需小于“最大时间跨度”（比如最大时间跨度为1时，begin_date和end_date的差值只能为0，才能小于1），否则会报错</param>
        /// <param name="endDate">获取数据的结束日期，end_date允许设置的最大值为昨日</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public AnalysisResultJson<UpStreamMsgDistWeekItem> GetUpStreamMsgDistWeek(string beginDate, string endDate, int timeOut = Config.TIME_OUT)
        {
            var accessToken = _api.GetAccessToken();
            string url = string.Format("https://api.weixin.qq.com/datacube/getupstreammsgdistweek?access_token={0}", accessToken);
            var data = new
            {
                begin_date = beginDate,
                end_date = endDate
            };
            return Post<AnalysisResultJson<UpStreamMsgDistWeekItem>>(url, data, timeOut);
        }

        /// <summary>
        /// 获取消息发送分布月数据（getupstreammsgdistmonth）
        /// 最大时间跨度 30
        /// </summary>
        /// <param name="beginDate">获取数据的起始日期，begin_date和end_date的差值需小于“最大时间跨度”（比如最大时间跨度为1时，begin_date和end_date的差值只能为0，才能小于1），否则会报错</param>
        /// <param name="endDate">获取数据的结束日期，end_date允许设置的最大值为昨日</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public AnalysisResultJson<UpStreamMsgDistMonthItem> GetUpStreamMsgDistMonth(string beginDate, string endDate, int timeOut = Config.TIME_OUT)
        {
            var accessToken = _api.GetAccessToken();
            string url = string.Format("https://api.weixin.qq.com/datacube/getupstreammsgdistmonth?access_token={0}", accessToken);
            var data = new
            {
                begin_date = beginDate,
                end_date = endDate
            };
            return Post<AnalysisResultJson<UpStreamMsgDistMonthItem>>(url, data, timeOut);
        }

        /// <summary>
        /// 获取用户增减数据
        /// 最大时间跨度 7
        /// </summary>
        /// <param name="beginDate">获取数据的起始日期，begin_date和end_date的差值需小于“最大时间跨度”（比如最大时间跨度为1时，begin_date和end_date的差值只能为0，才能小于1），否则会报错</param>
        /// <param name="endDate">获取数据的结束日期，end_date允许设置的最大值为昨日</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public AnalysisResultJson<UserSummaryItem> GetUserSummary(string beginDate, string endDate, int timeOut = Config.TIME_OUT)
        {
            var accessToken = _api.GetAccessToken();
            string url = string.Format("https://api.weixin.qq.com/datacube/getusersummary?access_token={0}", accessToken);
            var data = new
            {
                begin_date = beginDate,
                end_date = endDate
            };
            return Post<AnalysisResultJson<UserSummaryItem>>(url, data, timeOut);
        }

        /// <summary>
        /// 获取累计用户数据
        /// 最大时间跨度 7
        /// </summary>
        /// <param name="beginDate">获取数据的起始日期，begin_date和end_date的差值需小于“最大时间跨度”（比如最大时间跨度为1时，begin_date和end_date的差值只能为0，才能小于1），否则会报错</param>
        /// <param name="endDate">获取数据的结束日期，end_date允许设置的最大值为昨日</param>
        /// <param name="timeOut">代理请求超时时间（毫秒）</param>
        /// <returns></returns>
        public AnalysisResultJson<UserCumulateItem> GetUserCumulate(string beginDate, string endDate, int timeOut = Config.TIME_OUT)
        {
            var accessToken = _api.GetAccessToken();
            string url = string.Format("https://api.weixin.qq.com/datacube/getusercumulate?access_token={0}", accessToken);
            var data = new
            {
                begin_date = beginDate,
                end_date = endDate
            };
            return Post<AnalysisResultJson<UserCumulateItem>>(url, data, timeOut);
        }
    }
}
namespace TG.Common.Weixin
{
    partial class WxMpApi
    {
        private AnalysisApi _AnalysisApi;

        public AnalysisApi AnalysisApi
        {
            get
            {
                if (_AnalysisApi==null)
                {
                    _AnalysisApi= new AnalysisApi(this);
                }

                return _AnalysisApi;
            }
        }

    }
}