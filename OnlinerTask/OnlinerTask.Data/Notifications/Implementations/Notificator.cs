using OnlinerTask.Data.Notifications.Technologies;
using OnlinerTask.Data.RedisManager.RedisServer.RedisRequests.Implementations;

namespace OnlinerTask.Data.Notifications.Implementations
{
    public class Notificator: INotificator
    {
        private readonly INotifyTechnology technology;

        public Notificator(INotifyTechnology technology)
        {
            this.technology = technology;
        }

        public void AddProduct(AddProductRequest req)
        {
            technology.AddProduct(req.Message, req.RedirectPath);
        }

        public void RemoveProduct(RemoveProductRequest req)
        {
            technology.RemoveProduct(req.Message, req.RedirectPath);
        }

        public void ChangeInfo(ChangeTimeRequest req)
        {
            technology.ChangeInfo(req.Message);
        }
    }
}
