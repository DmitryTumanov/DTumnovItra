using System.Threading.Tasks;
using Nest;
using OnlinerTask.Data.DataBaseModels;
using OnlinerTask.Data.ElasticSearch.ClientsFabric;
using OnlinerTask.Data.ElasticSearch.ConnectionFabric;
using OnlinerTask.Data.Resources;

namespace OnlinerTask.Data.ElasticSearch.ProductLogger.Implementations
{
    public class ProductRemoveLogger : IProductLogger<Product>
    {
        private readonly ElasticClient elasticClient;

        public ProductRemoveLogger(IClientsFactory clientsFabric, IConnectionFactory connectionFabric)
        {
            var settings = connectionFabric.CreateConnection(Configurations.ElasticSearchUrl, Configurations.ProductRemoveLogIndex);
            elasticClient = clientsFabric.CreateClient(settings, Configurations.ProductRemoveLogIndex);
        }

        public async Task LogObject(Product productModel)
        {
            var defaultIndex = elasticClient.ConnectionSettings.DefaultIndex;
            var indexType = Configurations.ProductRemoveIndexType;

            await elasticClient.IndexAsync(productModel,
                i => i.Index(defaultIndex).Type(indexType).Refresh());
        }
    }
}
