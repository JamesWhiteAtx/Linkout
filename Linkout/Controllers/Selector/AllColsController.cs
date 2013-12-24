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
        public HttpResponseMessage Get(string ptrnid, int ctlg = 0)
        {
            SetType("ptrnkits");
            SetCtlg(ctlg);
            AddQuery("ptrnid", ptrnid);
            return JsonResponse();
        }
    }
}