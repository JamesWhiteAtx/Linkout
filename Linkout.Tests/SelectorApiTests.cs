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
    //[TestClass]
    //public class SelectorApiTests
    //{
    //    private readonly string makeid = "123";
    //    private readonly string year = "1965";
    //    private readonly string modelid = "345";
    //    private readonly string bodyid = "678";
    //    private readonly string trimid = "891";

    //    private readonly string carid = "321";
    //    private readonly string ptrnid = "654";
    //    private readonly string intcolid = "987";

    //    //private readonly string zipcode = "12345";

    //    [TestMethod]
    //    public void MakesControllerReturnServiceResponseForTypeMakes()
    //    {
    //        HttpResponseMessage testResp = SelectorTester.GetMockServiceResponse(new NetSuiteUriService().AddQuery("type", "makes"));
    //        HttpResponseMessage ctrlResp = SelectorTester.MakeSelCtrl<MakesController>().Get();
    //        TestHelpers.HttpRespStringsAreEqual(testResp, ctrlResp);
    //    }

    //    [TestMethod]
    //    public void YearsControllerReturnServiceResponseForTypeYears()
    //    {
    //        HttpResponseMessage testResp = SelectorTester.GetMockServiceResponse(new NetSuiteUriService()
    //            .AddQuery("type", "years").AddQuery("makeid", makeid));
    //        HttpResponseMessage ctrlResp = SelectorTester.MakeSelCtrl<YearsController>().Get(makeid);
    //        TestHelpers.HttpRespStringsAreEqual(testResp, ctrlResp);
    //    }

    //    [TestMethod]
    //    public void ModelsControllerReturnServiceResponseForTypeModels()
    //    {
    //        HttpResponseMessage testResp = SelectorTester.GetMockServiceResponse(new NetSuiteUriService()
    //            .AddQuery("type", "models").AddQuery("makeid", makeid).AddQuery("year", year));
    //        HttpResponseMessage ctrlResp = SelectorTester.MakeSelCtrl<ModelsController>().Get(makeid, year);
    //        TestHelpers.HttpRespStringsAreEqual(testResp, ctrlResp);
    //    }

    //    [TestMethod]
    //    public void BodiesControllerReturnServiceResponseForTypeBodies()
    //    {
    //        HttpResponseMessage testResp = SelectorTester.GetMockServiceResponse(new NetSuiteUriService()
    //            .AddQuery("type", "bodies").AddQuery("makeid", makeid).AddQuery("year", year).AddQuery("modelid", modelid));
    //        HttpResponseMessage ctrlResp = SelectorTester.MakeSelCtrl<BodiesController>().Get(makeid, year, modelid);
    //        TestHelpers.HttpRespStringsAreEqual(testResp, ctrlResp);
    //    }

    //    [TestMethod]
    //    public void TrimsControllerReturnServiceResponseForTypeTrims()
    //    {
    //        HttpResponseMessage testResp = SelectorTester.GetMockServiceResponse(new NetSuiteUriService()
    //            .AddQuery("type", "trims").AddQuery("makeid", makeid).AddQuery("year", year).AddQuery("modelid", modelid).AddQuery("bodyid", bodyid));
    //        HttpResponseMessage ctrlResp = SelectorTester.MakeSelCtrl<TrimsController>().Get(makeid, year, modelid, bodyid);
    //        TestHelpers.HttpRespStringsAreEqual(testResp, ctrlResp);
    //    }

    //    [TestMethod]
    //    public void CarsControllerReturnServiceResponseForTypeCars()
    //    {
    //        HttpResponseMessage testResp = SelectorTester.GetMockServiceResponse(new NetSuiteUriService()
    //            .AddQuery("type", "cars").AddQuery("makeid", makeid).AddQuery("year", year).AddQuery("modelid", modelid).AddQuery("bodyid", bodyid).AddQuery("trimid", trimid));
    //        HttpResponseMessage ctrlResp = SelectorTester.MakeSelCtrl<CarsController>().Get(makeid, year, modelid, bodyid, trimid);
    //        TestHelpers.HttpRespStringsAreEqual(testResp, ctrlResp);
    //    }

    //    [TestMethod]
    //    public void PtrnsControllerReturnServiceResponseForTypePtrns()
    //    {
    //        HttpResponseMessage testResp = SelectorTester.GetMockServiceResponse(new NetSuiteUriService()
    //            .AddQuery("type", "carptrns").AddQuery("carid", carid));
    //        HttpResponseMessage ctrlResp = SelectorTester.MakeSelCtrl<PtrnsController>().Get(carid);
    //        TestHelpers.HttpRespStringsAreEqual(testResp, ctrlResp);
    //    }

    //    [TestMethod]
    //    public void IntColsControllerReturnServiceResponseForTypeIntCols()
    //    {
    //        HttpResponseMessage testResp = SelectorTester.GetMockServiceResponse(new NetSuiteUriService()
    //            .AddQuery("type", "carintcols").AddQuery("carid", carid).AddQuery("ptrnid", ptrnid));
    //        HttpResponseMessage ctrlResp = SelectorTester.MakeSelCtrl<IntColsController>().Get(carid, ptrnid);
    //        TestHelpers.HttpRespStringsAreEqual(testResp, ctrlResp);
    //    }

    //    [TestMethod]
    //    public void RecColsControllerReturnServiceResponseForTypeRecCols()
    //    {
    //        HttpResponseMessage testResp = SelectorTester.GetMockServiceResponse(new NetSuiteUriService()
    //            .AddQuery("type", "ptrncolors").AddQuery("carid", carid).AddQuery("ptrnid", ptrnid).AddQuery("intcolid", intcolid));
    //        HttpResponseMessage ctrlResp = SelectorTester.MakeSelCtrl<RecColsController>().Get(carid, ptrnid, intcolid);
    //        TestHelpers.HttpRespStringsAreEqual(testResp, ctrlResp);
    //    }

    //    [TestMethod]
    //    public void AllColsControllerReturnServiceResponseForTypeAllCols()
    //    {
    //        HttpResponseMessage testResp = SelectorTester.GetMockServiceResponse(new NetSuiteUriService()
    //            .AddQuery("type", "ptrncolors").AddQuery("ptrnid", ptrnid));
    //        HttpResponseMessage ctrlResp = SelectorTester.MakeSelCtrl<AllColsController>().Get(ptrnid);
    //        TestHelpers.HttpRespStringsAreEqual(testResp, ctrlResp);
    //    }

        //[TestMethod]
        //public void InstallersControllerReturnServiceResponseForTypeInstallers()
        //{
        //    HttpResponseMessage testResp = SelectorTester.GetMockServiceResponse(new SelectorUri().);
        //    HttpResponseMessage ctrlResp = SelectorTester.MakeSelCtrl<InstallersController>().Get(makeid);
        //    SelectorTester.HttpRespStringsAreEqual(testResp, ctrlResp);
        //}
//    }
}
