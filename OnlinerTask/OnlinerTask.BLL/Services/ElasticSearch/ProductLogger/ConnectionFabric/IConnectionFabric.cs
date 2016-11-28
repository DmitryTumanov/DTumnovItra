using Nest;

namespace OnlinerTask.BLL.Services.ElasticSearch.ProductLogger.ConnectionFabric
{
    public interface IConnectionFabric
    {
        ConnectionSettings CreateConnection(string urlPath, string defaultIndex = "");
    }
}
