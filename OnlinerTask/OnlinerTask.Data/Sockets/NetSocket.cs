using System.Diagnostics;
using NetMQ;
using NetMQ.WebSockets;

namespace OnlinerTask.Data.Sockets
{
    public class NetSocket
    {
        public void AddProduct(dynamic name)
        {
            try
            {
                ExecuteUpdate("ws://localhost:3339", "ws://localhost:81", "addProduct", name);
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
                ExecuteUpdate("ws://localhost:3340", "ws://localhost:82", "removeProduct", name);
            }
            catch (NetMQException ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        public void ChangeInfo(dynamic time)
        {
            try
            {
                ExecuteUpdate("ws://localhost:3350", "ws://localhost:84", "infoProduct", time.ToString("t"));
            }
            catch (NetMQException ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private void ExecuteUpdate(string socketPath, string publisherPath, string chatType, dynamic message)
        {
            using (var socket = CreateSocket(socketPath))
            {
                using (var publisher = CreatePublisher(publisherPath))
                {
                    UpdateProductOrInfo(socket, publisher, chatType, message);
                }
            }
        }

        private void UpdateProductOrInfo(WSSocket newSocket, IOutgoingSocket publisher, string socketType, string text)
        {
            newSocket.SendMore(newSocket.Receive()).Send("OK");
            publisher.SendMore(socketType).Send(text);
            var poller = new Poller();
            poller.AddSocket(newSocket);

            poller.Start();
            newSocket.Receive();
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