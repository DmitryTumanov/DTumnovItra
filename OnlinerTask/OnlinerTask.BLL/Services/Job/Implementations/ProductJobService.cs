using System.Linq;
using System.Threading.Tasks;
using OnlinerTask.BLL.Services.Search;
using OnlinerTask.Data.DataBaseModels;
using OnlinerTask.Data.RedisManager.RedisServer;
using OnlinerTask.Data.Repository;
using OnlinerTask.Data.Requests;
using OnlinerTask.Data.Resources;
using OnlinerTask.Data.ScheduleModels;
using OnlinerTask.Data.SearchModels;
using OnlinerTask.Extensions.Extensions;
using ServiceStack;
using ServiceStack.Messaging;
using ServiceStack.Messaging.Redis;
using ServiceStack.Redis;
using IRepository = OnlinerTask.Data.Repository.IRepository;

namespace OnlinerTask.BLL.Services.Job.Implementations
{
    public class ProductJobService : IProductJob
    {
        private readonly ISearchService searchService;
        private readonly IProductRepository productRepository;
        private readonly IRepository repository;

        public ProductJobService(IProductRepository productRepository, ISearchService searchService, IRepository repository)
        {
            this.repository = repository;
            this.searchService = searchService;
            this.productRepository = productRepository;
        }

        public async Task Execute()
        {
            CreateAppHost();
            await GetAndPublishUpdates(CreateClient());
        }

        public void CreateAppHost()
        {
            var serverAppHost = new NotificationAppHost();
            if (ServiceStackHost.Instance != null)
            {
                return;
            }
            serverAppHost.Init();
            serverAppHost.Start(Configurations.RedisAppHost);
        }

        public async Task GetAndPublishUpdates(IMessageQueueClient mqClient)
        {
            await repository.GetAllProducts().ForEachAsync(async x =>
            {
                var product = await ProductUpdated(x);
                if (product == null)
                {
                    return;
                }
                var redisElem = WriteProduct(product, x.UserEmail);
                mqClient.Publish(redisElem);
            });
        }

        public IMessageQueueClient CreateClient()
        {
            var redisFactory = new PooledRedisClientManager(Configurations.RedisClient);
            var mqServer = new RedisMqServer(redisFactory);
            mqServer.Start();
            return mqServer.CreateMessageQueueClient();
        }

        private UsersUpdateEmail WriteProduct(ProductModel onlinerModel, string userEmail)
        {
            return productRepository.WriteUpdate(onlinerModel, userEmail);
        }

        private async Task<ProductModel> ProductUpdated(Product databaseModel)
        {
            var product = (await searchService.GetProducts(new SearchRequest(databaseModel.ProductKey), databaseModel.UserEmail)).FirstOrDefault();
            return Check(databaseModel, product) ? product : null;
        }

        private bool Check(Product databaseModel, ProductModel onlinerModel)
        {
            if (databaseModel.Price == null)
            {
                return false;
            }
            var isMaxAmountUpdate = onlinerModel.Prices.PriceMax.Amount != databaseModel.Price.PriceMaxAmmount.Amount;
            var isMinAmountUpdate = onlinerModel.Prices.PriceMin.Amount != databaseModel.Price.PriceMinAmmount.Amount;
            return isMaxAmountUpdate || isMinAmountUpdate;
        }
    }
}
