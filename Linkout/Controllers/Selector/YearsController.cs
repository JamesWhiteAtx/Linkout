using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Linkout.Controllers
{
    public class YearsController : SelectorController
    {
        public HttpResponseMessage Get(string makeid)
        {
            //SelectorBuilder ub = new SelectorBuilder();
            //ub.AddQuery("type", "years");
            //ub.AddQuery("makeid", makeid);
            //return GetSelectorJson(ub.Uri);
            AddQuery("type", "years");
            AddQuery("makeid", makeid);
            return JsonResponse();
        }
    }
}
