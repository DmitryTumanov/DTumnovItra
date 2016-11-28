using Nest;

namespace OnlinerTask.BLL.Services.ElasticSearch.ProductLogger.ClientsFabric
{
    public interface IClientsFabric
    {
        ElasticClient CreateClient(ConnectionSettings settings, string defaultIndex = "");
    }
}
