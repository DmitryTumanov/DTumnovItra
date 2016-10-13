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
    using Data.Repository;
    using OnlinerTask.Data.EntityMappers;
    using Data.EntityMappers.Interfaces;
    using Data.SearchModels;
    using Data.DataBaseModels;

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
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<ISearchService>().To<SearchService>();
            kernel.Bind<IRepository>().To<MsSQLRepository>();
            kernel.Bind(typeof(IProductMapper<,>)).To<ProductMapper>().WhenInjectedInto<MsSQLRepository>();
            kernel.Bind<IDependentMapper<Image, ImageModel>>().To<ImageMapper>();
            kernel.Bind<IDependentMapper<Review, ReviewModel>>().To<ReviewMapper>();
            kernel.Bind(typeof(IPriceMapper<,>)).To(typeof(PriceMapper)).WhenInjectedInto<ProductMapper>();
            kernel.Bind(typeof(IDependentMapper<,>)).To(typeof(OfferMapper)).WhenInjectedInto<PriceMapper>();
            kernel.Bind(typeof(IPriceAmmountMapper<,>)).To(typeof(PriceAmmountMapper)).WhenInjectedInto<PriceMapper>();
        }        
    }
}
