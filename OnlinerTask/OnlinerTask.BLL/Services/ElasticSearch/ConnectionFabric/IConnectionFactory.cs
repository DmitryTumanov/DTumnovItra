using Nest;

namespace OnlinerTask.BLL.Services.ElasticSearch.ConnectionFabric
{
    public interface IConnectionFactory
    {
        ConnectionSettings CreateConnection(string urlPath, string defaultIndex = "");
    }
}
