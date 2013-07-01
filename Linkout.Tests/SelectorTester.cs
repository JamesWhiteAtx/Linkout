using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using Moq;
using Linkout;
using Linkout.Services;
using Linkout.Controllers;

namespace Linkout.Tests
{
    public static class SelectorTester
    {
        public static HttpResponseMessage HRM(Uri uri)
        {
            return new HttpResponseMessage { Content = new StringContent(uri.ToString(), System.Text.Encoding.UTF8, "application/json") };
        }

        public static IJsonResponseService MakeMockService()
        {
            var selSrvc = new Mock<IJsonResponseService>();
            selSrvc.Setup(srvc => srvc.GetSelectorJson(It.IsAny<Uri>()))
                    .Returns((Uri uri) => HRM(uri));
            return selSrvc.Object;
        }

        public static T MakeSelCtrl<T>() where T : SelectorController, new()
        {
            SelectorController ctrl = new T();
            ctrl.SelectorService = MakeMockService();
            ctrl.NetSuiteUriService = new NetSuiteUriService();
            return (T)ctrl;
        }

        public static HttpResponseMessage GetMockServiceResponse(INetSuiteUriService bldr)
        {
            return MakeMockService().GetSelectorJson(bldr.Uri);
        }

    }

}
