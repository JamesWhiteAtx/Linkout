using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Configuration;
using System.Reflection;
using System.Web;

namespace Linkout.Tests
{
    [TestClass]
    public class UriServiceTests
    {
        NetSuiteConfiguration nsConfig = new NetSuiteConfiguration();

        [TestInitialize]
        public void InitializeMethod()
        {
            nsConfig = SelectorTester.MakeNetSuiteConfigurator();
        }        
        
        [TestMethod]
        public void ConfigurationManagerReadsAppConfigFileTestValue()
        {
            string value = ConfigurationManager.AppSettings["TestValue"];
            Assert.IsFalse(String.IsNullOrEmpty(value), "No App.Config found.");
            Assert.AreEqual(value, "Test Value");
        }

        [TestMethod]
        public void MakeNetSuiteConfigurationFromConfigFile()
        {
            NetSuiteConfiguration nsSect = ConfigurationManager.GetSection(NetSuiteConfiguration.ConfigSectionName) as NetSuiteConfiguration;
            Assert.AreEqual(nsConfig.Uri.Scheme, nsSect.Uri.Scheme);
        }

        private UriBuilder MakeBaseScriptBuilder()
        {
            return new UriBuilder
            {
                Scheme = SelectorTester.Scheme,
                Host = SelectorTester.FormsHost,
                Path = SelectorTester.ScriptPath
            };
        }

        private UriBuilder MakeSelectorScriptBuilder()
        {
            UriBuilder expectedUri = MakeBaseScriptBuilder();
            AddBldrQuery(expectedUri, NetSuiteUriScriptBase.NsScriptName, SelectorTester.SelScriptVal);
            AddBldrQuery(expectedUri, NetSuiteUriScriptBase.NsDeployName, SelectorTester.SelDeployVal);
            AddBldrQuery(expectedUri, NetSuiteUriScriptBase.NsCompidName, SelectorTester.CompidVal);
            AddBldrQuery(expectedUri, NetSuiteUriScriptBase.NsHName, SelectorTester.HVal);
            return expectedUri;
        }

        private void AddBldrQuery(UriBuilder bldr, string name, string value)
        {
            var queryString = HttpUtility.ParseQueryString(bldr.Query);
            queryString.Add(name, value);
            bldr.Query = queryString.ToString();
        }


        [TestMethod]
        public void NetSuiteUriScriptBaseMatchesConfigValues()
        {
            //https://forms.sandbox.netsuite.com/app/site/hosting/scriptlet.nl
 
            NetSuiteUriScriptBase x = new NetSuiteUriScriptBase(nsConfig.Uri);

            UriBuilder expectedUri = MakeBaseScriptBuilder();

            Assert.AreEqual(x.Uri.Scheme, expectedUri.Uri.Scheme);
            Assert.AreEqual(x.Uri.Host, expectedUri.Uri.Host);
            Assert.AreEqual(x.Uri.LocalPath, expectedUri.Uri.LocalPath);
            Assert.AreEqual(x.Uri.AbsoluteUri, expectedUri.Uri.AbsoluteUri);
        }

        private NetSuiteUriScriptSelector makeNetSuiteUriScriptSelector()
        {
            return new NetSuiteUriScriptSelector(nsConfig.Uri);
        }

        [TestMethod]
        public void NetSuiteUriScriptSelectorIsInstanceOfScriptBase()
        {
            NetSuiteUriScriptSelector x = makeNetSuiteUriScriptSelector();
            Assert.IsInstanceOfType(x, typeof(NetSuiteUriScriptBase)); 
        }

        [TestMethod]
        public void NetSuiteUriScriptSelectorMatchesConfigValues()
        {
            //https://forms.sandbox.netsuite.com/app/site/hosting/scriptlet.nl  ?script=32&deploy=1&compid=801095&h=20a61f1484463b5b9654 
            NetSuiteUriScriptSelector x = makeNetSuiteUriScriptSelector();

            UriBuilder expectedUri = MakeSelectorScriptBuilder();
            Assert.AreEqual(x.Uri.Query, expectedUri.Uri.Query);
            Assert.AreEqual(x.Uri.AbsoluteUri, expectedUri.Uri.AbsoluteUri);
        }

