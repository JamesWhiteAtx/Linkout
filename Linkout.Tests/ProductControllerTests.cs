using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Linkout.Controllers;
using Moq;
using System.Net.Http;
using Linkout.Models;
using Newtonsoft.Json;
using System.Web.Http;

namespace Linkout.Tests
{
    [TestClass]
    public class ProductControllerTests
    {
        [TestMethod]
        public void ProductControllerIsAnApiController()
        {
            var prodSrvc = new Mock<IProductService>();
            var jsonSrvc = new Mock<IJsonHttpResponseService>();
            ProductController ctrl = new ProductController(prodSrvc.Object, jsonSrvc.Object);
            Assert.IsInstanceOfType(ctrl, typeof(ApiController));
        }

        [TestMethod]
        public void ProductControllerConstructorRequiresProductService()
        {
            var prodSrvc = new Mock<IProductService>();
            var jsonSrvc = new Mock<IJsonHttpResponseService>();
            ProductController ctrl = new ProductController(prodSrvc.Object, jsonSrvc.Object);
            Assert.IsNotNull(ctrl);
        }

        [TestMethod]
        public void ProductControllerListingMethodReturnsHttpResponseMessage()
        {
            var prodSrvc = new Mock<IProductService>();
            ProductController ctrl = new ProductController(prodSrvc.Object, new JsonHttpResponseService());
            var result = ctrl.Listing();
            Assert.IsInstanceOfType(result, typeof(HttpResponseMessage));
        }

        protected static List<ProductModel> makeProdList() 
        { 
            return  new List<ProductModel>
            {
                new ProductModel
                    {
                        ID = 1,
                        Code = "1",
                        Description = "One",
                        Price = 100.12m
                    },
                new ProductModel
                    {
                        ID = 2,
                        Code = "2",
                        Description = "Two",
                        Price = 200.12m
                    }
            };
        }

        [TestMethod]
        public void ProductControllerListingMethodReturnsJsonEnumerableOfProductModels()
        {
            var expResp = new JsonHttpResponseService().GetObjectHttpResponseMessage(makeProdList());

            var prodSrvc = new Mock<IProductService>();
            prodSrvc.Setup(s => s.Listing()).Returns(makeProdList());
            
            ProductController ctrl = new ProductController(prodSrvc.Object, new JsonHttpResponseService());
            var ctrlResp = ctrl.Listing();

            TestHelpers.HttpRespStringsAreEqual(expResp, ctrlResp);
        }
    }
}
