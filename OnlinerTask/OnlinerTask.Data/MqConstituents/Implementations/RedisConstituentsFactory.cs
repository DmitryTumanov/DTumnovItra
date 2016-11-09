using OnlinerTask.Data.RedisManager.RedisServer;
using OnlinerTask.Data.Resources;
using ServiceStack;
using ServiceStack.Messaging;
using ServiceStack.Messaging.Redis;
using ServiceStack.Redis;

namespace OnlinerTask.Data.MqConstituents.Implementations
{
    public class RedisConstituentsFactory : IMqConstituentsFactory
    {
        public void CreateAppHost()
        {
            var serverAppHost = new NotificationAppHost();
            if (ServiceStackHost.Instance != null)
            {
                return;
            }
            serverAppHost.Init();
            serverAppHost.Start(Configurations.RedisAppHost);
        }

        public IMessageQueueClient CreateClient()
        {
            var redisFactory = new PooledRedisClientManager(Configurations.RedisClient);
            var mqServer = new RedisMqServer(redisFactory);
            mqServer.Start();
            return mqServer.CreateMessageQueueClient();
        }
    }
}
