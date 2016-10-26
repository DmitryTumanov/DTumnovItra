using System;
using Microsoft.AspNet.SignalR;
using OnlinerTask.Data.Hubs;
using OnlinerTask.Data.RedisManager.RedisServer.RedisRequests;
using OnlinerTask.Data.ScheduleModels;
using ServiceStack;
using ServiceStack.Redis;

namespace OnlinerTask.Data.RedisManager.RedisServer
{
    public class NotificationService : Service
    {
        private readonly IEmailManager manager;
        private readonly IHubContext hub;

        public NotificationService()
        {
            manager = new EmailCacheManager(new RedisClient("localhost", 6379));
            hub = GlobalHost.ConnectionManager.GetHubContext<PersonalHub>();
        }

        public object Any(UsersUpdateEmail req)
        {
            req.Id = Guid.NewGuid().ToString();
            manager.Set(req);
            return new object();
        }

        public object Any(ChangeTimeRequest req)
        {
            hub.Clients.All.changeTime(req.Message);
            return new object();
        }

        public object Any(AddProductRequest req)
        {
            hub.Clients.All.addProduct(req.Message, req.RedirectPath);
            return new object();
        }

        public object Any(RemoveProductRequest req)
        {
            hub.Clients.All.deleteProduct(req.Message, req.RedirectPath);
            return new object();
        }
    }
}
