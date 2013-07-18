using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;

namespace Linkout.Controllers
{
    public class ProdDTO
    {
        public string Description { get; set; }
        public string Price { get; set; }
    }

    public class ProductController : ApiController
    {
        private IProductService _productService;
        private IJsonHttpResponseService _jsonHttpResponseService;

        public ProductController(IProductService productService, IJsonHttpResponseService jsonHttpResponseService)
        {
            _productService = productService;
            _jsonHttpResponseService = jsonHttpResponseService;
        }

        //
        // GET: /Product/
        [HttpGet]
        public HttpResponseMessage Listing()
        {
            return _jsonHttpResponseService.GetObjectHttpResponseMessage(_productService.Listing());
        }

        // POST product
        public void Post([FromBody]ProdDTO prod)
        {
            var save = prod;
        }

        // POST product/5
        public void Post(int id, [FromBody]ProdDTO prod)
        {
            var update = prod;
        }

    }
}
