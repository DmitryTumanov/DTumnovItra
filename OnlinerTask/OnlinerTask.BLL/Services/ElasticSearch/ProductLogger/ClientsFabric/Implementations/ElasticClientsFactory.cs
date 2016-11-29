using Nest;

namespace OnlinerTask.BLL.Services.ElasticSearch.ProductLogger.ClientsFabric.Implementations
{
    public class ElasticClientsFactory : IClientsFactory
    {
        public ElasticClient CreateClient(ConnectionSettings settings, string defaultIndex = "")
        {
            var elasticClient = new ElasticClient(settings);
            if (!string.IsNullOrEmpty(defaultIndex) && !elasticClient.IndexExists(defaultIndex).Exists)
            {
                elasticClient.CreateIndex(defaultIndex);
            }
            return elasticClient;
        }
    }
}
