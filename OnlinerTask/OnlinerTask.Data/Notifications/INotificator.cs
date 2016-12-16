using OnlinerTask.Data.RedisManager.RedisServer.RedisRequests.Implementations;

namespace OnlinerTask.Data.Notifications
{
    public interface INotificator
    {
        void AddProduct(AddProductRequest req);

        void RemoveProduct(RemoveProductRequest req);

        void ChangeInfo(ChangeTimeRequest req);
    }
}
