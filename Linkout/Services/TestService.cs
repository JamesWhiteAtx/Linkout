using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Linkout
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