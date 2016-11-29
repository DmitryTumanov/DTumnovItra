using Nest;

namespace OnlinerTask.BLL.Services.ElasticSearch.ProductLogger.ConnectionFabric
{
    public interface IConnectionFactory
    {
        ConnectionSettings CreateConnection(string urlPath, string defaultIndex = "");
    }
}
