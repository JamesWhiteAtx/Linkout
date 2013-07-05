using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;

namespace Linkout.Controllers
{
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
    }
}
