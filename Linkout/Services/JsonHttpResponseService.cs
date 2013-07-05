using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Http;
using Newtonsoft.Json;

namespace Linkout
{
    public interface IJsonHttpResponseService
    {
        HttpResponseMessage GetStringHttpResponseMessage(string response);
        HttpResponseMessage GetObjectHttpResponseMessage(object response);
    }

    public class JsonHttpResponseService : IJsonHttpResponseService
    {
        public HttpResponseMessage GetStringHttpResponseMessage(string response)
        {
            return new HttpResponseMessage { Content = new StringContent(response, System.Text.Encoding.UTF8, "application/json") };
        }

        public HttpResponseMessage GetObjectHttpResponseMessage(object response)
        {
            string json = JsonConvert.SerializeObject(response);
            return GetStringHttpResponseMessage(json);
        }
    }

}