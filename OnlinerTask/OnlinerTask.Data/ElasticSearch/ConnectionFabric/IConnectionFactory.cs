using Nest;

namespace OnlinerTask.Data.ElasticSearch.ConnectionFabric
{
    public interface IConnectionFactory
    {
        ConnectionSettings CreateConnection(string urlPath, string defaultIndex = "");
    }
}
