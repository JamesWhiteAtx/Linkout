using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Linkout.Controllers
{
    public class PtrnsController : SelectorController
    {
        public HttpResponseMessage Get(string carid)
        {
            AddQuery("type", "carptrns");
            AddQuery("carid", carid);
            return JsonResponse();
        }
    }
}
