using Funq;
using OnlinerTask.Data.ScheduleModels;
using ServiceStack;
using ServiceStack.Messaging.Redis;
using ServiceStack.Redis;

namespace OnlinerTask.WEB.TimeRegistry
{
    public class EmailAppHost : AppHostHttpListenerBase
    {
        public EmailAppHost() : base("Test Server", typeof(EmailService).Assembly) { }

        public override void Configure(Container container)
        {
            base.Routes
                .Add<UsersUpdateEmail>("/sendemail")
                .Add<UsersUpdateEmail>("/sendemail/{Id}/{UserEmail}/{ProductName}/{Time}");

            var redisFactory = new PooledRedisClientManager("localhost:6379");
            container.Register<IRedisClientsManager>(redisFactory);
            var mqHost = new RedisMqServer(redisFactory);

            mqHost.RegisterHandler<UsersUpdateEmail>(base.ExecuteMessage);
            mqHost.Start();
        }
    }
}
