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
        private readonly IRepository repository;

        public MsSqlProductRepository(IRepository repository)
        {
            this.repository = repository;
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
            return repository.GetAllProducts();
        }

        public List<Product> GetPersonalProducts(string name)
        {
            return repository.GetPersonalProducts(name);
        }

        public Task<string> RemoveOnlinerProduct(int itemId, string name)
        {
            return repository.RemoveOnlinerProduct(itemId, name);
        }

        public bool CreateOnlinerProduct(ProductModel model, string userEmail)
        {
            return repository.CreateOnlinerProduct(model, userEmail);
        }
    }
}
