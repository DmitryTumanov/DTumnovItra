using System.Diagnostics;
using NetMQ;
using NetMQ.WebSockets;

namespace OnlinerTask.WEB.Sockets
{
    public class NetSocket
    {
        public void AddProduct(dynamic name)
        {
            try
            {
                ExecuteUpdate("ws://localhost:3339", "ws://localhost:81", "addProduct");
            }
            catch (NetMQException ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public void RemoveProduct(dynamic name)
        {
            try
            {
                ExecuteUpdate("ws://localhost:3340", "ws://localhost:82", "removeProduct");
            }
            catch (NetMQException ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public void ChangeInfo(dynamic name)
        {
            try
            {
                ExecuteUpdate("ws://localhost:3341", "ws://localhost:83", "infoProduct");
            }
            catch (NetMQException ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private void ExecuteUpdate(string socketPath, string publisherPath, string chatType)
        {
            using (var socket = CreateSocket(socketPath))
            {
                using (var publisher = CreatePublisher(publisherPath))
                {
                    UpdateProductOrInfo(socket, publisher, chatType);
                }
            }
        }

        private void UpdateProductOrInfo(WSSocket newSocket, IOutgoingSocket publisher, string socketType)
        {
            newSocket.ReceiveReady += (sender, eventArgs) =>
            {
                var identity = eventArgs.WSSocket.Receive();
                var message = eventArgs.WSSocket.ReceiveString();

                eventArgs.WSSocket.SendMore(identity).Send("OK");

                publisher.SendMore(socketType).Send(message);
            };

            var poller = new Poller();
            poller.AddSocket(newSocket);

            poller.Start();
        }

        private WSSocket CreateSocket(string path)
        {
            var socket = NetMQContext.Create().CreateWSRouter();
            socket.Bind(path);
            return socket;
        }

        private WSPublisher CreatePublisher(string path)
        {
            var publisher = NetMQContext.Create().CreateWSPublisher();
            publisher.Bind(path);
            return publisher;
        }
    }
}