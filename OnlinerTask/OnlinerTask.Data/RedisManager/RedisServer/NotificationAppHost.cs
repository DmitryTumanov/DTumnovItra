using Funq;
using OnlinerTask.Data.RedisManager.RedisServer.RedisRequests;
using OnlinerTask.Data.ScheduleModels;
using ServiceStack;
using ServiceStack.Messaging.Redis;
using ServiceStack.Redis;

namespace OnlinerTask.Data.RedisManager.RedisServer
{
    public class NotificationAppHost : AppHostHttpListenerBase
    {
        public NotificationAppHost() : base("Test Server", typeof(NotificationService).Assembly) { }

        public override void Configure(Container container)
        {
            base.Routes
                .Add<UsersUpdateEmail>("/sendemail")
                .Add<UsersUpdateEmail>("/sendemail/{Id}/{UserEmail}/{ProductName}/{Time}")
                .Add<ChangeTimeRequest>("/sendnotify")
                .Add<ChangeTimeRequest>("/sendnotify/{Message}")
                .Add<AddProductRequest>("/sendaddnotify")
                .Add<AddProductRequest>("/sendaddnotify/{Message}/{RedirectPath}")
                .Add<RemoveProductRequest>("/sendremovenotify")
                .Add<RemoveProductRequest>("/sendremovenotify/{Message}/{RedirectPath}");

            var redisFactory = new PooledRedisClientManager("localhost:6379");
            container.Register<IRedisClientsManager>(redisFactory);
            var mqHost = new RedisMqServer(redisFactory);

            mqHost.RegisterHandler<UsersUpdateEmail>(base.ExecuteMessage);
            mqHost.RegisterHandler<ChangeTimeRequest>(base.ExecuteMessage);
            mqHost.RegisterHandler<AddProductRequest>(base.ExecuteMessage);
            mqHost.RegisterHandler<RemoveProductRequest>(base.ExecuteMessage);
            mqHost.Start();
        }
    }
}
