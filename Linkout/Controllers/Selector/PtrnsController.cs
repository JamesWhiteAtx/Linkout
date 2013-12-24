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
        public HttpResponseMessage Get(string carid, int ctlg = 0)
        {
            SetType("carptrns");
            SetCtlg(ctlg);
            AddQuery("carid", carid);
            return JsonResponse();
        }
    }
}
