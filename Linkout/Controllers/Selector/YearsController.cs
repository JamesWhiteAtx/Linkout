using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Linkout.Controllers
{
    public class YearsController : SelectorController
    {
        public HttpResponseMessage Get(string makeid, int ctlg = 0)
        {
            SetType("years");
            SetCtlg(ctlg);
            AddQuery("makeid", makeid);
            return JsonResponse();
        }
    }
}
