using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Linkout.Controllers
{
    public class ConfigureController : Controller
    {
        //
        // GET: /Configure/
        [Authorize]
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
