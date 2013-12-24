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
        public INetSuiteUriSelectorService NetSuiteUriSelectorService { get; set; }

        public HttpResponseMessage JsonResponse()
        {
            return SelectorService.GetHttpJsonResponse(NetSuiteUriSelectorService.Uri);
        }

        public void AddQuery(string name, string value)
        {
            NetSuiteUriSelectorService.AddQuery(name, value);
        }

        public void SetType(string value)
        {
            NetSuiteUriSelectorService.SetType(value);
        }

        public void SetCtlg(int value)
        {
            NetSuiteUriSelectorService.SetCtlg(value);
        }


    }
}
