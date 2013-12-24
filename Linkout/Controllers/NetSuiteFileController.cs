using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.IO;
using System.Net.Http.Headers;

namespace Linkout.Controllers
{
    public class NetSuiteFileController : ApiController
    {
        private INetSuiteUriFileService _nsFileService;
        private IHttpRespPassThruService _httpService;

        public NetSuiteFileController(INetSuiteUriFileService nsFileService, IHttpRespPassThruService httpService)
        {
            _nsFileService = nsFileService;
            _httpService = httpService;
        }

        // GET
        public HttpResponseMessage Get(string id)
        {
            return _httpService.GetHttpResponse(_nsFileService.GetFileByIdUri(id));
        }
    }
}
