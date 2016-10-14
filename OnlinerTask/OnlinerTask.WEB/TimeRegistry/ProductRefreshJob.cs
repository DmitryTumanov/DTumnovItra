using FluentScheduler;
using System.Linq;
using System.Threading.Tasks;
using OnlinerTask.Data.DataBaseModels;
using OnlinerTask.BLL.Services;
using OnlinerTask.Data.SearchModels;
using System.Web.Mvc;
using OnlinerTask.Data.Requests;
using OnlinerTask.Data.Repository.Interfaces;

namespace OnlinerTask.WEB.TimeRegistry
{
    public class ProductRefreshJob : IJob
    {
        private readonly ISearchService _searchService;
        private readonly ITimeServiceRepository _repository;

        public ProductRefreshJob()
        {
            _repository = DependencyResolver.Current.GetService<ITimeServiceRepository>();
            _searchService = DependencyResolver.Current.GetService<ISearchService>();
        }
        public ProductRefreshJob(ITimeServiceRepository repository, ISearchService service)
        {
            _repository = repository;
            _searchService = service;
        }

        public async void Execute()
        {
            var products = _repository.GetAllProducts();
            foreach (var item in products)
            {
                if (await ProductUpdated(item))
                {
                    WriteProduct(item);
                }
            }
        }

        private void WriteProduct(Product item)
        {
            _repository.WriteUpdate(item);
        }

        private async Task<bool> ProductUpdated(Product item)
        {
            var product = (await _searchService.GetProducts(new SearchRequest() { SearchString = item.ProductKey }, item.UserEmail)).FirstOrDefault();
            return Check(item, product);
        }

        private bool Check(Product product, ProductModel model)
        {
            return model.Prices.PriceMax.Amount != product.Price.PriceMaxAmmount.Amount || model.Prices.PriceMin.Amount != product.Price.PriceMinAmmount.Amount ||
                model.Prices.PriceMax.Currency != product.Price.PriceMaxAmmount.Currency || model.Prices.PriceMin.Currency != product.Price.PriceMinAmmount.Currency;
        }
    }
}
