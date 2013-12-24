using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Linkout.Controllers
{
    public class IntColsController : SelectorController
    {
        public HttpResponseMessage Get(string carid, string ptrnid, int ctlg = 0)
        {
            SetType("carintcols");
            SetCtlg(ctlg);
            AddQuery("carid", carid);
            AddQuery("ptrnid", ptrnid);
            return JsonResponse();
        }
    }
}
