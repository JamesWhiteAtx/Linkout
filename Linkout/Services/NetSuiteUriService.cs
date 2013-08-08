using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Linkout
{
    //var url = "https://forms.sandbox.netsuite.com/app/site/hosting/scriptlet.nl?script=32&deploy=1&compid=801095&h=20a61f1484463b5b9654&type=makes";
    //var url = "https://forms.sandbox.netsuite.com/app/site/hosting/scriptlet.nl?script=7&deploy=1&compid=801095&h=d8da884e8430a0278dc1&stage=getmakes";

    public class UriService : UriBuilder
    {
        public UriService AddQuery(string name, string value)
        {
            var queryString = HttpUtility.ParseQueryString(this.Query);
            queryString.Add(name, value);
            this.Query = queryString.ToString();
            return this;
        }
    }

    public class NetSuiteUriBase : UriService
    {
        private INetSuiteConfigService _nsConfigService;
        protected INetSuiteConfigService NsConfigService { get { return _nsConfigService; } }

        public NetSuiteUriBase(INetSuiteConfigService nsConfigService)
        {
            _nsConfigService = nsConfigService;
        }
    }

    public class NetSuiteUriScriptBase : NetSuiteUriBase
    {
        public static readonly string NsScriptName = "script";
        public static readonly string NsDeployName = "deploy";
        public static readonly string NsCompidName = "compid";
        public static readonly string NsHName = "h";

        public NetSuiteUriScriptBase(INetSuiteConfigService nsConfigService)
            : base(nsConfigService)
        {
            this.Scheme = NsConfigService.Scheme;
            this.Host = NsConfigService.FormsHost;
            this.Path = NsConfigService.ScriptPath;
        }
    }

    public interface INetSuiteUriSelectorService
    {
        UriService AddQuery(string name, string value);
        Uri Uri { get; }
    }

    public class NetSuiteUriScriptSelector : NetSuiteUriScriptBase, INetSuiteUriSelectorService
    {
        public NetSuiteUriScriptSelector(INetSuiteConfigService nsConfigService)
            : base(nsConfigService)
        {
            this.AddQuery(NsScriptName, NsConfigService.SelScriptVal)
                .AddQuery(NsDeployName, NsConfigService.SelDeployVal)
                .AddQuery(NsCompidName, NsConfigService.CompidVal)
                .AddQuery(NsHName, NsConfigService.HVal);
        }
    }

    public class NetSuiteUriSystemBase : NetSuiteUriBase
    {
        public NetSuiteUriSystemBase(INetSuiteConfigService nsConfigService)
            : base(nsConfigService)
        {
            this.Scheme = nsConfigService.Scheme;
            this.Host = nsConfigService.SysHost;
        }
    }

    public class NetSuiteUriCustRecord : NetSuiteUriSystemBase
    {
        public static readonly string NsRecTypeName = "rectype";
        public static readonly string NsRecIDName = "id";

        public NetSuiteUriCustRecord(INetSuiteConfigService nsConfigService)
            : base(nsConfigService)
        {
            this.Path = NsConfigService.CustRecPath;
        }

        public NetSuiteUriCustRecord SetTypeID(string type, string id=null) 
        { 
            AddQuery(NsRecTypeName, type);
            AddQuery(NsRecIDName, id);
            return this;
        }

        public string GetTypeIDUrl(string type, string id = null)
        {
            return SetTypeID(type, id).Uri.AbsoluteUri;
        }

        public string GetUrlMake(string id=null)
        {
            return GetTypeIDUrl(NsConfigService.MakeCustRecId);
        }
        public string GetUrlModel(string id = null)
        {
            return GetTypeIDUrl(NsConfigService.ModelCustRecId);
        }
        public string GetUrlBody(string id = null)
        {
            return GetTypeIDUrl(NsConfigService.BodyCustRecId);
        }
        public string GetUrlTrim(string id = null)
        {
            return GetTypeIDUrl(NsConfigService.TrimCustRecId);
        }
        public string GetUrlCar(string id = null)
        {
            return GetTypeIDUrl(NsConfigService.CarCustRecId);
        }
        public string GetUrlPattern(string id = null)
        {
            return GetTypeIDUrl(NsConfigService.PatternCustRecId);
        }
    }

    public class NetSuiteUriItem : NetSuiteUriSystemBase
    { 
        public static readonly string NsItemIDName = "id";

        public NetSuiteUriItem(INetSuiteConfigService nsConfigService)
            : base(nsConfigService)
        {
            this.Path = NsConfigService.ItemPath;
        }

        public NetSuiteUriItem SetID(string id = null)
        {
            AddQuery(NsItemIDName, id);
            return this;
        }
    }

    public interface INetSuiteUriService
    {
        string getUrlImageHost();
        string getUrlCustRecMake(string id = null);
        string getUrlCustRecModel(string id = null);
        string getUrlCustRecBody(string id = null);
        string getUrlCustRecTrim(string id = null);
        string getUrlCustRecCar(string id = null);
        string getUrlCustRecPattern(string id = null);
        string getUrlItem(string id = null);
    }

    public class NetSuiteUriService : INetSuiteUriService
    {
        private INetSuiteConfigService _nsConfigService;
        //protected INetSuiteConfigService NsConfigService { get { return _nsConfigService; } }

        public NetSuiteUriService(INetSuiteConfigService nsConfigService)
        {
            _nsConfigService = nsConfigService;
        }

        public string getUrlImageHost()
        {
            NetSuiteUriSystemBase bldr = new NetSuiteUriSystemBase(_nsConfigService);
            return bldr.Uri.AbsoluteUri;
        }

        public string getUrlCustRecMake(string id = null)
        {
            NetSuiteUriCustRecord bldr = new NetSuiteUriCustRecord(_nsConfigService);
            return bldr.GetUrlMake(id);
        }
        public string getUrlCustRecModel(string id = null)
        {
            NetSuiteUriCustRecord bldr = new NetSuiteUriCustRecord(_nsConfigService);
            return bldr.GetUrlModel(id);
        }
        public string getUrlCustRecBody(string id = null)
        {
            NetSuiteUriCustRecord bldr = new NetSuiteUriCustRecord(_nsConfigService);
            return bldr.GetUrlBody(id);
        }
        public string getUrlCustRecTrim(string id = null)
        {
            NetSuiteUriCustRecord bldr = new NetSuiteUriCustRecord(_nsConfigService);
            return bldr.GetUrlTrim(id);
        }
        public string getUrlCustRecCar(string id = null)
        {
            NetSuiteUriCustRecord bldr = new NetSuiteUriCustRecord(_nsConfigService);
            return bldr.GetUrlCar(id);
        }
        public string getUrlCustRecPattern(string id = null)
        {
            NetSuiteUriCustRecord bldr = new NetSuiteUriCustRecord(_nsConfigService);
            return bldr.GetUrlPattern(id);
        }
        public string getUrlItem(string id = null)
        {
            NetSuiteUriItem bldr = new NetSuiteUriItem(_nsConfigService);
            bldr.SetID(id);
            return bldr.Uri.AbsoluteUri;
        }
    }

    //public class NetSuiteUriService : UriBuilder, INetSuiteUriService
    //{
    //    //public static readonly string NsBaseUri = "https://forms.sandbox.netsuite.com/app/site/hosting/scriptlet.nl";
    //    public static readonly string NsScriptName = "script";
    //    //public static readonly string NsScriptVal = "32";
    //    public static readonly string NsDeployName = "deploy";
    //    //public static readonly string NsDeployVal = "1";
    //    public static readonly string NsCompidName = "compid";
    //    //public static readonly string NsCompidVal = "801095";
    //    public static readonly string NsHName = "h";
    //    //public static readonly string NsHVal = "20a61f1484463b5b9654";

    //    public static readonly string NsRecTypeName = "rectype";
    //    public static readonly string NsIdName = "id";

    //    private INetsuiteConfigService _uriService;

    //    public NetSuiteUriService(INetsuiteConfigService uriService)
    //        //: base(NsBaseUri)
    //    {
    //        _uriService = uriService;

    //        this.Scheme = uriService.Scheme;
    //        this.Host = uriService.FormsHost;
    //        this.Path = uriService.ScriptPath;
    //        this.AddQuery(NsScriptName, uriService.SelScriptVal)
    //            .AddQuery(NsDeployName, uriService.SelDeployVal)
    //            .AddQuery(NsCompidName, uriService.CompidVal)
    //            .AddQuery(NsHName, uriService.HVal);
    //    }

    //    public NetSuiteUriService AddQuery(string name, string value)
    //    {
    //        var queryString = HttpUtility.ParseQueryString(this.Query);
    //        queryString.Add(name, value);
    //        this.Query = queryString.ToString();
    //        return this;
    //    }

    //}
}