using Microsoft.AspNet.SignalR;
using Microsoft.AspNet.SignalR.Hubs;

namespace OnlinerTask.WEB.Hubs
{
    public class PersonalHub:Hub
    {
        public void ChangeSettings(dynamic time)
        {
            Clients.Caller.changeTime(time);
        }

        public void DeleteProduct(dynamic name)
        {
            Clients.Caller.deleteProduct(name);
        }

        public void AddProduct(dynamic name)
        {
            Clients.Caller.addProduct(name);
        }
    }
}