using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Linkout.Controllers.Costco
{
    public class CostcoAdminController : Controller
    {
        //
        // GET: /Costco/
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
