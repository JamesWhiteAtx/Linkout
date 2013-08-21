using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Linkout.Controllers
{
    //[AllowAnonymous]
    public class DemoController : Controller
    {
        public ActionResult Index()
        {
            //return View();
            return Redirect("https://www.roadwire.com");
        }

        [Authorize]
        public ActionResult NsOrder()
        {
            return View();
        }

        public ActionResult Partials(string name)
        {
            return PartialView("Partials/" + name);
        }
    }
}
