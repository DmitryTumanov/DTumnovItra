using System;
using OnlinerTask.Data.ScheduleModels;
using ServiceStack;
using ServiceStack.Redis;

namespace OnlinerTask.Data.RedisManager.RedisServer
{
    public class EmailService : Service
    {
        private readonly IEmailManager manager;

        public EmailService()
        {
            manager = new EmailCacheManager(new RedisClient("localhost", 6379));
        }

        public object Any(UsersUpdateEmail req)
        {
            req.Id = Guid.NewGuid().ToString();
            manager.Set(req);
            return new object();
        }
    }
}
