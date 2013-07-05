using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Linkout.Tests
{
    [TestClass]
    public class ProductApiTests
    {
        //global for the test run
        private static TestContext context;

        [ClassInitialize()]
        public static void InitializeRoutesTest(TestContext testContext)
        {
            context = TestHelpers.InitializeRoutesTest(testContext);
        }

        [TestMethod]
        public void ProductListingRouteMapsToProductControllerListingMethod()
        {
            var routeData = TestHelpers.GetRouteData(context, "http://site.com/product/listing");
            Assert.AreEqual(routeData.Values["controller"], "Product");
            Assert.AreEqual(routeData.Values["action"], "Listing");
        }
    }
}
