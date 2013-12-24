using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Linkout.Controllers
{
    public class BodiesController : SelectorController
    {
        public HttpResponseMessage Get(string makeid, string year, string modelid, int ctlg = 0)
        {
            SetType("bodies");
            SetCtlg(ctlg);
            AddQuery("makeid", makeid);
            AddQuery("year", year);
            AddQuery("modelid", modelid);
            return JsonResponse();
        }
    }
}
