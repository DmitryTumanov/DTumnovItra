using System.Web;
using System.Web.Mvc;
using OnlinerTask.Data.Resources;
using OnlinerTask.Data.Sockets;

namespace OnlinerTask.WEB.Modules
{
    public class SocketModule : IHttpModule
    {
        private readonly ISocketLauncher launcher;

        public SocketModule()
        {
            launcher = DependencyResolver.Current.GetService<ISocketLauncher>();
        }

        public void Init(HttpApplication context)
        {
            launcher.ExecuteTcpConnection(Configurations.TcpConnectionPath, Configurations.WebSocketConnectionPath);
        }

        public void Dispose()
        {
        }
    }
}