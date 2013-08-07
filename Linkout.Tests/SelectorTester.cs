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
        public static readonly string Scheme = "https";
        public static readonly string FormsHost = "forms.sandbox.netsuite.com";
        public static readonly string SysHost = "system.sandbox.netsuite.com";

        public static readonly string ScriptPath = "/app/site/hosting/scriptlet.nl";
        public static readonly string CustRecPath = "/app/common/custom/custrecordentry.nl";
        public static readonly string ItemPath = "/app/common/item/item.nl";

        public static readonly string CompidVal = "801095";
        public static readonly string HVal = "20a61f1484463b5b9654";

        public static readonly string SelScriptVal = "32";
        public static readonly string SelDeployVal = "1";

        public static readonly string MakeCustRecId = "19";
        public static readonly string ModelCustRecId = "20";
        public static readonly string BodyCustRecId = "21";
        public static readonly string TrimCustRecId = "69";
        public static readonly string CarCustRecId = "63";
        public static readonly string PatternCustRecId = "13";

        public static NetSuiteConfiguration MakeNetSuiteConfigurator()
        {
            NetSuiteConfiguration nsConfig = new NetSuiteConfiguration();

            nsConfig.Uri.Scheme = Scheme;
            nsConfig.Uri.FormsHost = FormsHost;
            nsConfig.Uri.SysHost = SysHost;

            nsConfig.Uri.ScriptPath = ScriptPath;
            nsConfig.Uri.CustRecPath = CustRecPath;
            nsConfig.Uri.ItemPath = ItemPath;

            nsConfig.Uri.CompidVal = CompidVal;
            nsConfig.Uri.HVal = HVal;

            nsConfig.Uri.SelScriptVal = SelScriptVal;
            nsConfig.Uri.SelDeployVal = SelDeployVal;

            nsConfig.Uri.MakeCustRecId = MakeCustRecId;
            nsConfig.Uri.ModelCustRecId = ModelCustRecId;
            nsConfig.Uri.BodyCustRecId = BodyCustRecId;
            nsConfig.Uri.TrimCustRecId = TrimCustRecId;
            nsConfig.Uri.CarCustRecId = CarCustRecId;
            nsConfig.Uri.PatternCustRecId = PatternCustRecId;

            return nsConfig;
        }

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

        public static T MakeSelCtrl<T>(INetSuiteConfigService nsConfigService) where T : SelectorController, new()
        {
            SelectorController ctrl = new T();
            ctrl.SelectorService = MakeMockService();
            ctrl.NetSuiteUriSelectorService = new NetSuiteUriScriptSelector(nsConfigService);
            return (T)ctrl;
        }

        public static HttpResponseMessage GetMockServiceResponse(INetSuiteUriSelectorService bldr)
        {
            return MakeMockService().GetSelectorJson(bldr.Uri);
        }

    }

}
