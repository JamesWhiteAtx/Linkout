using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Http;
using Moq;
using System.Web;
using System.Web.Routing;
using System.Net.Http;
using System.Web.Http.Routing;
using Linkout;

namespace Linkout.Tests
{
    [TestClass]
    public class SelectorRoutesTests
    {
        //global for the test run
        private static TestContext context;

        private readonly string makeid = "123";
        private readonly string year = "1965";
        private readonly string modelid = "345";
        private readonly string bodyid = "678";
        private readonly string trimid = "891";

        private readonly string carid = "321";
        private readonly string ptrnid = "654";
        private readonly string intcolid = "987";

        private readonly string zipcode = "12345";


        [ClassInitialize()]
        public static void InitializeRoutesTest(TestContext testContext)
        {
            context = TestHelpers.InitializeRoutesTest(testContext);
        }

        [TestMethod]
        public void MakesRouteMapsToMakeGet()
        {
            var routeData = TestHelpers.GetRouteData(context, "http://site.com/selector/makes");
            Assert.AreEqual(routeData.Values["controller"], "Makes");
            Assert.AreEqual(routeData.Values["action"], "Get");
        }
        [TestMethod]
        public void YearsRouteMapsToYearsGet()
        {
            var routeData = TestHelpers.GetRouteData(context, "http://site.com/selector/make/"+makeid+"/years");
            Assert.AreEqual(routeData.Values["controller"], "Years");
            Assert.AreEqual(routeData.Values["action"], "Get");
            Assert.AreEqual(routeData.Values["makeid"], makeid);
        }

        [TestMethod]
        public void ModelsRouteMapsToModelsGet()
        {
            var routeData = TestHelpers.GetRouteData(context, "http://site.com/selector/make/"+makeid+"/year/"+year+"/models");
            Assert.AreEqual(routeData.Values["controller"], "Models");
            Assert.AreEqual(routeData.Values["action"], "Get");
            Assert.AreEqual(routeData.Values["makeid"], makeid);
            Assert.AreEqual(routeData.Values["year"], year);
        }

        [TestMethod]
        public void BodiesRouteMapsToBodiesGet()
        {
            var routeData = TestHelpers.GetRouteData(context, "http://site.com/selector/make/"+makeid+"/year/"+year+"/model/"+modelid+"/bodies");
            Assert.AreEqual(routeData.Values["controller"], "Bodies");
            Assert.AreEqual(routeData.Values["action"], "Get");
            Assert.AreEqual(routeData.Values["makeid"], makeid);
            Assert.AreEqual(routeData.Values["year"], year);
            Assert.AreEqual(routeData.Values["modelid"], modelid);
        }

        [TestMethod]
        public void TrimsRouteMapsToTrimsGet()
        {
            var routeData = TestHelpers.GetRouteData(context, "http://site.com/selector/make/"+makeid+"/year/"+year+"/model/"+modelid+"/body/"+bodyid+"/trims");
            Assert.AreEqual(routeData.Values["controller"], "Trims");
            Assert.AreEqual(routeData.Values["action"], "Get");
            Assert.AreEqual(routeData.Values["makeid"], makeid);
            Assert.AreEqual(routeData.Values["year"], year);
            Assert.AreEqual(routeData.Values["modelid"], modelid);
            Assert.AreEqual(routeData.Values["bodyid"], bodyid);
        }

        [TestMethod]
        public void CarsRouteMapsToCarsGet()
        {
            var routeData = TestHelpers.GetRouteData(context, "http://site.com/selector/make/"+makeid+"/year/"+year+"/model/"+modelid+"/body/"+bodyid+"/trim/"+trimid+"/cars");
            Assert.AreEqual(routeData.Values["controller"], "Cars");
            Assert.AreEqual(routeData.Values["action"], "Get");
            Assert.AreEqual(routeData.Values["makeid"], makeid);
            Assert.AreEqual(routeData.Values["year"], year);
            Assert.AreEqual(routeData.Values["modelid"], modelid);
            Assert.AreEqual(routeData.Values["bodyid"], bodyid);
            Assert.AreEqual(routeData.Values["trimid"], trimid);
        }

        [TestMethod]
        public void PatternRouteMapsToPatternGet()
        {
            var routeData = TestHelpers.GetRouteData(context, "http://site.com/selector/car/"+carid+"/ptrns");
            Assert.AreEqual(routeData.Values["controller"], "Ptrns");
            Assert.AreEqual(routeData.Values["action"], "Get");
            Assert.AreEqual(routeData.Values["carid"], carid);
        }

        [TestMethod]
        public void InteriorColorsRouteMapsToInteriorColorsGet()
        {
            var routeData = TestHelpers.GetRouteData(context, "http://site.com/selector/car/"+carid+"/ptrn/"+ptrnid+"/intcols");
            Assert.AreEqual(routeData.Values["controller"], "IntCols");
            Assert.AreEqual(routeData.Values["action"], "Get");
            Assert.AreEqual(routeData.Values["carid"], carid);
            Assert.AreEqual(routeData.Values["ptrnid"], ptrnid);
        }
        [TestMethod]

        public void ReccomendedColorRouteMapsToReccomendedColorGet()
        {
            var routeData = TestHelpers.GetRouteData(context, "http://site.com/selector/car/"+carid+"/ptrn/"+ptrnid+"/intcol/"+intcolid+"/reccols");
            Assert.AreEqual(routeData.Values["controller"], "RecCols");
            Assert.AreEqual(routeData.Values["action"], "Get");
            Assert.AreEqual(routeData.Values["carid"], carid);
            Assert.AreEqual(routeData.Values["ptrnid"], ptrnid);
            Assert.AreEqual(routeData.Values["intcolid"], intcolid);
        }

        [TestMethod]
        public void AllColorRouteMapsToAllColorGet()
        {
            var routeData = TestHelpers.GetRouteData(context, "http://site.com/selector/ptrn/"+ptrnid+"/allcols");
            Assert.AreEqual(routeData.Values["controller"], "AllCols");
            Assert.AreEqual(routeData.Values["action"], "Get");
            Assert.AreEqual(routeData.Values["ptrnid"], ptrnid);
        }

        [TestMethod]
        public void ScheduleRouteMapsToScheduleGet()
        {
            var routeData = TestHelpers.GetRouteData(context, "http://site.com/schedule/installers/"+zipcode);
            Assert.AreEqual(routeData.Values["controller"], "Installers");
            Assert.AreEqual(routeData.Values["action"], "Get");
            Assert.AreEqual(routeData.Values["zipcode"], zipcode);
        }
        
    }
}
