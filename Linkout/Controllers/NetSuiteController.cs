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
            
            if (typeStr == "imgbase") 
            {
                return _jsonHttpResponseService.GetStringHttpResponseMessage(_nsService.getUrlImageHost());
            }
            else if (typeStr == "custrecmake") 
            {
                return _jsonHttpResponseService.GetStringHttpResponseMessage(_nsService.getUrlCustRecMake());
            }
            else if (typeStr == "custrecmodel")
            {
                return _jsonHttpResponseService.GetStringHttpResponseMessage(_nsService.getUrlCustRecModel());
            }
            else if (typeStr == "custrecbody")
            {
                return _jsonHttpResponseService.GetStringHttpResponseMessage(_nsService.getUrlCustRecBody());
            }
            else if (typeStr == "custrectrim")
            {
                return _jsonHttpResponseService.GetStringHttpResponseMessage(_nsService.getUrlCustRecTrim());
            }
            else if (typeStr == "custreccar")
            {
                return _jsonHttpResponseService.GetStringHttpResponseMessage(_nsService.getUrlCustRecCar());
            }
            else if (typeStr == "custrecpattern")
            {
                return _jsonHttpResponseService.GetStringHttpResponseMessage(_nsService.getUrlCustRecPattern());
            }
            else if (typeStr == "item")
            {
                return _jsonHttpResponseService.GetStringHttpResponseMessage(_nsService.getUrlItem());
            }

            else if (typeStr == "webstoreitem")
            {
                var x = _jsonHttpResponseService.GetObjectHttpResponseMessage(new { prefix = "http://shopping.sandbox.netsuite.com/s.nl/c.801095/it.A/id.", suffix = "/.f" });
                return x;
            }
            else {
                return _jsonHttpResponseService.GetStringHttpResponseMessage(String.Empty);
            }
            
        }
    }
}
