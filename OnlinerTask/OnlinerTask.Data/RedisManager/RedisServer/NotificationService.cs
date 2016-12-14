using System;
using System.Web.Mvc;
using OnlinerTask.Data.ElasticSearch.LoggerModels;
using OnlinerTask.Data.ElasticSearch.ProductLogger;
using OnlinerTask.Data.ElasticSearch.UserActivityLogger;
using OnlinerTask.Data.Notifications;
using OnlinerTask.Data.RedisManager.RedisServer.RedisRequests.Implementations;
using OnlinerTask.Data.ScheduleModels;
using ServiceStack;

namespace OnlinerTask.Data.RedisManager.RedisServer
{
    public class NotificationService : Service
    {
        private readonly IEmailManager manager;
        private readonly INotificator notificator;
        private readonly IProductLogger<AddedProductModel> addProductLogger;
        private readonly IProductLogger<RemovedProduct> removeProductLogger;
        private readonly IActivityLogger activityLogger;

        public NotificationService()
        {
            manager = DependencyResolver.Current.GetService<IEmailManager>();
            notificator = DependencyResolver.Current.GetService<INotificator>();
            addProductLogger = DependencyResolver.Current.GetService<IProductLogger<AddedProductModel>>();
            removeProductLogger = DependencyResolver.Current.GetService<IProductLogger<RemovedProduct>>();
            activityLogger = DependencyResolver.Current.GetService<IActivityLogger>();
        }

        public object Any(UsersUpdateEmail req)
        {
            req.Id = Guid.NewGuid().ToString();
            manager.Set(req);
            return new object();
        }

        public object Any(ChangeTimeRequest req)
        {
            notificator.ChangeInfo(req);
            return new object();
        }

        public object Any(AddProductRequest req)
        {
            notificator.AddProduct(req);
            return new object();
        }

        public object Any(RemoveProductRequest req)
        {
            notificator.RemoveProduct(req);
            return new object();
        }

        public object Any(AddedProductModel req)
        {
            addProductLogger.LogObject(req);
            return new object();
        }

        public object Any(RemovedProduct req)
        {
            removeProductLogger.LogObject(req);
            return new object();
        }

        public object Any(WebRequest req)
        {
            activityLogger.LogRequest(req);
            return new object();
        }
    }
}
