using NetMQ;
using OnlinerTask.Data.Resources;

namespace OnlinerTask.Data.Sockets
{
    public class NetSocket
    {
        public void AddProduct(dynamic name, dynamic path = null)
        {
            SendTcpMessage(name, Configurations.TcpConnectionPath, path != null ? Configurations.SearchAddChatType : Configurations.AddChatType, path);
        }

        public void RemoveProduct(dynamic name, dynamic path = null)
        {
            SendTcpMessage(name, Configurations.TcpConnectionPath, path != null ? Configurations.SearchRemoveChatType : Configurations.RemoveChatType, path);
        }

        public void ChangeInfo(dynamic time)
        {
            SendTcpMessage(time, Configurations.TcpConnectionPath, Configurations.InfoChatType);
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
    }
}