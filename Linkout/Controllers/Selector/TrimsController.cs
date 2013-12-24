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
        public HttpResponseMessage Get(string makeid, string year, string modelid, string bodyid, int ctlg = 0)
        {
            SetType("trims");
            SetCtlg(ctlg);
            AddQuery("makeid", makeid);
            AddQuery("year", year);
            AddQuery("modelid", modelid);
            AddQuery("bodyid", bodyid);
            return JsonResponse();
        }
    }
}
