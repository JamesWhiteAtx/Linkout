using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Linkout.Models;

namespace Linkout.Controllers
{
    [AllowAnonymous]
    public class MockController : Controller
    {
        //
        // GET: /Mock/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CostcoLeather()
        {
            return View();
        }

        public ActionResult CostcoHeaters()
        {
            return View();
        }

        public ActionResult Product(string id)
        {
            MockProduct prod = new MockProduct();
            return View(prod);
        }
    }
}
