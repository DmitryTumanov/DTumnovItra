using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlinerTask.Data.SearchModels;
using OnlinerTask.Data.DataBaseModels;
using System.Data.Entity;
using OnlinerTask.Data.Extensions;
using OnlinerTask.Data.EntityMappers.Interfaces;
using OnlinerTask.Data.Repository.Interfaces;
using System;

namespace OnlinerTask.Data.Repository
{
    public class MsSQLProductRepository : IProductRepository
    {
        private IProductMapper<Product, ProductModel> productMapper;
        private IRepository repository;

        public MsSQLProductRepository(IProductMapper<Product, ProductModel> productMapper, IRepository repository)
        {
            this.productMapper = productMapper;
            this.repository = repository;
        }

        public async Task<List<ProductModel>> CheckProducts(List<ProductModel> products, string UserName)
        {
            await AsyncOperations.ForEachAsync(products, async i =>
            {
                i.IsChecked = await CheckItem(i.Id, UserName); ;
            });
            return products;
        }

        public async Task<bool> CheckItem(int ItemId, string Username)
        {
            using (var context = new OnlinerProducts())
            {
                return await context.Product.FirstOrDefaultAsync(x => x.ProductId == ItemId && x.UserEmail == Username) != null ? true : false;
            }
        }

        public List<Product> GetAllProducts()
        {
            using (var context = new OnlinerProducts())
            {
                return context.Product
                    .Include(x => x.Image)
                    .Include(x => x.Price)
                    .Include(x => x.Price.Offer)
                    .Include(x => x.Price.PriceMinAmmount)
                    .Include(x => x.Price.PriceMaxAmmount)
                    .Include(x => x.Review).ToList();
            }
        }

        public List<Product> GetPersonalProducts(string name)
        {
            return repository.GetPersonalProducts(name);
        }

        public Task<bool> RemoveOnlinerProduct(int itemId, string name)
        {
            return repository.RemoveOnlinerProduct(itemId, name);
        }

        public bool CreateOnlinerProduct(ProductModel model, string UserEmail)
        {
            return repository.CreateOnlinerProduct(model, UserEmail);
        }
    }
}
