using Ninject.Syntax;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(OnlinerTask.WEB.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(OnlinerTask.WEB.App_Start.NinjectWebCommon), "Stop")]

namespace OnlinerTask.WEB.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using BLL.Services;
    using Data.EntityMappers;
    using Data.EntityMappers.Interfaces;
    using Data.SearchModels;
    using Data.DataBaseModels;
    using Data.Repository.Interfaces;
    using Data.Repository;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper Bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            Bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            Bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();

                // Web Api
                System.Web.Http.GlobalConfiguration.Configuration.DependencyResolver = new Ninject.WebApi.DependencyResolver.NinjectDependencyResolver(kernel);

                // MVC 
                System.Web.Mvc.DependencyResolver.SetResolver(new Ninject.Web.Mvc.NinjectDependencyResolver(kernel));

                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IBindingRoot kernel)
        {
            kernel.Bind<ISearchService>().To<SearchService>();
            kernel.Bind<IProductRepository>().To<MsSqlProductRepository>();
            kernel.Bind<IPersonalRepository>().To<MsSqlPersonalRepository>();
            kernel.Bind<IRepository>().To<MsSqlRepository>();
            kernel.Bind<ITimeServiceRepository>().To<MsSqlTimeServiceRepository>();
            kernel.Bind(typeof(IProductMapper<,>)).To<ProductMapper>();
            kernel.Bind<IDependentMapper<Image, ImageModel>>().To<ImageMapper>();
            kernel.Bind<IDependentMapper<Review, ReviewModel>>().To<ReviewMapper>();
            kernel.Bind(typeof(IPriceMapper<,>)).To(typeof(PriceMapper)).WhenInjectedInto<ProductMapper>();
            kernel.Bind(typeof(IDependentMapper<,>)).To(typeof(OfferMapper)).WhenInjectedInto<PriceMapper>();
            kernel.Bind(typeof(IPriceAmmountMapper<,>)).To(typeof(PriceAmmountMapper)).WhenInjectedInto<PriceMapper>();
        }        
    }
}
