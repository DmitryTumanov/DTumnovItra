using System.Linq;
using System.Threading.Tasks;
using Ninject;
using OnlinerTask.BLL.Services.ElasticSearch.ProductLogger;
using OnlinerTask.BLL.Services.Search;
using OnlinerTask.Data.DataBaseModels;
using OnlinerTask.Data.Repository;
using OnlinerTask.Data.Requests;
using OnlinerTask.Data.Responses;
using OnlinerTask.Data.SearchModels;

namespace OnlinerTask.BLL.Services.Products.Implementations
{
    public abstract class Manager : IManager
    {
        private readonly ISearchService searchService;
        private readonly IRepository repository;

        [Inject, Named("AddLogger")]
        public IProductLogger<ProductModel> AddProductLogger { get; set; }

        [Inject, Named("RemoveLogger")]
        public IProductLogger<Product> RemoveProductLogger { get; set; }

        protected Manager(ISearchService searchService, IRepository repository)
        {
            this.searchService = searchService;
            this.repository = repository;
        }

        public async Task AddProduct(PutRequest request, string name)
        {
            var result = (await searchService.GetProducts(request, name))?.FirstOrDefault();
            if (result == null)
            {
                return;
            }
            repository.CreateOnlinerProduct(result, name);
            AddNotify(result.FullName);
            await AddProductLogger.LogObject(result);
        }

        public async Task RemoveProduct(DeleteRequest request, string name)
        {
            var product = await repository.RemoveOnlinerProduct(request.ItemId, name);
            RemoveNotify(product.FullName);
            await RemoveProductLogger.LogObject(product);
        }
        public virtual Task<PersonalPageResponse> GetProducts(string name)
        {
            return null;
        }

        public abstract void AddNotify(string name);
        public abstract void RemoveNotify(string name);
    }
}
