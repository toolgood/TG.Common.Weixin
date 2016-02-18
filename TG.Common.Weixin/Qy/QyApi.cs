using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using TG.Common.Weixin.Codes;

namespace TG.Common.Weixin.Qy
{
    public abstract class QyApi: WebApi
    {
        protected WxCorpApi _api;
        public QyApi(WxCorpApi api)
        {
            _api = api;
        }
    }


}
