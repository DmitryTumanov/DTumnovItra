using FluentScheduler;
using System.Linq;
using System.Threading.Tasks;
using OnlinerTask.Data.DataBaseModels;
using OnlinerTask.BLL.Services;
using OnlinerTask.Data.SearchModels;
using System.Web.Mvc;
using OnlinerTask.Data.EntityMappers.Interfaces;
using OnlinerTask.Data.Requests;
using OnlinerTask.Data.Repository.Interfaces;
using OnlinerTask.Data.ScheduleModels;
using ServiceStack;
using ServiceStack.Messaging;
using ServiceStack.Messaging.Redis;
using ServiceStack.Redis;

namespace OnlinerTask.WEB.TimeRegistry
{
    public class ProductRefreshJob : IJob
    {
        private readonly ISearchService searchService;
        private readonly ITimeServiceRepository repository;

        public ProductRefreshJob()
        {
            repository = DependencyResolver.Current.GetService<ITimeServiceRepository>();
            searchService = DependencyResolver.Current.GetService<ISearchService>();
        }
        public ProductRefreshJob(ITimeServiceRepository repository, ISearchService service)
        {
            this.repository = repository;
            searchService = service;
        }

        public async void Execute()
        {
            CreateServer();
            var mqClient = CreateClient();
            var products = repository.GetAllProducts();
            foreach (var item in products)
            {
                var product = await ProductUpdated(item);
                if (product == null) continue;
                var redisElem = WriteProduct(product, item.UserEmail);
                mqClient.Publish(redisElem);
            }
        }

        private void CreateServer()
        {
            var serverAppHost = new EmailAppHost();
            if (ServiceStackHost.Instance != null) return;
            serverAppHost.Init();
            serverAppHost.Start("http://localhost:1400/");
        }

        private UsersUpdateEmail WriteProduct(ProductModel item, string userEmail)
        {
            return repository.WriteUpdate(item, userEmail);
        }

        private async Task<ProductModel> ProductUpdated(Product item)
        {
            var product = (await searchService.GetProducts(new SearchRequest() { SearchString = item.ProductKey }, item.UserEmail)).FirstOrDefault();
            return Check(item, product) ? product : null;
        }

        private bool Check(Product product, ProductModel model)
        {
            return model.Prices.PriceMax.Amount != product.Price.PriceMaxAmmount.Amount || model.Prices.PriceMin.Amount != product.Price.PriceMinAmmount.Amount;
        }

        private IMessageQueueClient CreateClient()
        {
            var redisFactory = new PooledRedisClientManager("localhost:6379");
            var mqServer = new RedisMqServer(redisFactory);
            mqServer.Start();
            return mqServer.CreateMessageQueueClient();
        }
    }
}
