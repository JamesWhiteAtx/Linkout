using System;
using System.Collections.Generic;
using System.Net.Http;
using Linkout.Services;

namespace Linkout.Controllers
{
    public class MakesController : SelectorController
    {
        public HttpResponseMessage Get()
        {
            //SelectorBuilder ub = new SelectorBuilder();
            //ub.AddQuery("type", "makes");
            //return GetSelectorJson(ub.Uri);
            AddQuery("type", "makes");
            return JsonResponse();
        }
    }
}
