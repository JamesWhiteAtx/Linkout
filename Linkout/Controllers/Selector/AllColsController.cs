using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Linkout.Controllers
{
    public class AllColsController : SelectorController
    {
        public HttpResponseMessage Get(string ptrnid)
        {
            //SelectorBuilder ub = new SelectorBuilder();
            //ub.AddQuery("type", "ptrncolors");
            //ub.AddQuery("ptrnid", ptrnid);
            //return GetSelectorJson(ub.Uri);
            AddQuery("type", "ptrncolors");
            AddQuery("ptrnid", ptrnid);
            return JsonResponse();
        }
    }
}