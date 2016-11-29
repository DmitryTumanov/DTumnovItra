using System.Threading.Tasks;
using Nest;
using OnlinerTask.BLL.Services.ElasticSearch.ClientsFabric;
using OnlinerTask.BLL.Services.ElasticSearch.ConnectionFabric;
using OnlinerTask.Data.Resources;
using OnlinerTask.Data.SearchModels;

namespace OnlinerTask.BLL.Services.ElasticSearch.ProductLogger.Implementations
{
    public class ProductAddLogger : IProductLogger<ProductModel>
    {
        private readonly ElasticClient elasticClient;

        public ProductAddLogger(IClientsFactory clientsFabric, IConnectionFactory connectionFabric)
        {
            var settings = connectionFabric.CreateConnection(Configurations.ElasticSearchUrl, Configurations.ProductLogIndex);
            elasticClient = clientsFabric.CreateClient(settings, Configurations.ProductLogIndex);
        }

        public async Task LogObject(ProductModel productModel)
        {
            var defaultIndex = elasticClient.ConnectionSettings.DefaultIndex;
            var indexType = Configurations.ProductAddIndexType;

            await elasticClient.IndexAsync(productModel,
                i => i.Index(defaultIndex).Type(indexType).Refresh());
        }
    }
}
