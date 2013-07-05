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
        private IPriceService _priceService;
        private IJsonHttpResponseService _jsonHttpResponseService;

        public PriceController(IPriceService service, IJsonHttpResponseService jsonHttpResponseService)
        {
            _priceService = service;
            _jsonHttpResponseService = jsonHttpResponseService;
        }

        [HttpGet]
        public HttpResponseMessage Leather(int rows)
        {
            return _jsonHttpResponseService.GetObjectHttpResponseMessage( _priceService.GetLeatherPrice(rows) );
        }

        [HttpGet]
        public HttpResponseMessage Heater()
        {
            return _jsonHttpResponseService.GetObjectHttpResponseMessage(_priceService.GetHeaterPrice());
        }
    }
}
