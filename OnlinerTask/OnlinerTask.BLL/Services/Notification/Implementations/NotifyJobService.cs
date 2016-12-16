using OnlinerTask.Data.MqConstituents;
using OnlinerTask.Data.RedisManager.RedisServer.RedisRequests.Implementations;
using ServiceStack.Messaging;

namespace OnlinerTask.BLL.Services.Notification.Implementations
{
    public class NotifyJobService : INotification
    {
        private readonly IMessageQueueClient queueClient;

        public NotifyJobService(IMqConstituentsFactory constituentsFactory)
        {
            queueClient = constituentsFactory.CreateClient();
        }

        public void AddProduct(dynamic name)
        {
            queueClient.Publish(new AddProductRequest() { Message = name.ToString() });
        }

        public void AddProductFromSearch(dynamic name)
        {
            queueClient.Publish(new AddProductRequest() { Message = name.ToString(), RedirectPath = "http://localhost:33399/Manage" });
        }

        public void ChangeSettings(dynamic time)
        {
            queueClient.Publish(new ChangeTimeRequest() { Message = time.ToString("t") });
        }

        public void DeleteProduct(dynamic name)
        {
            queueClient.Publish(new RemoveProductRequest() { Message = name.ToString() });
        }

        public void DeleteProductFromSearch(dynamic name)
        {
            queueClient.Publish(new RemoveProductRequest() { Message = name.ToString(), RedirectPath = "http://localhost:33399/Manage" });
        }
    }
}
