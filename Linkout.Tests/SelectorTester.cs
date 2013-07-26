using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Net.Http;
using Moq;
using Linkout;
using Linkout.Controllers;

namespace Linkout.Tests
{
    public static class SelectorTester
    {
        public static HttpResponseMessage HRM(Uri uri)
        {
            return new JsonHttpResponseService().GetObjectHttpResponseMessage(uri);
        }

        public static IJsonWebResponseService MakeMockService()
        {
            var selSrvc = new Mock<IJsonWebResponseService>();
            selSrvc.Setup(srvc => srvc.GetSelectorJson(It.IsAny<Uri>()))
                    .Returns((Uri uri) => HRM(uri));
            return selSrvc.Object;
        }

        //public static T MakeSelCtrl<T>() where T : SelectorController, new()
        //{
        //    SelectorController ctrl = new T();
        //    ctrl.SelectorService = MakeMockService();
        //    ctrl.NetSuiteUriService = new NetSuiteUriService();
        //    return (T)ctrl;
        //}

        public static HttpResponseMessage GetMockServiceResponse(INetSuiteUriService bldr)
        {
            return MakeMockService().GetSelectorJson(bldr.Uri);
        }

    }

}
