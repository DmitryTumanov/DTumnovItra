using Funq;
using OnlinerTask.Data.DataBaseModels;
using OnlinerTask.Data.ElasticSearch.LoggerModels;
using OnlinerTask.Data.RedisManager.RedisServer.RedisRequests.Implementations;
using OnlinerTask.Data.ScheduleModels;
using OnlinerTask.Data.SearchModels;
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
                .Add<ChangeTimeRequest>("/sendnotify")
                .Add<AddProductRequest>("/sendaddnotify")
                .Add<RemoveProductRequest>("/sendremovenotify")
                .Add<Product>("/logdataproduct")
                .Add<ProductModel>("/logproductmodel")
                .Add<ProductModel>("/loguseractivity");

            var redisFactory = new PooledRedisClientManager("localhost:6379");
            container.Register<IRedisClientsManager>(redisFactory);
            var mqHost = new RedisMqServer(redisFactory);

            mqHost.RegisterHandler<UsersUpdateEmail>(base.ExecuteMessage);
            mqHost.RegisterHandler<ChangeTimeRequest>(base.ExecuteMessage);
            mqHost.RegisterHandler<AddProductRequest>(base.ExecuteMessage);
            mqHost.RegisterHandler<RemoveProductRequest>(base.ExecuteMessage);
            mqHost.RegisterHandler<ProductModel>(base.ExecuteMessage);
            mqHost.RegisterHandler<Product>(base.ExecuteMessage);
            mqHost.RegisterHandler<WebRequest>(base.ExecuteMessage);
            mqHost.Start();
        }
    }
}
