using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Linkout.Controllers;
using Moq;
using System.Web.Mvc;
using System.Net.Http;
using System.Web.Http;
using Linkout;
using Newtonsoft.Json;

namespace Linkout.Tests
{
    //[TestClass]
    //public class PriceApiTests
    //{
    //    private readonly decimal heaterPrice = 199m;

    //    private IPriceService MakeMockPriceService()
    //    {
    //        var srvc = new Mock<IPriceService>();
    //        srvc.Setup(s => s.GetLeatherPrice(It.IsAny<int>())).Returns((int rows) =>
    //        {
    //            return new LeatherPrice
    //                {
    //                    Type = "leather",
    //                    Rows = rows,
    //                    Price = (decimal)rows
    //                };
    //        });
    //        srvc.Setup(s => s.GetHeaterPrice( )).Returns(
    //            new HeaterPrice
    //            {
    //                Type = "heater",
    //                Price = heaterPrice
    //            }
    //        );
    //        return srvc.Object;
    //    }

    //    private PriceController MakePriceController(IPriceService srvc)
    //    {
    //        return new PriceController(srvc, new JsonHttpResponseService());
    //    }

    //    [TestMethod]
    //    public void PriceServiceIterfaceGetLeatherPriceRequiresRowsParm()
    //    {
    //        IPriceService srvc = MakeMockPriceService();
    //        var price = srvc.GetLeatherPrice(2);
    //        Assert.IsInstanceOfType(price, typeof(ILeatherPrice));
    //    }

    //    [TestMethod]
    //    public void PriceServiceIterfaceGetLeatherPriceReturnDecimal()
    //    {
    //        IPriceService srvc = MakeMockPriceService();
    //        var price = srvc.GetLeatherPrice(2);
    //        decimal expected = 2M;
    //        Assert.AreEqual(price.Price, expected);
    //    }

    //    [TestMethod]
    //    public void PriceControllerHasConstructorServiceInjected()
    //    {
    //        PriceController ctrl = new PriceController(MakeMockPriceService(), new JsonHttpResponseService());
    //    }

    //    [TestMethod]
    //    public void PriceControllerIsInstanceOfApiController()
    //    {
    //        Assert.IsInstanceOfType(MakePriceController(MakeMockPriceService()), typeof(ApiController));
    //    }

    //    [TestMethod]
    //    public void PriceControllerLeatherMethodReturnsHttpResponseMessage()
    //    {
    //        PriceController ctrl = MakePriceController(MakeMockPriceService());
    //        HttpResponseMessage result = ctrl.Leather(2);
    //        Assert.IsInstanceOfType(result, typeof(HttpResponseMessage));
    //    }

    //    [TestMethod]
    //    public void PriceControlLerLeatherMethodReturnsExpectedServiceResponse()
    //    {
    //        PriceController ctrl = MakePriceController(MakeMockPriceService());
    //        HttpResponseMessage ctrlResp = ctrl.Leather(2);

    //        IPriceService srvc = MakeMockPriceService();
    //        var price = srvc.GetLeatherPrice(2);
    //        HttpResponseMessage testResp = new JsonHttpResponseService().GetObjectHttpResponseMessage(price);

    //        TestHelpers.HttpRespStringsAreEqual(testResp, ctrlResp);
    //    }

    //    [TestMethod]
    //    public void PriceControllerHeaterMethodReturnsHttpResponseMessage()
    //    {
    //        PriceController ctrl = MakePriceController(MakeMockPriceService());
    //        HttpResponseMessage result = ctrl.Heater();
    //        Assert.IsInstanceOfType(result, typeof(HttpResponseMessage));
    //    }

    //    [TestMethod]
    //    public void PriceControlLerHeaterMethodReturnsExpectedServiceResponse()
    //    {
    //        PriceController ctrl = MakePriceController(MakeMockPriceService());
    //        HttpResponseMessage ctrlResp = ctrl.Heater();

    //        IPriceService srvc = MakeMockPriceService();
    //        var price = srvc.GetHeaterPrice();
    //        HttpResponseMessage testResp = new JsonHttpResponseService().GetObjectHttpResponseMessage(price);

    //        TestHelpers.HttpRespStringsAreEqual(testResp, ctrlResp);
    //    }

    //}
}

