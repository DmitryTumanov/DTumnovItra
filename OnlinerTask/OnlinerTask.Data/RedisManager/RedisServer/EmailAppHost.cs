using Funq;
using OnlinerTask.Data.Resources;
using OnlinerTask.Data.ScheduleModels;
using ServiceStack;
using ServiceStack.Messaging.Redis;
using ServiceStack.Redis;

namespace OnlinerTask.Data.RedisManager.RedisServer
{
    public class EmailAppHost : AppHostHttpListenerBase
    {
        public EmailAppHost() : base(ResourceSection.RedisServerName, typeof(EmailService).Assembly) { }

        public override void Configure(Container container)
        {
            base.Routes
                .Add<UsersUpdateEmail>(ResourceSection.RedisRoute)
                .Add<UsersUpdateEmail>(ResourceSection.RedisFullRoute);

            var redisFactory = new PooledRedisClientManager(ResourceSection.RedisClient);
            container.Register<IRedisClientsManager>(redisFactory);
            var mqHost = new RedisMqServer(redisFactory);

            mqHost.RegisterHandler<UsersUpdateEmail>(base.ExecuteMessage);
            mqHost.Start();
        }
    }
}
