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
            //return new HttpResponseMessage { Content = new StringContent("ffffffffffffffff", System.Text.Encoding.UTF8, "application/json") };
            return _httpService.GetHttpResponse(_nsFileService.GetFileByIdUri(id));
            /*
            Uri uri = _nsFileService.GetFileByIdUri(id);
            //string s = uri.AbsoluteUri;
            //return new HttpResponseMessage { Content = new StringContent(s, System.Text.Encoding.UTF8, "application/json") };

            try
            {
                string method = "GET";
                WebHeaderCollection headers = null;

                //WebRequest request = WebRequest.Create(uri);
                WebRequest request = WebRequest.Create("http://i.dailymail.co.uk/i/pix/2013/07/16/article-2365170-054BDA35000005DC-54_306x423.jpg");
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
            catch (WebException webEx)
            {
                string msg = "webEx.ToString() = " + webEx.Status.ToString() +"| "+ webEx.ToString();
                // Now you can access webEx.Response object that contains more info on the server response              
                if (webEx.Status == WebExceptionStatus.ProtocolError)
                {
                    msg = ((HttpWebResponse)webEx.Response).StatusCode.ToString()
                        +" - "+ ((HttpWebResponse)webEx.Response).StatusDescription.ToString();
                }
                return new HttpResponseMessage { Content = new StringContent("msg err " + msg, System.Text.Encoding.UTF8, "application/json") };
            }
            catch (Exception e)
            {
                return new HttpResponseMessage { Content = new StringContent("jpw err "+e.ToString(), System.Text.Encoding.UTF8, "application/json") };
            }
            */
        }
    }
}
