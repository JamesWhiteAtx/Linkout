using System;

using System.Web.Configuration;
using System.Configuration;


namespace Linkout.Services
{
    public interface IConfigurationService
    {
        CompilationSection GetWebSysCompilationSection();
        bool GetWebSysCompilationSectionDebug();
    }

    public class ConfigurationService : IConfigurationService
    {
        public CompilationSection GetWebSysCompilationSection()
        {
            return ConfigurationManager.GetSection("system.web/compilation") as CompilationSection;
        }

        public bool GetWebSysCompilationSectionDebug()
        {
            CompilationSection section = GetWebSysCompilationSection();
            return (section.IfNotNull(s => s.Debug) == true);
        }
    }


}