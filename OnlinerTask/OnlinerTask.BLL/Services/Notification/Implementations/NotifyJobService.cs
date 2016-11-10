using OnlinerTask.Data.RedisManager.RedisServer.RedisRequests.Implementations;
using ServiceStack.Messaging;
using ServiceStack.Messaging.Redis;
using ServiceStack.Redis;

namespace OnlinerTask.BLL.Services.Notification.Implementations
{
    public class NotifyJobService : INotification
    {
        public void AddProduct(dynamic name)
        {
            CreateClient().Publish(new AddProductRequest() { Message = name.ToString()});
        }

        public void AddProductFromSearch(dynamic name)
        {
            CreateClient().Publish(new AddProductRequest() { Message = name.ToString(), RedirectPath = "http://localhost:33399/Manage" });
        }

        public void ChangeSettings(dynamic time)
        {
            CreateClient().Publish(new ChangeTimeRequest() {Message = time.ToString("t")});
        }

        public void DeleteProduct(dynamic name)
        {
            CreateClient().Publish(new RemoveProductRequest() { Message = name.ToString()});
        }

        public void DeleteProductFromSearch(dynamic name)
        {
            CreateClient().Publish(new RemoveProductRequest() { Message = name.ToString(), RedirectPath = "http://localhost:33399/Manage" });
        }

        private IMessageQueueClient CreateClient()
        {
            var redisFactory = new PooledRedisClientManager("localhost:6379");
            var mqServer = new RedisMqServer(redisFactory);
            mqServer.Start();
            return mqServer.CreateMessageQueueClient();
        }
    }
}
