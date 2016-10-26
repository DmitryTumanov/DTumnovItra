using Microsoft.AspNet.SignalR;
using OnlinerTask.Data.RedisManager.RedisServer.RedisRequests;

namespace OnlinerTask.Data.Hubs
{
    public class PersonalHub:Hub
    {
        public void ChangeSettings(ChangeTimeRequest req)
        {
            Clients.Caller.changeTime(req.Message);
        }

        public void DeleteProduct(RemoveProductRequest req)
        {
            Clients.Caller.deleteProduct(req.Message, req.RedirectPath);
        }

        public void AddProduct(AddProductRequest req)
        {
            Clients.Caller.addProduct(req.Message, req.RedirectPath);
        }
    }
}