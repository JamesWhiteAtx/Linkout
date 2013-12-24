using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Linkout.Controllers;
using System.Web.Http;
using Moq;
using System.Net.Http;

namespace Linkout.Tests
{
    [TestClass]
    public class NetSuiteFileTests
    {
        //global for the test run
        private static TestContext context;


        NetSuiteConfiguration nsConfig;
        INetSuiteConfigService nsConfigService;

        [TestInitialize]
        public void InitializeMethod()
        {
            nsConfig = SelectorTester.MakeNetSuiteConfigurator();
            nsConfigService = nsConfig.Uri;
        }        

        [ClassInitialize()]
        public static void InitializeRoutesTest(TestContext testContext)
        {
            context = TestHelpers.InitializeRoutesTest(testContext);
        }

        [TestMethod]
        public void NetSuiteFileRouteMapsToNetSuiteFileControllerWithIdParmeter()
        {
            var routeData = TestHelpers.PostRouteData(context, "http://site.com/netsuitefile/1");
            Assert.AreEqual(routeData.Values["controller"], "NetSuiteFile");
            Assert.AreEqual(routeData.Values["id"], "1");
        }

        [TestMethod]
        public void NetSuiteFileControllerIsAnApiController()
        {
            var nsService = new Mock<INetSuiteUriFileService>();
            var httpSrvc = new Mock<IHttpRespPassThruService>();
            NetSuiteFileController ctrl = new NetSuiteFileController(nsService.Object, httpSrvc.Object);
            Assert.IsInstanceOfType(ctrl, typeof(ApiController));
        }

        [TestMethod]
        public void NetSuiteFileControllerConstructorRequiresNetSuiteUriService()
        {
            var nsService = new Mock<INetSuiteUriFileService>();
            var httpSrvc = new Mock<IHttpRespPassThruService>();
            NetSuiteFileController ctrl = new NetSuiteFileController(nsService.Object, httpSrvc.Object);
            Assert.IsNotNull(ctrl);
        }

        //[TestMethod]
        //public void NetSuiteFileController()
        //{
        //    var nsService = new Mock<INetSuiteUriFileService>();
        //    NetSuiteFileController ctrl = new NetSuiteFileController(nsService.Object);


        //    ctrl.Request = new HttpRequestMessage(HttpMethod.Post, "http://site.com/netsuitefile/1");
        //    //The line below was needed in WebApi RC as null config caused an issue after upgrade from Beta
        //    ctrl.Configuration = new System.Web.Http.HttpConfiguration(new System.Web.Http.HttpRouteCollection());

        //    var result = ctrl.Get("1");
        //    Assert.IsNotNull(result);
        //}

    }

}