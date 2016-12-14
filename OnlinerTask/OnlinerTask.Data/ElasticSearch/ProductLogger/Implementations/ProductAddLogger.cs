using System.Threading.Tasks;
using Nest;
using OnlinerTask.Data.ElasticSearch.ClientsFabric;
using OnlinerTask.Data.ElasticSearch.ConnectionFabric;
using OnlinerTask.Data.ElasticSearch.LoggerModels;
using OnlinerTask.Data.Resources;

namespace OnlinerTask.Data.ElasticSearch.ProductLogger.Implementations
{
    public class ProductAddLogger : IProductLogger<AddedProductModel>
    {
        private readonly ElasticClient elasticClient;

        public ProductAddLogger(IClientsFactory clientsFabric, IConnectionFactory connectionFabric)
        {
            var settings = connectionFabric.CreateConnection(Configurations.ElasticSearchUrl, Configurations.ProductAddLogIndex);
            elasticClient = clientsFabric.CreateClient(settings, Configurations.ProductAddLogIndex);
        }

        public async Task LogObject(AddedProductModel productModel)
        {
            var defaultIndex = elasticClient.ConnectionSettings.DefaultIndex;
            var indexType = Configurations.ProductAddIndexType;

            await elasticClient.IndexAsync(productModel,
                i => i.Index(defaultIndex).Type(indexType).Refresh());
        }
    }
}
