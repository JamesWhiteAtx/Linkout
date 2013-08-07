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
    [TestClass]
    public class SelectorApiTests
    {
        private readonly string makeid = "123";
        private readonly string year = "1965";
        private readonly string modelid = "345";
        private readonly string bodyid = "678";
        private readonly string trimid = "891";

        private readonly string carid = "321";
        private readonly string ptrnid = "654";
        private readonly string intcolid = "987";

        //private readonly string zipcode = "12345";

        NetSuiteConfiguration nsConfig;
        INetSuiteConfigService nsConfigService;

        [TestInitialize]
        public void InitializeMethod()
        {
            nsConfig = SelectorTester.MakeNetSuiteConfigurator();
            nsConfigService = nsConfig.Uri;
        }        

        [TestMethod]
        public void MakesControllerReturnServiceResponseForTypeMakes()
        {
            var srvc = new NetSuiteUriScriptSelector(nsConfigService);
            srvc.AddQuery("type", "makes");
            HttpResponseMessage testResp = SelectorTester.GetMockServiceResponse(srvc);
            HttpResponseMessage ctrlResp = SelectorTester.MakeSelCtrl<MakesController>(nsConfigService).Get();
            TestHelpers.HttpRespStringsAreEqual(testResp, ctrlResp);
        }

        [TestMethod]
        public void YearsControllerReturnServiceResponseForTypeYears()
        {
            var srvc = new NetSuiteUriScriptSelector(nsConfigService);
            srvc.AddQuery("type", "years").AddQuery("makeid", makeid);
            HttpResponseMessage testResp = SelectorTester.GetMockServiceResponse(srvc);
            HttpResponseMessage ctrlResp = SelectorTester.MakeSelCtrl<YearsController>(nsConfigService).Get(makeid);
            TestHelpers.HttpRespStringsAreEqual(testResp, ctrlResp);
        }

        [TestMethod]
        public void ModelsControllerReturnServiceResponseForTypeModels()
        {
            var srvc = new NetSuiteUriScriptSelector(nsConfigService);
            srvc.AddQuery("type", "models").AddQuery("makeid", makeid).AddQuery("year", year);
            HttpResponseMessage testResp = SelectorTester.GetMockServiceResponse(srvc);
            HttpResponseMessage ctrlResp = SelectorTester.MakeSelCtrl<ModelsController>(nsConfigService).Get(makeid, year);
            TestHelpers.HttpRespStringsAreEqual(testResp, ctrlResp);
        }

        [TestMethod]
        public void BodiesControllerReturnServiceResponseForTypeBodies()
        {
            var srvc = new NetSuiteUriScriptSelector(nsConfigService);
            srvc.AddQuery("type", "bodies").AddQuery("makeid", makeid).AddQuery("year", year).AddQuery("modelid", modelid);
            HttpResponseMessage testResp = SelectorTester.GetMockServiceResponse(srvc);
            HttpResponseMessage ctrlResp = SelectorTester.MakeSelCtrl<BodiesController>(nsConfigService).Get(makeid, year, modelid);
            TestHelpers.HttpRespStringsAreEqual(testResp, ctrlResp);
        }

        [TestMethod]
        public void TrimsControllerReturnServiceResponseForTypeTrims()
        {
            var srvc = new NetSuiteUriScriptSelector(nsConfigService);
            srvc.AddQuery("type", "trims").AddQuery("makeid", makeid).AddQuery("year", year).AddQuery("modelid", modelid).AddQuery("bodyid", bodyid);
            HttpResponseMessage testResp = SelectorTester.GetMockServiceResponse(srvc);
            HttpResponseMessage ctrlResp = SelectorTester.MakeSelCtrl<TrimsController>(nsConfigService).Get(makeid, year, modelid, bodyid);
            TestHelpers.HttpRespStringsAreEqual(testResp, ctrlResp);
        }

        [TestMethod]
        public void CarsControllerReturnServiceResponseForTypeCars()
        {
            var srvc = new NetSuiteUriScriptSelector(nsConfigService);
            srvc.AddQuery("type", "cars").AddQuery("makeid", makeid).AddQuery("year", year).AddQuery("modelid", modelid).AddQuery("bodyid", bodyid).AddQuery("trimid", trimid);
            HttpResponseMessage testResp = SelectorTester.GetMockServiceResponse(srvc);
            HttpResponseMessage ctrlResp = SelectorTester.MakeSelCtrl<CarsController>(nsConfigService).Get(makeid, year, modelid, bodyid, trimid);
            TestHelpers.HttpRespStringsAreEqual(testResp, ctrlResp);
        }

        [TestMethod]
        public void PtrnsControllerReturnServiceResponseForTypePtrns()
        {
            var srvc = new NetSuiteUriScriptSelector(nsConfigService);
            srvc.AddQuery("type", "carptrns").AddQuery("carid", carid);
            HttpResponseMessage testResp = SelectorTester.GetMockServiceResponse(srvc);
            HttpResponseMessage ctrlResp = SelectorTester.MakeSelCtrl<PtrnsController>(nsConfigService).Get(carid);
            TestHelpers.HttpRespStringsAreEqual(testResp, ctrlResp);
        }

        [TestMethod]
        public void IntColsControllerReturnServiceResponseForTypeIntCols()
        {
            var srvc = new NetSuiteUriScriptSelector(nsConfigService);
            srvc.AddQuery("type", "carintcols").AddQuery("carid", carid).AddQuery("ptrnid", ptrnid);
            HttpResponseMessage testResp = SelectorTester.GetMockServiceResponse(srvc);
            HttpResponseMessage ctrlResp = SelectorTester.MakeSelCtrl<IntColsController>(nsConfigService).Get(carid, ptrnid);
            TestHelpers.HttpRespStringsAreEqual(testResp, ctrlResp);
        }

        [TestMethod]
        public void RecColsControllerReturnServiceResponseForTypeRecCols()
        {
            var srvc = new NetSuiteUriScriptSelector(nsConfigService);
            srvc.AddQuery("type", "ptrncolors").AddQuery("carid", carid).AddQuery("ptrnid", ptrnid).AddQuery("intcolid", intcolid);
            HttpResponseMessage testResp = SelectorTester.GetMockServiceResponse(srvc);
            HttpResponseMessage ctrlResp = SelectorTester.MakeSelCtrl<RecColsController>(nsConfigService).Get(carid, ptrnid, intcolid);
            TestHelpers.HttpRespStringsAreEqual(testResp, ctrlResp);
        }

        [TestMethod]
        public void AllColsControllerReturnServiceResponseForTypeAllCols()
        {
            var srvc = new NetSuiteUriScriptSelector(nsConfigService);
            srvc.AddQuery("type", "ptrncolors").AddQuery("ptrnid", ptrnid);
            HttpResponseMessage testResp = SelectorTester.GetMockServiceResponse(srvc);
            HttpResponseMessage ctrlResp = SelectorTester.MakeSelCtrl<AllColsController>(nsConfigService).Get(ptrnid);
            TestHelpers.HttpRespStringsAreEqual(testResp, ctrlResp);
        }

        //[TestMethod]
        //public void InstallersControllerReturnServiceResponseForTypeInstallers()
        //{
        //    HttpResponseMessage testResp = SelectorTester.GetMockServiceResponse(new SelectorUri().);
        //    HttpResponseMessage ctrlResp = SelectorTester.MakeSelCtrl<InstallersController>().Get(makeid);
        //    SelectorTester.HttpRespStringsAreEqual(testResp, ctrlResp);
        //}
    }
}
