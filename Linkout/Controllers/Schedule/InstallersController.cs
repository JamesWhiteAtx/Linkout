using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;

namespace Linkout.Controllers
{
    public class InstallersController : ApiController
    {
        private IJsonHttpResponseService _jsonHttpResponseService;

        public InstallersController(IJsonHttpResponseService jsonHttpResponseService)
        {
            _jsonHttpResponseService = jsonHttpResponseService;
        }

        public HttpResponseMessage Get(string zipcode = null)
        {
            HttpResponseMessage respMsg = _jsonHttpResponseService.GetStringHttpResponseMessage(String.Empty);

            if (!String.IsNullOrWhiteSpace(zipcode)) {
                var list = new[] {
                    new { id = 0, miles = 0.5, descr = "First Location (Street Address)" },
                    new { id = 1, miles = 1.3, descr = "Second Location (Street Address)" },
                    new { id = 2, miles = 2.0, descr = "Third Location (Street Address)" },
                    new { id = 3, miles = 2.1, descr = "Fourth Location (Street Address)" },
                    new { id = 4, miles = 2.4, descr = "Fifth Location (Street Address)" },
                    new { id = 5, miles = 3.1, descr = "Sixth Location (Street Address)" },
                    new { id = 6, miles = 3.3, descr = "Seventh Location (Street Address)" },
                    new { id = 7, miles = 7.0, descr = "Eighth Location (Street Address)" },
                    new { id = 8, miles = 10.0, descr = "Ninth Location (Street Address)" },
                    new { id = 9, miles = 22.0, descr = "Tenth Location (Street Address)" },
                };
                respMsg = _jsonHttpResponseService.GetObjectHttpResponseMessage(list);
            }
            return respMsg;
        }
    }
}
