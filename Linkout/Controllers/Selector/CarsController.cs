using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Linkout.Controllers
{
    public class CarsController : SelectorController
    {
        public HttpResponseMessage Get(string makeid, string year, string modelid, string bodyid, string trimid)
        {
            //SelectorBuilder ub = new SelectorBuilder();
            //ub.AddQuery("type", "cars");
            //ub.AddQuery("makeid", makeid);
            //ub.AddQuery("year", year);
            //ub.AddQuery("modelid", modelid);
            //ub.AddQuery("bodyid", bodyid);
            //ub.AddQuery("trimid", trimid);
            //return GetSelectorJson(ub.Uri);

            AddQuery("type", "cars");
            AddQuery("makeid", makeid);
            AddQuery("year", year);
            AddQuery("modelid", modelid);
            AddQuery("bodyid", bodyid);
            AddQuery("trimid", trimid);
            return JsonResponse();
        }
    }
}
