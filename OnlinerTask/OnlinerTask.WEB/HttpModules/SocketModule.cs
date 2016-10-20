using System.Threading;
using System.Web;
using NetMQ;
using NetMQ.WebSockets;
using OnlinerTask.WEB.Sockets;

namespace OnlinerTask.WEB.HttpModules
{
    public class SocketModule : IHttpModule
    {
        private NetSocket socket;

        public void Init(HttpApplication context)
        {
            socket = new NetSocket();

            var addThread = new Thread(socket.AddProduct) {IsBackground = true};
            addThread.Start("");

            var removeThread = new Thread(socket.RemoveProduct) {IsBackground = true};
            removeThread.Start("");

            var infoThread = new Thread(socket.ChangeInfo) {IsBackground = true};
            infoThread.Start("");
        }

        public void Dispose()
        {
        }
    }
}