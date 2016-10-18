using System.Linq;
using System.Threading.Tasks;
using OnlinerTask.Data.DataBaseModels;
using OnlinerTask.Data.Extensions;
using OnlinerTask.Data.RedisManager.RedisServer;
using OnlinerTask.Data.Repository.Interfaces;
using OnlinerTask.Data.Requests;
using OnlinerTask.Data.ScheduleModels;
using OnlinerTask.Data.SearchModels;
using ServiceStack;
using ServiceStack.Messaging;
using ServiceStack.Messaging.Redis;
using ServiceStack.Redis;

namespace OnlinerTask.BLL.Services.Job
{
    public class ProductJobService : IProductJob
    {
        private readonly ISearchService searchService;
        private readonly ITimeServiceRepository repository;

        public ProductJobService(ITimeServiceRepository repository, ISearchService searchService)
        {
            this.repository = repository;
            this.searchService = searchService;
        }

        public async Task Execute()
        {
            CreateServer();
            using (var mqClient = CreateClient())
            {
                await GetAndPublishUpdates(mqClient);
            }
        }

        public void CreateServer()
        {
            var serverAppHost = new EmailAppHost();
            if (ServiceStackHost.Instance != null) return;
            serverAppHost.Init();
            serverAppHost.Start("http://localhost:1400/");
        }

        public async Task GetAndPublishUpdates(IMessageQueueClient mqClient)
        {
            var products = repository.GetAllProducts();
            await products.ForEachAsync(async x =>
            {
                var product = await ProductUpdated(x);
                if (product == null) return;
                var redisElem = WriteProduct(product, x.UserEmail);
                mqClient.Publish(redisElem);
            });
        }

        public IMessageQueueClient CreateClient()
        {
            var redisFactory = new PooledRedisClientManager("localhost:6379");
            var mqServer = new RedisMqServer(redisFactory);
            mqServer.Start();
            return mqServer.CreateMessageQueueClient();
        }

        private UsersUpdateEmail WriteProduct(ProductModel item, string userEmail)
        {
            return repository.WriteUpdate(item, userEmail);
        }

        private async Task<ProductModel> ProductUpdated(Product item)
        {
            var product = (await searchService.GetProducts(new SearchRequest(item.ProductKey), item.UserEmail)).FirstOrDefault();
            return Check(item, product) ? product : null;
        }

        private bool Check(Product product, ProductModel model)
        {
            var maxAmountUpdate = model.Prices.PriceMax.Amount != product.Price.PriceMaxAmmount.Amount;
            var minAmountUpdate = model.Prices.PriceMin.Amount != product.Price.PriceMinAmmount.Amount;
            return maxAmountUpdate || minAmountUpdate;
        }
    }
}
