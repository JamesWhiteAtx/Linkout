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
    using Linkout.Services;
    using CST.Security.Data;


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
            kernel.Bind<INetSuiteUriService>().To<NetSuiteUriService>();
            kernel.Bind<IConfigurationService>().To<ConfigurationService>();
            kernel.Bind<IPriceService>().To<PriceService>();
            kernel.Bind<IProductService>().To<ProductService>();
        }        
    }
}