        [TestMethod]
        public void NetSuiteUriScriptSelectorExtraParmsExtendQueryString()
        {
            //https://forms.sandbox.netsuite.com/app/site/hosting/scriptlet.nl  ?script=32&deploy=1&compid=801095&h=20a61f1484463b5b9654  &type=cars&makeid=1&year=2009&modelid=785&bodyid=22&trimid=138
            NetSuiteUriScriptSelector x = makeNetSuiteUriScriptSelector();
            x.AddQuery("type", "cars").AddQuery("makeid", "1").AddQuery("year", "2009");

            UriBuilder expectedUri = MakeSelectorScriptBuilder();
            AddBldrQuery(expectedUri, "type", "cars");
            AddBldrQuery(expectedUri, "makeid", "1");
            AddBldrQuery(expectedUri, "year", "2009");

            Assert.AreEqual(x.Uri.Query, expectedUri.Uri.Query);
            Assert.AreEqual(x.Uri.AbsoluteUri, expectedUri.Uri.AbsoluteUri);
        }

        private UriBuilder MakeBaseSystemBuilder()
        {
            return new UriBuilder
            {
                Scheme = SelectorTester.Scheme,
                Host = SelectorTester.SysHost
            };
        }

        [TestMethod]
        public void NetSuiteUriSytemBaseMatchesConfigValues()
        {
            //https://system.sandbox.netsuite.com

            NetSuiteUriSystemBase x = new NetSuiteUriSystemBase(nsConfig.Uri);

            UriBuilder expectedUri = MakeBaseSystemBuilder();

            Assert.AreEqual(x.Uri.Scheme, expectedUri.Uri.Scheme);
            Assert.AreEqual(x.Uri.Host, expectedUri.Uri.Host);
            Assert.AreEqual(x.Uri.LocalPath, expectedUri.Uri.LocalPath);
            Assert.AreEqual(x.Uri.AbsoluteUri, expectedUri.Uri.AbsoluteUri);
        }

        [TestMethod]
        public void NetSuiteUriCustRecordIsInstanceOfSystemBase()
        {
            NetSuiteUriCustRecord x = new NetSuiteUriCustRecord(nsConfig.Uri);
            Assert.IsInstanceOfType(x, typeof(NetSuiteUriSystemBase)); 
        }

        private UriBuilder MakeCustRecBuilder()
        {
            UriBuilder bldr = MakeBaseSystemBuilder();
            bldr.Path = SelectorTester.CustRecPath;
            return bldr;
        }

        [TestMethod]
        public void NetSuiteUriCustRecordMatchesConfigValues()
        {
            //https://system.sandbox.netsuite.com /app/common/custom/custrecordentry.nl

            NetSuiteUriCustRecord x = new NetSuiteUriCustRecord(nsConfig.Uri);

            UriBuilder expectedUri = MakeCustRecBuilder();

            Assert.AreEqual(x.Uri.Scheme, expectedUri.Uri.Scheme);
            Assert.AreEqual(x.Uri.Host, expectedUri.Uri.Host);
            Assert.AreEqual(x.Uri.LocalPath, expectedUri.Uri.LocalPath);
            Assert.AreEqual(x.Uri.AbsoluteUri, expectedUri.Uri.AbsoluteUri);
        }

        private UriBuilder MakeCustRecTypeBuilder(string type, string id = null)
        {
            UriBuilder bldr = MakeCustRecBuilder();
            AddBldrQuery(bldr, NetSuiteUriCustRecord.NsRecTypeName, type);
            AddBldrQuery(bldr, NetSuiteUriCustRecord.NsRecIDName, id);
            return bldr;
        }

        private string GetCustRecTypeUrl(string type, string id = null)
        {
            return MakeCustRecTypeBuilder(type, id).Uri.AbsoluteUri;
        }

        [TestMethod]
        public void NetSuiteUriCustRecordSetTypeExtendsQueryTypeAndIdWithNoValue()
        {
            //https://system.sandbox.netsuite.com /app/common/custom/custrecordentry.nl ?rectype=19 &id=

            NetSuiteUriCustRecord x = new NetSuiteUriCustRecord(nsConfig.Uri);
            x.SetTypeID("19");

            UriBuilder expectedUri = MakeCustRecTypeBuilder("19");

            Assert.AreEqual(x.Uri.Query, expectedUri.Uri.Query);
            Assert.AreEqual(x.Uri.AbsoluteUri, expectedUri.Uri.AbsoluteUri);
        }


