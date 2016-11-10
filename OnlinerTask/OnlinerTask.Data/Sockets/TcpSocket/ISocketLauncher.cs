namespace OnlinerTask.Data.Sockets.TcpSocket
{
    public interface ISocketLauncher
    {
        void ExecuteTcpConnection(string tcpString, string wsSocket);
    }
}
