using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Linkout.Controllers
{
    public class ModelsController : SelectorController
    {
        public HttpResponseMessage Get(string makeid, string year)
        {
            //SelectorBuilder ub = new SelectorBuilder();
            //ub.AddQuery("type", "models");
            //ub.AddQuery("makeid", makeid);
            //ub.AddQuery("year", year);
            //return GetSelectorJson(ub.Uri);
            AddQuery("type", "models");
            AddQuery("makeid", makeid);
            AddQuery("year", year);
            return JsonResponse();
        }
    }
}
