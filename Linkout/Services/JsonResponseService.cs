using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using System.Net;
using System.IO;

namespace Linkout.Services
{
    public interface IJsonResponseService
    {
        HttpResponseMessage GetSelectorJson(Uri uri);
    }

    public class JsonResponseService : IJsonResponseService
    {
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
            return new HttpResponseMessage { Content = new StringContent(responseFromServer, System.Text.Encoding.UTF8, "application/json") };
        }
    }
}