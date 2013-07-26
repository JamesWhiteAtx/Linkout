using System.Web;
using System.Configuration;
using System.Web.Configuration;

namespace Linkout
{
    public class NetSuiteConfiguration : ConfigurationSection
    {
        ConfigurationProperty _Uri;

        public NetSuiteConfiguration()
        {
            _Uri = new ConfigurationProperty("Uri", typeof(UriElement), null);

            this.Properties.Add(_Uri);
        }

        public UriElement Uri
        {
            get { return this[_Uri] as UriElement; }
            set { this[_Uri] = value; }
        }
    }

    public interface INetsuiteUriService
    {
        string Scheme { get; set; }
        string FormsHost { get; set; }
        string SysHost { get; set; }
        string ScriptPath { get; set; }
        string CustRecPath { get; set; }
        string ItemPath { get; set; }
        
        string SelScriptVal { get; set; }
        string SelDeployVal { get; set; }
        string CompidVal { get; set; }
        string HVal { get; set; }
    }

    public class UriElement : ConfigurationElement, INetsuiteUriService
    {
        public UriElement()
        {
            _Scheme = new ConfigurationProperty("scheme", typeof(string), "<UNDEFINED>");
            _FormsHost = new ConfigurationProperty("formsHost", typeof(string), "<UNDEFINED>");
            _SysHost = new ConfigurationProperty("sysHost", typeof(string), "<UNDEFINED>");
            _ScriptPath = new ConfigurationProperty("scriptPath", typeof(string), "<UNDEFINED>");
            _CustRecPath = new ConfigurationProperty("custRecPath", typeof(string), "<UNDEFINED>");
            _ItemPath = new ConfigurationProperty("itemPath", typeof(string), "<UNDEFINED>");
            _CompidVal = new ConfigurationProperty("compidVal", typeof(string), "<UNDEFINED>");
            _HVal = new ConfigurationProperty("hVal", typeof(string), "<UNDEFINED>");

            _SelScriptVal = new ConfigurationProperty("selScriptVal", typeof(string), "<UNDEFINED>");
            _SelDeployVal = new ConfigurationProperty("selDeployVal", typeof(string), "<UNDEFINED>");

            this.Properties.Add(_Scheme);
            this.Properties.Add(_FormsHost);
            this.Properties.Add(_SysHost);
            this.Properties.Add(_ScriptPath);
            this.Properties.Add(_CustRecPath);
            this.Properties.Add(_ItemPath);
            this.Properties.Add(_SelScriptVal);
            this.Properties.Add(_SelDeployVal);
            this.Properties.Add(_CompidVal);
            this.Properties.Add(_HVal);
        }

        ConfigurationProperty _Scheme; 
        public string Scheme { get { return (string)this[_Scheme]; } set { this[_Scheme] = value; } }
        
        ConfigurationProperty _FormsHost; 
        public string FormsHost { get { return (string)this[_FormsHost]; } set { this[_FormsHost] = value; } }
        
        ConfigurationProperty _SysHost; 
        public string SysHost { get { return (string)this[_SysHost]; } set { this[_SysHost] = value; } }
        
        ConfigurationProperty _ScriptPath; 
        public string ScriptPath { get { return (string)this[_ScriptPath]; } set { this[_ScriptPath] = value; } }
        
        ConfigurationProperty _CustRecPath; 
        public string CustRecPath { get { return (string)this[_CustRecPath]; } set { this[_CustRecPath] = value; } }
        
        ConfigurationProperty _ItemPath; 
        public string ItemPath { get { return (string)this[_ItemPath]; } set { this[_ItemPath] = value; } }
        
        ConfigurationProperty _CompidVal; 
        public string CompidVal { get { return (string)this[_CompidVal]; } set { this[_CompidVal] = value; } }
        
        ConfigurationProperty _HVal; 
        public string HVal { get { return (string)this[_HVal]; } set { this[_HVal] = value; } }

        ConfigurationProperty _SelScriptVal;
        public string SelScriptVal { get { return (string)this[_SelScriptVal]; } set { this[_SelScriptVal] = value; } }

        ConfigurationProperty _SelDeployVal;
        public string SelDeployVal { get { return (string)this[_SelDeployVal]; } set { this[_SelDeployVal] = value; } }
    }

}