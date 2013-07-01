using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Linkout.Services;

namespace Linkout.Controllers
{
    public class ValuesController : ApiController
    {
        ITestService service;
        public ValuesController(ITestService srvc)
	    {
            service = srvc;
    	}

        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2", service.GetStr() };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}