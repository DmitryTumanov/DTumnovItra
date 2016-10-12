using FluentScheduler;
using OnlinerTask.Data.Repository;
using System.Linq;
using System.Threading.Tasks;
using OnlinerTask.Data.DataBaseModels;
using OnlinerTask.BLL.Services;
using OnlinerTask.Data.SearchModels;
using System.Web.Mvc;
using OnlinerTask.Data.Requests;

namespace OnlinerTask.WEB.TimeRegistry
{
    public class ProductRefreshJob : IJob
    {
        private ISearchService service;
        private IRepository repository;

        public ProductRefreshJob()
        {
            repository = DependencyResolver.Current.GetService<IRepository>();
            service = DependencyResolver.Current.GetService<ISearchService>();
        }
        public ProductRefreshJob(IRepository repository, ISearchService service)
        {
            this.repository = repository;
            this.service = service;
        }

        public async void Execute()
        {
            var products = repository.GetAllProducts();
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
            repository.WriteUpdate(item);
        }

        private async Task<bool> ProductUpdated(Product item)
        {
            var product = (await service.GetProducts(new SearchRequest() { SearchString = item.ProductKey }, repository, item.UserEmail)).FirstOrDefault();
            return Check(item, product);
        }

        private bool Check(Product product, ProductModel model)
        {
            return model.Prices.PriceMax.Amount != product.Price.PriceMaxAmmount.Amount || model.Prices.PriceMin.Amount != product.Price.PriceMinAmmount.Amount ||
                model.Prices.PriceMax.Currency != product.Price.PriceMaxAmmount.Currency || model.Prices.PriceMin.Currency != product.Price.PriceMinAmmount.Currency;
        }

        private void RefreshProduct(Product item)
        {
            repository.UpdateProduct(item);
        }
    }
}
