using System;
using OnlinerTask.Data.RedisManager.RedisServer.RedisRequests;
using OnlinerTask.Data.ScheduleModels;
using OnlinerTask.Data.Sockets;
using ServiceStack;
using ServiceStack.Redis;

namespace OnlinerTask.Data.RedisManager.RedisServer
{
    public class NotificationService : Service
    {
        private readonly IEmailManager manager;
        private readonly NetSocket socket;

        public NotificationService()
        {
            manager = new EmailCacheManager(new RedisClient("localhost", 6379));
            socket = new NetSocket();
        }

        public object Any(UsersUpdateEmail req)
        {
            req.Id = Guid.NewGuid().ToString();
            manager.Set(req);
            return new object();
        }

        public object Any(ChangeTimeRequest req)
        {
            socket.ChangeInfo(req.Message);
            return new object();
        }

        public object Any(AddProductRequest req)
        {
            socket.AddProduct(req.Message);
            return new object();
        }

        public object Any(RemoveProductRequest req)
        {
            socket.RemoveProduct(req.Message);
            return new object();
        }
    }
}
