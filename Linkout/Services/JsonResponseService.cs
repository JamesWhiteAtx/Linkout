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
        string GetServerJsonResponse(Uri uri, string method, WebHeaderCollection headers);
        string GetServerJsonResponse(Uri uri);
        HttpResponseMessage GetHttpJsonResponse(Uri uri, string method, WebHeaderCollection headers);
        HttpResponseMessage GetHttpJsonResponse(Uri uri);
    }

    public class JsonWebResponseService : IJsonWebResponseService
    {
        private IJsonHttpResponseService _jsonHttpResponseService;

        public JsonWebResponseService(IJsonHttpResponseService jsonHttpResponseService)
        {
            _jsonHttpResponseService = jsonHttpResponseService;
        }

        public string ResponseFromServer(WebRequest request)
        {
            string responseFromServer = String.Empty;
            using (WebResponse response = request.GetResponse())
            {
                using (Stream dataStream = response.GetResponseStream())
                {
                    StreamReader reader = new StreamReader(dataStream);
                    responseFromServer = reader.ReadToEnd();
                }
            }
            return responseFromServer;
        }

        public string GetServerJsonResponse(Uri uri, string method, WebHeaderCollection headers)
        {
            WebRequest request = WebRequest.Create(uri);
            
            request.Method = method;
            request.ContentType = "application/json; charset=utf-8";

            if (headers != null)
            {
                foreach (string name in headers)
                {
                    request.Headers.Add(name, headers[name]);
                }
            }
            
            return ResponseFromServer(request);
        }

        public string GetServerJsonResponse(Uri uri)
        {
            return GetServerJsonResponse(uri, "GET", null);
        }

        public HttpResponseMessage GetHttpJsonResponse(Uri uri, string method, WebHeaderCollection headers)
        {
            string responseFromServer = GetServerJsonResponse(uri, method, headers);
            return _jsonHttpResponseService.GetStringHttpResponseMessage(responseFromServer);
        }

        public HttpResponseMessage GetHttpJsonResponse(Uri uri)
        {
            return GetHttpJsonResponse(uri, "GET", null);
        }
    }
}