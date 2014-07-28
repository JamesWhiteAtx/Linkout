using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Linkout.Controllers.Costco
{
    [AllowAnonymous]
    public class CostcoController : Controller
    {
        //
        // GET: /Costco/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Partial(string name)
        {
            return PartialView("Partial/" + name);
        }
    }
}
