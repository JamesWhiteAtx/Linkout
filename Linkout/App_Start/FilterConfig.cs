using System.Web;
using System.Web.Mvc;
using CST.Security;

namespace Linkout
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new LogonAuthorizeAttribute());
            filters.Add(new HandleErrorAttribute());
        }
    }
}