using Microsoft.AspNet.SignalR;
using OnlinerTask.Data.Hubs;

namespace OnlinerTask.Data.Notifications.Technologies.Implementations
{
    public class SignalRNotificator : INotifyTechnology
    {
        private readonly IHubContext hub;

        public SignalRNotificator()
        {
            hub = GlobalHost.ConnectionManager.GetHubContext<PersonalHub>();
        }

        public void AddProduct(dynamic name, dynamic path = null)
        {
            hub.Clients.All.addProduct(name, path);
        }

        public void ChangeInfo(dynamic time)
        {
            hub.Clients.All.changeTime(time);
        }

        public void RemoveProduct(dynamic name, dynamic path = null)
        {
            hub.Clients.All.deleteProduct(name, path);
        }
    }
}
