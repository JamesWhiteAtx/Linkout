[assembly: WebActivator.PreApplicationStartMethod(typeof(Linkout.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivator.ApplicationShutdownMethodAttribute(typeof(Linkout.App_Start.NinjectWebCommon), "Stop")]

namespace Linkout.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;

	using System.Web.Http;
	using NinjectWebApi;
    using Linkout;
    using CST.Security.Data;
    using System.Configuration;

    using ComHub;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
            kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

            RegisterServices(kernel);

            GlobalConfiguration.Configuration.DependencyResolver = new NinjectResolver(kernel);

            return kernel;
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<ITestService>().To<TestService>();

            kernel.Bind<SecurityEntities>().ToSelf().InRequestScope();

            kernel.Bind<IJsonHttpResponseService>().To<JsonHttpResponseService>();
            kernel.Bind<IJsonWebResponseService>().To<JsonWebResponseService>();
            kernel.Bind<IConfigurationService>().To<ConfigurationService>();
            //kernel.Bind<IPriceService>().To<PriceService>();
            kernel.Bind<IProductService>().To<ProductService>();

            kernel.Bind<INetSuiteUriSelectorService>().To<NetSuiteUriScriptSelector>();
            kernel.Bind<INetSuiteUriFileService>().To<NetSuiteUriScriptFile>();
            kernel.Bind<INetSuiteCcOrderUriService>().To<NetSuiteCcOrderUriService>();
            kernel.Bind<IHttpRespPassThruService>().To<HttpRespPassThruService>();

            kernel.Bind<INetSuiteUriService>().To<NetSuiteUriService>();

            kernel.Bind<INetSuiteConfigService>().ToMethod(
                m => {
                    NetSuiteConfiguration nsSect = ConfigurationManager.GetSection(NetSuiteConfiguration.ConfigSectionName) as NetSuiteConfiguration;
                    return nsSect.Uri;
                }
            );

            kernel.Bind<IAppSettingsService>().To<AppSettingsService>();
            kernel.Bind<ICHFtpService>().To<CHFtpService>();
            kernel.Bind<IGnuPGService>().To<GnuPGService>();
            kernel.Bind<IFileService>().To<FileService>();
            kernel.Bind<IComHubModelService>().To<ComHubModelService>();

            kernel.Bind<IContentService>().To<ContentService>();
            kernel.Bind<ICcProductService>().To<CcProductService>();
            kernel.Bind<ICcOrderService>().To<CcOrderService>();


        }        
    }
}
