using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Net;
using System.IO;

namespace Linkout
{
    public interface IHttpRespPassThruService
    {
        //string GetServerJsonResponse(Uri uri, string method, WebHeaderCollection headers);
        //string GetServerJsonResponse(Uri uri);
        HttpResponseMessage GetHttpResponse(Uri uri, string method, WebHeaderCollection headers);
        HttpResponseMessage GetHttpResponse(Uri uri);
    }

    public class HttpRespPassThruService : IHttpRespPassThruService
    {
        public HttpResponseMessage GetHttpResponse(Uri uri, string method, WebHeaderCollection headers)
        {
            WebRequest request = WebRequest.Create(uri);
            request.Method = method;
            if (headers != null)
            {
                foreach (string name in headers)
                {
                    request.Headers.Add(name, headers[name]);
                }
            }
            
            using (WebResponse respFromServer = request.GetResponse())
            {
                using (Stream dataStream = respFromServer.GetResponseStream())
                {
                    MemoryStream ms = new MemoryStream();
                    dataStream.CopyTo(ms);

                    HttpResponseMessage response = new HttpResponseMessage();
                    response.StatusCode = HttpStatusCode.OK;
                    ms.Position = 0;
                    response.Content = new StreamContent(ms);
                    response.Content.Headers.Add("Content-Type", respFromServer.ContentType);

                    return response;
                }
            }
        }

        public HttpResponseMessage GetHttpResponse(Uri uri)
        {
            return GetHttpResponse(uri, "GET", null);
        }
    }
}