        [TestMethod]
        public void NetSuiteUriMakeRecordQueryMatchesConfig()
        {
            Assert.AreEqual(new NetSuiteUriCustRecord(nsConfig.Uri).GetUrlMake(), GetCustRecTypeUrl(SelectorTester.MakeCustRecId));
        }
        
        [TestMethod]
        public void NetSuiteUriModelRecordQueryMatchesConfig()
        {
            Assert.AreEqual(new NetSuiteUriCustRecord(nsConfig.Uri).GetUrlModel(), GetCustRecTypeUrl(SelectorTester.ModelCustRecId));
        }
    
        [TestMethod]
        public void NetSuiteUriBodyRecordQueryMatchesConfig()
        {
            Assert.AreEqual(new NetSuiteUriCustRecord(nsConfig.Uri).GetUrlBody(), GetCustRecTypeUrl(SelectorTester.BodyCustRecId));
        }

        [TestMethod]
        public void NetSuiteUriTrimRecordQueryMatchesConfig()
        {
            Assert.AreEqual(new NetSuiteUriCustRecord(nsConfig.Uri).GetUrlTrim(), GetCustRecTypeUrl(SelectorTester.TrimCustRecId));
        }

        [TestMethod]
        public void NetSuiteUriCarRecordQueryMatchesConfig()
        {
            Assert.AreEqual(new NetSuiteUriCustRecord(nsConfig.Uri).GetUrlCar(), GetCustRecTypeUrl(SelectorTester.CarCustRecId));
        }

        [TestMethod]
        public void NetSuiteUriPatternRecordQueryMatchesConfig()
        {
            Assert.AreEqual(new NetSuiteUriCustRecord(nsConfig.Uri).GetUrlPattern(), GetCustRecTypeUrl(SelectorTester.PatternCustRecId));
        }

        [TestMethod]
        public void NetSuiteUriItemIsInstanceOfSystemBase()
        {
            NetSuiteUriItem x = new NetSuiteUriItem(nsConfig.Uri);
            Assert.IsInstanceOfType(x, typeof(NetSuiteUriSystemBase));
        }

        private UriBuilder MakeItemBuilder()
        {
            UriBuilder bldr = MakeBaseSystemBuilder();
            bldr.Path = SelectorTester.ItemPath;
            return bldr;
        }

        [TestMethod]
        public void NetSuiteUriItemMatchesConfigValues()
        {
            //https://system.sandbox.netsuite.com /app/common/item/item.nl ?id=

            NetSuiteUriItem x = new NetSuiteUriItem(nsConfig.Uri);

            UriBuilder expectedUri = MakeItemBuilder();

            Assert.AreEqual(x.Uri.Scheme, expectedUri.Uri.Scheme);
            Assert.AreEqual(x.Uri.Host, expectedUri.Uri.Host);
            Assert.AreEqual(x.Uri.LocalPath, expectedUri.Uri.LocalPath);
            Assert.AreEqual(x.Uri.AbsoluteUri, expectedUri.Uri.AbsoluteUri);
        }

        [TestMethod]
        public void NetSuiteUriItemSetIdWithoutValueExtendsQuery()
        {
            //https://system.sandbox.netsuite.com /app/common/item/item.nl ?id=

            NetSuiteUriItem x = new NetSuiteUriItem(nsConfig.Uri);
            x.SetID(null);

            UriBuilder expectedUri = MakeItemBuilder();
            AddBldrQuery(expectedUri, NetSuiteUriItem.NsItemIDName, null);

            Assert.AreEqual(x.Uri.Query, expectedUri.Uri.Query);
            Assert.AreEqual(x.Uri.AbsoluteUri, expectedUri.Uri.AbsoluteUri);
        }

        [TestMethod]
        public void NetSuiteUriItemSetIdWithValueExtendsQuery()
        {
            //https://system.sandbox.netsuite.com /app/common/item/item.nl ?id=

            NetSuiteUriItem x = new NetSuiteUriItem(nsConfig.Uri);
            x.SetID("1");

            UriBuilder expectedUri = MakeItemBuilder();
            AddBldrQuery(expectedUri, NetSuiteUriItem.NsItemIDName, "1");

            Assert.AreEqual(x.Uri.Query, expectedUri.Uri.Query);
            Assert.AreEqual(x.Uri.AbsoluteUri, expectedUri.Uri.AbsoluteUri);
        }

    }
}
