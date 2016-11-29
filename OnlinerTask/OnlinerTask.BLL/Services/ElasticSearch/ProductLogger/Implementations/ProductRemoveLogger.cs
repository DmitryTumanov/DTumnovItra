using System.Threading.Tasks;
using Nest;
using OnlinerTask.BLL.Services.ElasticSearch.ProductLogger.ClientsFabric;
using OnlinerTask.BLL.Services.ElasticSearch.ProductLogger.ConnectionFabric;
using OnlinerTask.Data.DataBaseModels;
using OnlinerTask.Data.Resources;

namespace OnlinerTask.BLL.Services.ElasticSearch.ProductLogger.Implementations
{
    public class ProductRemoveLogger : IProductLogger<Product>
    {
        private readonly ElasticClient elasticClient;

        public ProductRemoveLogger(IClientsFactory clientsFabric, IConnectionFactory connectionFabric)
        {
            var settings = connectionFabric.CreateConnection(Configurations.ElasticSearchUrl, Configurations.ProductLogIndex);
            elasticClient = clientsFabric.CreateClient(settings, Configurations.ProductLogIndex);
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
