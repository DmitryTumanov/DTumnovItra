namespace OnlinerTask.Data.Sockets
{
    public interface ISocketLauncher
    {
        void ExecuteTcpConnection(string tcpString, string wsSocket);
    }
}
