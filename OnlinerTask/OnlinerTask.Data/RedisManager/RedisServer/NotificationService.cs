using System;
using System.Web.Mvc;
using OnlinerTask.Data.Notifications;
using OnlinerTask.Data.RedisManager.RedisServer.RedisRequests;
using OnlinerTask.Data.ScheduleModels;
using ServiceStack;

namespace OnlinerTask.Data.RedisManager.RedisServer
{
    public class NotificationService : Service
    {
        private readonly IEmailManager manager;
        private readonly INotificator notificator;

        public NotificationService()
        {
            manager = DependencyResolver.Current.GetService<IEmailManager>();
            notificator = DependencyResolver.Current.GetService<INotificator>();
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
    }
}
