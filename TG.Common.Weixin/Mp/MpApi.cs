using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using TG.Common.Weixin.Codes;

namespace TG.Common.Weixin.Mp
{
    public class MpApi: WebApi
    {
        protected WxMpApi _api;
        public MpApi(WxMpApi api)
        {
            _api = api;
        }

    }
}
