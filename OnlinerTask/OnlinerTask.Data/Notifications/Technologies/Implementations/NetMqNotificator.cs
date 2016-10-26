using OnlinerTask.Data.Sockets;

namespace OnlinerTask.Data.Notifications.Technologies.Implementations
{
    public class NetMqNotificator : INotifyTechnology
    {
        private readonly NetSocket socket;

        public NetMqNotificator()
        {
            socket = new NetSocket();
        }

        public void AddProduct(dynamic name, dynamic path = null)
        {
            socket.AddProduct(name, path);
        }

        public void ChangeInfo(dynamic time)
        {
            socket.ChangeInfo(time);
        }

        public void RemoveProduct(dynamic name, dynamic path = null)
        {
            socket.RemoveProduct(name, path);
        }
    }
}
