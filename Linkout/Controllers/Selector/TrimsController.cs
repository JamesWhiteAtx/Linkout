using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Linkout.Controllers
{
    public class TrimsController : SelectorController
    {
        public HttpResponseMessage Get(string makeid, string year, string modelid, string bodyid)
        {
            //SelectorBuilder ub = new SelectorBuilder();
            //ub.AddQuery("type", "trims");
            //ub.AddQuery("makeid", makeid);
            //ub.AddQuery("year", year);
            //ub.AddQuery("modelid", modelid);
            //ub.AddQuery("bodyid", bodyid);
            //return GetSelectorJson(ub.Uri);

            AddQuery("type", "trims");
            AddQuery("makeid", makeid);
            AddQuery("year", year);
            AddQuery("modelid", modelid);
            AddQuery("bodyid", bodyid);
            return JsonResponse();
        }
    }
}
