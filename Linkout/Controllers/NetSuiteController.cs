using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Linkout.Controllers
{
    public class NetSuiteController : ApiController
    {
        private INetSuiteUriService _nsService;
        
        private IJsonHttpResponseService _jsonHttpResponseService;
        
        public NetSuiteController(INetSuiteUriService nsService, IJsonHttpResponseService jsonHttpResponseService)
        {
            _nsService = nsService;
            _jsonHttpResponseService = jsonHttpResponseService;
        }
        
        // GET
        public HttpResponseMessage Get(string type)
        {
            string typeStr = type.Trim().ToLower();
            string url = String.Empty;
            
            if (typeStr == "imgbase") 
            {
                url = _nsService.getUrlImageHost();
            }
            else if (typeStr == "custrecmake") 
            {
                url = _nsService.getUrlCustRecMake();
            }
            else if (typeStr == "custrecmodel")
            {
                url = _nsService.getUrlCustRecModel();
            }
            else if (typeStr == "custrecbody")
            {
                url = _nsService.getUrlCustRecBody();
            }
            else if (typeStr == "custrectrim")
            {
                url = _nsService.getUrlCustRecTrim();
            }
            else if (typeStr == "custreccar")
            {
                url = _nsService.getUrlCustRecCar();
            }
            else if (typeStr == "custrecpattern")
            {
                url = _nsService.getUrlCustRecPattern();
            }
            else if (typeStr == "item")
            {
                url = _nsService.getUrlItem();
            }

            return _jsonHttpResponseService.GetStringHttpResponseMessage(url);
        }
    }
}
