using System.Collections.Generic;
using System.Threading.Tasks;
using OnlinerTask.Data.SearchModels;
using OnlinerTask.Data.DataBaseModels;
using System.Data.Entity;
using OnlinerTask.Data.Extensions;
using OnlinerTask.Data.Repository.Interfaces;

namespace OnlinerTask.Data.Repository
{
    public class MsSqlProductRepository : IProductRepository
    {
        private readonly IRepository _repository;

        public MsSqlProductRepository(IRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<ProductModel>> CheckProducts(List<ProductModel> products, string userName)
        {
            await products.ForEachAsync(async i =>
            {
                i.IsChecked = await CheckItem(i.Id, userName);
            });
            return products;
        }

        public async Task<bool> CheckItem(int itemId, string username)
        {
            using (var context = new OnlinerProducts())
            {
                return await context.Product.FirstOrDefaultAsync(x => x.ProductId == itemId && x.UserEmail == username) != null;
            }
        }

        public List<Product> GetAllProducts()
        {
            return _repository.GetAllProducts();
        }

        public List<Product> GetPersonalProducts(string name)
        {
            return _repository.GetPersonalProducts(name);
        }

        public Task<bool> RemoveOnlinerProduct(int itemId, string name)
        {
            return _repository.RemoveOnlinerProduct(itemId, name);
        }

        public bool CreateOnlinerProduct(ProductModel model, string userEmail)
        {
            return _repository.CreateOnlinerProduct(model, userEmail);
        }
    }
}
