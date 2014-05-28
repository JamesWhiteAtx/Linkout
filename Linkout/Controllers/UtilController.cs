using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Linkout.Controllers
{
    public class UtilController : Controller
    {
        //
        // GET: /Util/
        [Authorize]
        public ActionResult Index()
        {
            return View();
        }

        private ActionResult RedirectToHashActId(string id)
        {
            string afterHash = ControllerContext.RouteData.GetRequiredString("action");
            if (id != null)
            {
                afterHash = afterHash + "?id=" + id;
            }
            return Redirect(Url.Action("Index") + "#" + afterHash);
        }

        // GET: /Configure/ItemCars
        [Authorize]
        public ActionResult ItemCars(string id)
        {
            return RedirectToHashActId(id);
        }

        public ActionResult Partials(string name)
        {
            return PartialView("Partials/" + name);
        }

    }
}
