using System.Threading.Tasks;
using OnlinerTask.BLL.Services.Job.ProductJob.ProductUpdate;
using OnlinerTask.Data.MqConstituents;
using OnlinerTask.Data.Repository;
using OnlinerTask.Extensions.Extensions;
using ServiceStack.Messaging;
using IRepository = OnlinerTask.Data.Repository.IRepository;

namespace OnlinerTask.BLL.Services.Job.ProductJob.Implementations
{
    public class ProductJobService : IProductJob
    {
        private readonly IProductRepository productRepository;
        private readonly IRepository repository;
        private readonly IProductUpdater productUpdater;
        private readonly IMqConstituentsFactory constituentsFactory;

        public ProductJobService(IProductRepository productRepository, IProductUpdater productUpdater, IRepository repository, IMqConstituentsFactory constituentsFactory)
        {
            this.repository = repository;
            this.productRepository = productRepository;
            this.productUpdater = productUpdater;
            this.constituentsFactory = constituentsFactory;
        }

        public async Task Execute()
        {
            constituentsFactory.CreateAppHost();
            await GetAndPublishUpdates(constituentsFactory.CreateClient());
        }

        public async Task GetAndPublishUpdates(IMessageQueueClient mqClient)
        {
            await repository.GetAllProducts().ForEachAsync(async x =>
            {
                var product = await productUpdater.GetUpdate(x);
                if (product == null)
                {
                    return;
                }
                var redisElem = productRepository.WriteUpdate(product, x.UserEmail);
                mqClient?.Publish(redisElem);
            });
        }
    }
}
