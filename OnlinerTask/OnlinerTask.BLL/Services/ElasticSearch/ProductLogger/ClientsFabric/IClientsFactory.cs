using Nest;

namespace OnlinerTask.BLL.Services.ElasticSearch.ProductLogger.ClientsFabric
{
    public interface IClientsFactory
    {
        ElasticClient CreateClient(ConnectionSettings settings, string defaultIndex = "");
    }
}
