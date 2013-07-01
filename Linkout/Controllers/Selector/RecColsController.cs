using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Linkout.Controllers
{
    public class RecColsController : SelectorController
    {
        public HttpResponseMessage Get(string carid, string ptrnid, string intcolid)
        {
            //SelectorBuilder ub = new SelectorBuilder();
            //ub.AddQuery("type", "ptrncolors");
            //ub.AddQuery("carid", carid);
            //ub.AddQuery("ptrnid", ptrnid);
            //ub.AddQuery("intcolid", intcolid);
            //return GetSelectorJson(ub.Uri);

            AddQuery("type", "ptrncolors");
            AddQuery("carid", carid);
            AddQuery("ptrnid", ptrnid);
            AddQuery("intcolid", intcolid);
            return JsonResponse();
        }
    }
}