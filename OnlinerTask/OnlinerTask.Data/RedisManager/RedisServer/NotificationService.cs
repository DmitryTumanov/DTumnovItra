using System;
using System.Web.Mvc;
using OnlinerTask.Data.DataBaseModels;
using OnlinerTask.Data.ElasticSearch.ProductLogger;
using OnlinerTask.Data.Notifications;
using OnlinerTask.Data.RedisManager.RedisServer.RedisRequests.Implementations;
using OnlinerTask.Data.ScheduleModels;
using OnlinerTask.Data.SearchModels;
using ServiceStack;

namespace OnlinerTask.Data.RedisManager.RedisServer
{
    public class NotificationService : Service
    {
        private readonly IEmailManager manager;
        private readonly INotificator notificator;
        private readonly IProductLogger<ProductModel> addProductLogger;
        private readonly IProductLogger<Product> removeProductLogger;

        public NotificationService()
        {
            manager = DependencyResolver.Current.GetService<IEmailManager>();
            notificator = DependencyResolver.Current.GetService<INotificator>();
            addProductLogger = DependencyResolver.Current.GetService<IProductLogger<ProductModel>>();
            removeProductLogger = DependencyResolver.Current.GetService<IProductLogger<Product>>();
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

        public object Any(ProductModel req)
        {
            addProductLogger.LogObject(req);
            return new object();
        }

        public object Any(Product req)
        {
            removeProductLogger.LogObject(req);
            return new object();
        }
    }
}
