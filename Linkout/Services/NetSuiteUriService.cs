using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Linkout.Services
{
    //var url = "https://forms.sandbox.netsuite.com/app/site/hosting/scriptlet.nl?script=32&deploy=1&compid=801095&h=20a61f1484463b5b9654&type=makes";
    //var url = "https://forms.sandbox.netsuite.com/app/site/hosting/scriptlet.nl?script=7&deploy=1&compid=801095&h=d8da884e8430a0278dc1&stage=getmakes";

    public interface INetSuiteUriService
    {
        NetSuiteUriService AddQuery(string name, string value);
        Uri Uri { get; }
    }

    public class NetSuiteUriService : UriBuilder, INetSuiteUriService
    {
        public static readonly string NsBaseUri = "https://forms.sandbox.netsuite.com/app/site/hosting/scriptlet.nl";
        public static readonly string NsScriptName = "script";
        public static readonly string NsScriptVal = "32";
        public static readonly string NsDeployName = "deploy";
        public static readonly string NsDeployVal = "1";
        public static readonly string NsCompidName = "compid";
        public static readonly string NsCompidVal = "801095";
        public static readonly string NsHName = "h";
        public static readonly string NsHVal = "20a61f1484463b5b9654";


        public NetSuiteUriService()
            : base(NsBaseUri)
        {
            this.AddQuery(NsScriptName, NsScriptVal)
                .AddQuery(NsDeployName, NsDeployVal)
                .AddQuery(NsCompidName, NsCompidVal)
                .AddQuery(NsHName, NsHVal);
        }

        public NetSuiteUriService AddQuery(string name, string value)
        {
            var queryString = HttpUtility.ParseQueryString(this.Query);
            queryString.Add(name, value);
            this.Query = queryString.ToString();
            return this;
        }

    }
}