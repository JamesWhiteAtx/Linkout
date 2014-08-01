using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Linkout;
using CST.Security.Data;

namespace Linkout.Controllers
{
    [AllowAnonymous]
    public class HomeController : Controller
    {
        private IConfigurationService configService;

        public HomeController(IConfigurationService configsrvc)
        {
            configService = configsrvc;
        }

        public ActionResult Index()
        {
            if (configService.GetWebSysCompilationSectionDebug())
            {
                return View();
            }
            else
            {
                return Redirect("http://www.roadwire.com");
            }
        }

        public ActionResult About()
        {
            using (SecurityEntities securityEntities = new SecurityEntities())
            {
                ViewBag.DBName = securityEntities.DBNameObfuscate();
            }

            return View();
        }
    }
}
