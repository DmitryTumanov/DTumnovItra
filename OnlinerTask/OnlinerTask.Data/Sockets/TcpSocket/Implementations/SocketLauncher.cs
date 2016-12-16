using System.Diagnostics;
using System.Threading;
using NetMQ;
using NetMQ.WebSockets;

namespace OnlinerTask.Data.Sockets.TcpSocket.Implementations
{
    public class SocketLauncher : ISocketLauncher
    {
        public void ExecuteTcpConnection(string tcpString, string wsSocket)
        {
            new Thread(() =>
            {
                using (var tcp = NetMQContext.Create().CreateResponseSocket())
                {
                    try
                    {
                        ConfigureTcpConnection(tcp, tcpString, wsSocket);
                    }
                    catch (NetMQException)
                    {
                        Debug.WriteLine($"{tcpString} is used.");
                    }
                }
            })
            { IsBackground = true }.Start();
        }

        private static WSPublisher CreatePublisher(string path)
        {
            var publisher = NetMQContext.Create().CreateWSPublisher();
            publisher.Bind(path);
            return publisher;
        }

        private static void ConfigureTcpConnection(NetMQSocket tcp, string tcpString, string wsSocket)
        {
            tcp.Bind(tcpString);
            using (var publisher = CreatePublisher(wsSocket))
            {
                using (var poller = new Poller())
                {
                    tcp.ReceiveReady += (sender, args) =>
                    {
                        var message = args.Socket.ReceiveMessage();
                        var type = message[0].ConvertToString();
                        var text = message[1].ConvertToString();
                        publisher.SendMore(type).Send(text);
                        tcp.Send("OK");
                    };
                    poller.AddSocket(tcp);
                    poller.Start();
                }
            }
        }
    }
}
