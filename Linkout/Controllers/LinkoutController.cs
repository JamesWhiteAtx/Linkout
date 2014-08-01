using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Linkout.Controllers
{
    //[AllowAnonymous]
    public class LinkoutController : Controller
    {
        //
        // GET: /Linkout/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Partials(string name)
        {
            return PartialView("Partials/" + name);
        }
    }
}
