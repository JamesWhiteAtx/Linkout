using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Linkout.Controllers;
using Linkout.Services;
using Linkout;
using Moq;
using System.Net.Http;

namespace Linkout.Tests
{
    [TestClass]
    public class SelectorControllerTest
    {
        SelectorController ctrl;

        [TestInitialize]
        public void MyTestMethod()
        {
            ctrl = new SelectorController()
            {
                NetSuiteUriService = new NetSuiteUriService()
            };
        }

        [TestMethod]
        public void SelectorUriAddQueryCreatesNameValueQueryUrl()
        {
            NetSuiteUriService bldr = new NetSuiteUriService();
            string baseUrl = bldr.ToString();
            bldr.AddQuery("name1", "value1");
            bldr.AddQuery("name2", "value2");
            string qryUrl = baseUrl + "&name1=value1&name2=value2"; 

            Assert.AreEqual(qryUrl, bldr.ToString());
        }

        [TestMethod]
        public void BaseNetSuiteUriPointsToNetSuiteDeployUrl()
        {
            string nsUri = NetSuiteUriService.NsBaseUri + 
                "?" + NetSuiteUriService.NsScriptName + "=" + NetSuiteUriService.NsScriptVal +
                "&" + NetSuiteUriService.NsDeployName + "=" + NetSuiteUriService.NsDeployVal +
                "&" + NetSuiteUriService.NsCompidName + "=" + NetSuiteUriService.NsCompidVal +
                "&" + NetSuiteUriService.NsHName + "=" + NetSuiteUriService.NsHVal;

            Assert.AreEqual(nsUri, ctrl.NetSuiteUriService.Uri.ToString());
        }

        [TestMethod]
        public void SelectorControllerAddQueryCreatesNameValueQueryUrl()
        {
            NetSuiteUriService bldr = new NetSuiteUriService();
            bldr.AddQuery("name1", "value1");
            bldr.AddQuery("name2", "value2");

            ctrl.AddQuery("name1", "value1");
            ctrl.AddQuery("name2", "value2");
            Assert.AreEqual(ctrl.NetSuiteUriService.ToString(), bldr.ToString());
        }

        [TestMethod]
        public void SelectorControllerJsonResponseReturnServiceJson()
        {
            string responseFromServer = "the response from server";
            HttpResponseMessage mockResp = new HttpResponseMessage { Content = new StringContent(responseFromServer, System.Text.Encoding.UTF8, "application/json") };

            var selSrvc = new Mock<IJsonResponseService>();
            selSrvc.Setup(srvc => srvc.GetSelectorJson(It.IsAny<Uri>())).Returns(mockResp);
            ctrl.SelectorService = selSrvc.Object;

            HttpResponseMessage ctrlResp = ctrl.JsonResponse();
            Assert.AreEqual(ctrlResp, mockResp);
        }
    }
}
