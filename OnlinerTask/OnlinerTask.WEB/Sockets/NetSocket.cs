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
                var socket = CreateSocket("ws://localhost:3339");
                var publisher = CreatePublisher("ws://localhost:81");
                UpdateProductOrInfo(socket, publisher, "addProduct");
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
                var socket = CreateSocket("ws://localhost:3340");
                var publisher = CreatePublisher("ws://localhost:82");
                UpdateProductOrInfo(socket, publisher, "removeProduct");
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
                var socket = CreateSocket("ws://localhost:3341");
                var publisher = CreatePublisher("ws://localhost:83");
                UpdateProductOrInfo(socket, publisher, "infoProduct");
            }
            catch (NetMQException ex)
            {
                Debug.WriteLine(ex.Message);
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

        private IOutgoingSocket CreatePublisher(string path)
        {
            var publisher = NetMQContext.Create().CreateWSPublisher();
            publisher.Bind(path);
            return publisher;
        }
    }
}