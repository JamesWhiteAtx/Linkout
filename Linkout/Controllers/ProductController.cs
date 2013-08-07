using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net.Http;
using System.Web.Http;
using Newtonsoft.Json;
using Linkout.Models;

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
        // GET: /Product/Listing
        [HttpGet]
        public HttpResponseMessage Listing()
        {
            return _jsonHttpResponseService.GetObjectHttpResponseMessage(_productService.Listing());
        }

        // POST product
        public void Post([FromBody]ProdDescrPriceModel prod)
        {
            var save = prod;
        }

        // POST product/5
        public void Post(int id, [FromBody]ProdDescrPriceModel prod)
        {
            _productService.Update(id, prod.Description, prod.Price);
        }

    }
}
