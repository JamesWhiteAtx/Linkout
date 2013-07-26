using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
//using CST.Localization;
using CST.Security;
using CST.Security.Data;
using Linkout;

namespace Linkout.Controllers
{
    public class AccountController : SecurityController
    {
        public AccountController(SecurityEntities securityDbContext) : base(securityDbContext) { }
        
        //
        // GET: /Account/Index
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View();
        }

        //
        // GET: /Account/LogOn
        [AllowAnonymous]
        public ActionResult LogOn(string returnUrl)
        {
            return GetLogOnAction("Unauthorized");
        }

        //
        // POST: /Account/LogOn
        [HttpPost]
        [AllowAnonymous]
        public ActionResult LogOn(LogOnModel model, string returnUrl)
        {
            return PostLogOnAction(model, "LogonChangePassword", "Index", "Home");
        }

        //
        // GET: /Account/LogOff
        [AllowAnonymous]
        [Authorize]
        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();
            ClearUserSettings();

            return RedirectToAction("Index", "Home");
        }

    }
}
