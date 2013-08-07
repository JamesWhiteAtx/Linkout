using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Http;
using System.Web.Http.Routing;

namespace Linkout.Tests
{
    public static class TestHelpers
    {
        public static void HttpRespStringsAreEqual(HttpResponseMessage testResp, HttpResponseMessage ctrlResp)
        {
            Assert.IsNotNull(ctrlResp);
            string testStr = testResp.Content.ReadAsStringAsync().Result;
            string ctrlStr = ctrlResp.Content.ReadAsStringAsync().Result;
            Assert.AreEqual(testStr, ctrlStr);
        }

        public static TestContext InitializeRoutesTest(TestContext testContext)
        {
            //context = testContext;
            HttpRouteCollection routes = new HttpRouteCollection();
            HttpConfiguration config = new HttpConfiguration(routes);
            testContext.Properties["routes"] = routes;
            testContext.Properties["config"] = config;
            WebApiConfig.Register(config);
            return testContext;
        }

        public static IHttpRouteData GetRouteData(TestContext testContext, string url)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, url);
            HttpRouteCollection routes = (HttpRouteCollection)testContext.Properties["routes"];
            return routes.GetRouteData(request);
        }

        public static IHttpRouteData PostRouteData(TestContext testContext, string url)
        {
            var request = new HttpRequestMessage(HttpMethod.Post, url);
            HttpRouteCollection routes = (HttpRouteCollection)testContext.Properties["routes"];
            return routes.GetRouteData(request);
        }
    }
}
