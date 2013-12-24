using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Linkout.Controllers
{
    public class CarsController : SelectorController
    {
        public HttpResponseMessage Get(string makeid, string year, string modelid, string bodyid, string trimid, int ctlg = 0)
        {
            SetType("cars");
            SetCtlg(ctlg);
            AddQuery("makeid", makeid);
            AddQuery("year", year);
            AddQuery("modelid", modelid);
            AddQuery("bodyid", bodyid);
            AddQuery("trimid", trimid);
            return JsonResponse();
        }
    }
}
