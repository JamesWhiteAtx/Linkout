using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Linkout.Controllers
{
    public class BodiesController : SelectorController
    {
        public HttpResponseMessage Get(string makeid, string year, string modelid)
        {
            //SelectorBuilder ub = new SelectorBuilder();
            //ub.AddQuery("type", "bodies");
            //ub.AddQuery("makeid", makeid);
            //ub.AddQuery("year", year);
            //ub.AddQuery("modelid", modelid);
            //return GetSelectorJson(ub.Uri);

            AddQuery("type", "bodies");
            AddQuery("makeid", makeid);
            AddQuery("year", year);
            AddQuery("modelid", modelid);
            return JsonResponse();
        }
    }
}
