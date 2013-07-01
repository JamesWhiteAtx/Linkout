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
        public HttpResponseMessage Get(string zipcode = null)
        {
            String json = String.Empty;
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
                json = JsonConvert.SerializeObject(list);
            }

            return new HttpResponseMessage { Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json") };
        }
    }
}
