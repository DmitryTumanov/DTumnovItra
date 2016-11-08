using System.Linq;
using System.Threading.Tasks;
using OnlinerTask.BLL.Services.Search;
using OnlinerTask.Data.Repository;
using OnlinerTask.Data.Requests;
using OnlinerTask.Data.Responses;

namespace OnlinerTask.BLL.Services.Products.Implementations
{
    public abstract class Manager : IManager
    {
        private readonly ISearchService searchService;
        private readonly IPersonalRepository repository;

        protected Manager(ISearchService searchService, IPersonalRepository repository)
        {
            this.searchService = searchService;
            this.repository = repository;
        }

        public async Task AddProduct(PutRequest request, string name)
        {
            var result = (await searchService.GetProducts(request, name)).FirstOrDefault();
            repository.CreateOnlinerProduct(result, name);
            AddNotify(result?.FullName);
        }

        public async Task RemoveProduct(DeleteRequest request, string name)
        {
            var productName = await repository.RemoveOnlinerProduct(request.ItemId, name);
            RemoveNotify(productName);
        }
        public async Task<PersonalPageResponse> GetProducts(string name)
        {
            return await GetAllProducts(name);
        }

        public abstract void AddNotify(string name);
        public abstract void RemoveNotify(string name);

        public virtual Task<PersonalPageResponse> GetAllProducts(string name)
        {
            return null;
        }
    }
}
