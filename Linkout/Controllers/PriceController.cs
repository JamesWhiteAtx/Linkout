using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;

namespace Linkout.Controllers
{
    public class PriceController : ApiController
    {
        private IPriceService priceService;

        public PriceController(IPriceService service)
        {
            priceService = service;
        }

        [HttpGet]
        public HttpResponseMessage Leather(int rows)
        {
            string json = JsonConvert.SerializeObject( priceService.GetLeatherPrice(rows) );
            return new HttpResponseMessage { Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json") };
        }

        [HttpGet]
        public HttpResponseMessage Heater()
        {
            string json = JsonConvert.SerializeObject( priceService.GetHeaterPrice() );
            return new HttpResponseMessage { Content = new StringContent(json, System.Text.Encoding.UTF8, "application/json") };
        }
    }
}
