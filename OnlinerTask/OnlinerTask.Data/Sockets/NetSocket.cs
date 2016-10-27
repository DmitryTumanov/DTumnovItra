using System.Diagnostics;
using System.Threading;
using NetMQ;
using NetMQ.WebSockets;
using Poller = NetMQ.Poller;

namespace OnlinerTask.Data.Sockets
{
    public class NetSocket
    {
        public NetSocket()
        {
            ExecuteTcpConnection("tcp://localhost:5556", "ws://localhost:81");
        }

        public void AddProduct(dynamic name, dynamic path = null)
        {
            SendTcpMessage(name, "tcp://localhost:5556", path != null ? "SearchAddProduct" : "addProduct", path);
        }

        public void RemoveProduct(dynamic name, dynamic path = null)
        {
            SendTcpMessage(name, "tcp://localhost:5556", path != null ? "SearchRemoveProduct" : "removeProduct", path);
        }

        public void ChangeInfo(dynamic time)
        {
            SendTcpMessage(time, "tcp://localhost:5556", "infoProduct");
        }

        private static void SendTcpMessage(string text, string tcpString, string type, string path = null)
        {
            using (var client = NetMQContext.Create().CreateRequestSocket())
            {
                client.Connect(tcpString);
                var message = new NetMQMessage();
                message.Append(type);
                message.Append(text);
                if (!string.IsNullOrEmpty(path))
                {
                    message.Append(path);
                }
                client.SendMessage(message);
            }
        }

        private static WSPublisher CreatePublisher(string path)
        {
            var publisher = NetMQContext.Create().CreateWSPublisher();
            publisher.Bind(path);
            return publisher;
        }

        private static void ExecuteTcpConnection(string tcpString, string wsSocket)
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