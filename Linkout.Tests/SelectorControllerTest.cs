using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Linkout.Controllers;
using Linkout;
using Moq;
using System.Net.Http;

namespace Linkout.Tests
{
    [TestClass]
    public class SelectorControllerTest
    {
        SelectorController ctrl;

        private INetSuiteConfigService MakeNetsuiteConfigService()
        {
            return SelectorTester.MakeNetSuiteConfigurator().Uri;
        }

        private INetSuiteUriSelectorService MakeSelectorService()
        {
            return new NetSuiteUriScriptSelector(MakeNetsuiteConfigService());
        }

        [TestInitialize]
        public void InitializeMethod()
        {
            ctrl = new SelectorController()
            {
                NetSuiteUriSelectorService = MakeSelectorService()
            };
        }

        [TestMethod]
        public void SelectorUriAddQueryCreatesNameValueQueryUrl()
        {
            INetSuiteUriSelectorService bldr = MakeSelectorService();
            string baseUrl = bldr.ToString();
            bldr.AddQuery("name1", "value1");
            bldr.AddQuery("name2", "value2");
            string qryUrl = baseUrl + "&name1=value1&name2=value2";

            Assert.AreEqual(qryUrl, bldr.ToString());
        }

        [TestMethod]
        public void BaseNetSuiteUriPointsToNetSuiteDeployUrl()
        {
            INetSuiteConfigService cfg = MakeNetsuiteConfigService();

            string nsUri = cfg.Scheme +"://"+ cfg.FormsHost + cfg.ScriptPath +
                "?" + NetSuiteUriScriptBase.NsScriptName + "=" + cfg.SelScriptVal +
                "&" + NetSuiteUriScriptBase.NsDeployName + "=" + cfg.SelDeployVal +
                "&" + NetSuiteUriScriptBase.NsCompidName + "=" + cfg.CompidVal +
                "&" + NetSuiteUriScriptBase.NsHName + "=" + cfg.HVal;

            Assert.AreEqual(nsUri, ctrl.NetSuiteUriSelectorService.Uri.ToString());
        }

        [TestMethod]
        public void SelectorControllerAddQueryCreatesNameValueQueryUrl()
        {
            INetSuiteUriSelectorService bldr = MakeSelectorService();
            bldr.AddQuery("name1", "value1");
            bldr.AddQuery("name2", "value2");

            ctrl.AddQuery("name1", "value1");
            ctrl.AddQuery("name2", "value2");
            Assert.AreEqual(ctrl.NetSuiteUriSelectorService.ToString(), bldr.ToString());
        }

        [TestMethod]
        public void SelectorControllerJsonResponseReturnServiceJson()
        {
            string responseFromServer = "the response from server";
            HttpResponseMessage mockResp = new JsonHttpResponseService().GetStringHttpResponseMessage(responseFromServer);

            var selSrvc = new Mock<IJsonWebResponseService>();
            selSrvc.Setup(srvc => srvc.GetSelectorJson(It.IsAny<Uri>())).Returns(mockResp);
            ctrl.SelectorService = selSrvc.Object;

            HttpResponseMessage ctrlResp = ctrl.JsonResponse();
            Assert.AreEqual(ctrlResp, mockResp);
        }
    }
}
