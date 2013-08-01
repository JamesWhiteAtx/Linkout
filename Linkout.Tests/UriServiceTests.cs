using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;
using System.Reflection;

namespace Linkout.Tests
{
    [TestClass]
    public class UriServiceTests
    {
        private readonly string Scheme="https";
        private readonly string FormsHost="forms.sandbox.netsuite.com";
        private readonly string SysHost="system.sandbox.netsuite.com";

        private readonly string ScriptPath="app/site/hosting/scriptlet.nl";
        private readonly string CustRecPath="app/common/custom/custrecordentry.nl";
        private readonly string ItemPath="app/common/item/item.nl";
      
        private readonly string CompidVal="801095";
        private readonly string HVal="20a61f1484463b5b9654";

        private readonly string SelScriptVal="32";
        private readonly string SelDeployVal = "1";


        NetSuiteConfiguration nsConfig = new NetSuiteConfiguration();

        [TestInitialize]
        public void InitializeMethod()
        {
            nsConfig = new NetSuiteConfiguration();

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
        }        
        
        [TestMethod]
        public void ConfigurationManagerReadsAppConfigFileTestValue()
        {
            string value = ConfigurationManager.AppSettings["TestValue"];
            Assert.IsFalse(String.IsNullOrEmpty(value), "No App.Config found.");
            Assert.AreEqual(value, "Test Value");
        }

        [TestMethod]
        public void MakeNetSuiteConfiguration()
        {
            NetSuiteConfiguration nsSect = ConfigurationManager.GetSection("NetSuiteSection") as NetSuiteConfiguration;
            Assert.AreEqual(nsConfig.Uri.Scheme, nsSect.Uri.Scheme);
        }
    }
}
