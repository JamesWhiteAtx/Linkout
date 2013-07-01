using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Linkout.Services
{
    public interface ITestService
    {
        string GetStr();
    }

    public class TestService : ITestService
    {
        public string GetStr()
        {
            return "test service";
        }
    }
}