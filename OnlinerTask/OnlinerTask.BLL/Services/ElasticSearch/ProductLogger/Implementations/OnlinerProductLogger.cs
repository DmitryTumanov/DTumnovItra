using System.Threading.Tasks;
using Nest;
using OnlinerTask.BLL.Services.ElasticSearch.ProductLogger.ClientsFabric;
using OnlinerTask.BLL.Services.ElasticSearch.ProductLogger.ConnectionFabric;
using OnlinerTask.Data.Resources;
using OnlinerTask.Data.SearchModels;

namespace OnlinerTask.BLL.Services.ElasticSearch.ProductLogger.Implementations
{
    public class OnlinerProductLogger : IProductLogger
    {
        private readonly ElasticClient elasticClient;

        public OnlinerProductLogger(IClientsFactory clientsFabric, IConnectionFactory connectionFabric)
        {
            var settings = connectionFabric.CreateConnection(Configurations.ElasticSearchUrl, Configurations.ProductLogIndex);
            elasticClient = clientsFabric.CreateClient(settings, Configurations.ProductLogIndex);
        }

        public async Task LogAdding(int productId, ProductModel productModel)
        {
            await LogProductFromOnliner(productId,productModel, Configurations.ProductLogIndex, Configurations.ProductAddIndexType);
        }

        public async Task RemoveLog(int productId)
        {
            await RemoveProductLog(productId, Configurations.ProductLogIndex, Configurations.ProductAddIndexType);
        }

        private async Task RemoveProductLog(int productId, string productIndex, string productIndexType)
        {
            await elasticClient.DeleteAsync<ProductModel>(productId, d=>d.Index(productIndex).Type(productIndexType));
        }

        private async Task LogProductFromOnliner(int productId, ProductModel productModel, string productIndex, string productIndexType)
        {
            await elasticClient.IndexAsync(productModel,
                i => i.Index(productIndex).Type(productIndexType).Id(productId).Refresh());
        }
    }
}
