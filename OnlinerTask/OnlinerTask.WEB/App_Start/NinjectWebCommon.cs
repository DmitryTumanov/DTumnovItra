using Ninject.Syntax;
using OnlinerTask.BLL.Services.ConfigChange;
using OnlinerTask.BLL.Services.ConfigChange.Implementations;
using OnlinerTask.BLL.Services.Job;
using OnlinerTask.BLL.Services.Job.Implementations;
using OnlinerTask.BLL.Services.Products;
using OnlinerTask.BLL.Services.Products.Implementations;
using OnlinerTask.BLL.Services.Search;
using OnlinerTask.BLL.Services.TimeChange;
using OnlinerTask.BLL.Services.TimeChange.Implementations;
using OnlinerTask.Data.DataBaseContexts;
using OnlinerTask.Data.DataBaseInterfaces;
using OnlinerTask.Data.EntityMappers.Implementations;
using OnlinerTask.Data.IdentityModels;
using OnlinerTask.Data.Notifications;
using OnlinerTask.Data.Notifications.Technologies;
using OnlinerTask.Data.Notifications.Technologies.Implementations;
using OnlinerTask.Data.RedisManager;
using OnlinerTask.Data.Repository.Implementations;
using OnlinerTask.Data.Resources;
using OnlinerTask.Data.Sockets;
using OnlinerTask.WEB.Controllers;
using ServiceStack.Redis;

[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(OnlinerTask.WEB.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(OnlinerTask.WEB.App_Start.NinjectWebCommon), "Stop")]

namespace OnlinerTask.WEB.App_Start
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using Data.EntityMappers;
    using Data.SearchModels;
    using Data.DataBaseModels;
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
            kernel.Bind<IRedisClient>().ToMethod(x => new RedisClient("localhost", 6379));
            kernel.Bind<IEmailManager>().To<EmailCacheManager>();
            kernel.Bind<IProductJob>().To<ProductJobService>();
            kernel.Bind<IEmailJob>().To<EmailJobService>();
            kernel.Bind<INotification>().To<NotifyJobService>();
            kernel.Bind<INotificator>().To<Notificator>();
            kernel.Bind<INotifyTechnology>().To<SignalRNotificator>().When(x=> Configurations.SignalRTechnology == Configurations.NotifyTechnology);
            kernel.Bind<INotifyTechnology>().To<NetMqNotificator>().When(x => Configurations.NetMqTechnology == Configurations.NotifyTechnology);
            kernel.Bind<ISocketLauncher>().To<SocketLauncher>();
            kernel.Bind<IManager>().To<PersonalManager>().WhenInjectedInto<PersonalController>();
            kernel.Bind<IManager>().To<ProductManager>().WhenInjectedInto<ProductController>();
            kernel.Bind<ITimeChanger>().To<TimeChanger>();
            kernel.Bind<IOnlinerContext>().To<OnlinerProducts>().InThreadScope();
            kernel.Bind<IUserContext>().To<ApplicationDbContext>().InThreadScope();
            kernel.Bind<ITechnologyChanger>().To<TechnologyChanger>();
        }        
    }
}
