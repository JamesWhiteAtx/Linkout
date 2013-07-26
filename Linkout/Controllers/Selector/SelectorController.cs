using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.IO;
using Linkout;
using Ninject;

namespace Linkout.Controllers
{
    public class SelectorController : ApiController
    {
        [Inject]
        public IJsonWebResponseService SelectorService { get; set; }

        [Inject]
        public INetSuiteUriService NetSuiteUriService { get; set; }

        public HttpResponseMessage JsonResponse()
        {
            return SelectorService.GetSelectorJson(NetSuiteUriService.Uri);
        }

        public NetSuiteUriService AddQuery(string name, string value)
        {
            return NetSuiteUriService.AddQuery(name, value);
        }

    }
}
