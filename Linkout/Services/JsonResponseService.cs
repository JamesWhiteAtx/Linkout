using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Net;
using System.IO;

namespace Linkout
{
    public interface IJsonWebResponseService
    {
        HttpResponseMessage GetSelectorJson(Uri uri);
    }

    public class JsonWebResponseService : IJsonWebResponseService
    {
        private IJsonHttpResponseService _jsonHttpResponseService;

        public JsonWebResponseService(IJsonHttpResponseService jsonHttpResponseService)
        {
            _jsonHttpResponseService = jsonHttpResponseService;
        }

        public HttpResponseMessage GetSelectorJson(Uri uri)
        {
            string responseFromServer = String.Empty;
            WebRequest request = WebRequest.Create(uri);
            request.Method = "GET";
            request.ContentType = "application/json; charset=utf-8";
            using (WebResponse response = request.GetResponse())
            {
                using (Stream dataStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(dataStream);
                    responseFromServer = reader.ReadToEnd();
                }
            }
            return _jsonHttpResponseService.GetStringHttpResponseMessage(responseFromServer);
        }
    }
}