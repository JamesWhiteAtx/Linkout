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
            AddQuery("type", "ptrnrecs");
            AddQuery("carid", carid);
            AddQuery("ptrnid", ptrnid);
            AddQuery("intcolid", intcolid);
            return JsonResponse();
        }
    }
}