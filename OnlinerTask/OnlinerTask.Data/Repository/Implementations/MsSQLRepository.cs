using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using OnlinerTask.Data.DataBaseInterfaces;
using OnlinerTask.Data.DataBaseModels;
using OnlinerTask.Data.EntityMappers;
using OnlinerTask.Data.SearchModels;

namespace OnlinerTask.Data.Repository.Implementations
{
    public class MsSqlRepository : IRepository
    {
        private readonly IProductMapper<Product, ProductModel> productMapper;
        private readonly IOnlinerContext context;

        public MsSqlRepository(IProductMapper<Product, ProductModel> productMapper, IOnlinerContext context)
        {
            this.productMapper = productMapper;
            this.context = context;
        }

        public int CreateOnlinerProduct(ProductModel model, string userEmail)
        {
            Product product;
            if (model == null)
            {
                return -1;
            }
            if (model.Prices != null)
            {
                var maxid = CreatePriceAmmount(new PriceAmmount() { Amount = model.Prices.PriceMax.Amount, Currency = model.Prices.PriceMax.Currency });
                var minid = CreatePriceAmmount(new PriceAmmount() { Amount = model.Prices.PriceMin.Amount, Currency = model.Prices.PriceMin.Currency });
                product = ModelToDb(model, userEmail, maxid, minid);
            }
            else
            {
                product = ModelToDb(model, userEmail);
            }
            return CreateProduct(product);
        }

        private int CreatePriceAmmount(PriceAmmount price)
        {
            if (price == null)
            {
                return -1;
            }
            context.PriceAmmount.Add(price);
            context.SaveChanges();
            return price.Id;
        }

        private int CreateProduct(Product product)
        {
            if (product == null)
            {
                return -1;
            }
            context.Product.Add(product);
            context.SaveChanges();
            return product.Id;
        }

        public async Task<Product> RemoveOnlinerProduct(int itemId, string name)
        {
            var model = await context.Product.FirstOrDefaultAsync(x => x.UserEmail == name && x.ProductId == itemId);
            if (model == null)
            {
                return null;
            }
            if (model.Price != null)
            {
                await RemovePriceAmount(context, model.Price.PriceMaxId, model.Price.PriceMinId);
            }
            context.Product.Remove(model);
            await context.SaveChangesAsync();
            return model;
        }

        private async Task RemovePriceAmount(IOnlinerContext context, int? priceMaxId, int? priceMinId)
        {
            var minprice = await context.PriceAmmount.FirstOrDefaultAsync(x => x.Id == priceMinId);
            var maxprice = await context.PriceAmmount.FirstOrDefaultAsync(x => x.Id == priceMaxId);
            if (maxprice != null)
            {
                context.PriceAmmount.Remove(maxprice);
            }
            if (minprice != null)
            {
                context.PriceAmmount.Remove(minprice);
            }
            await context.SaveChangesAsync();
        }

        private Product ModelToDb(ProductModel model, string userEmail, int maxid = 0, int minid = 0)
        {
            return productMapper.ConvertToModel(model, userEmail, maxid, minid);
        }

        public List<Product> GetPersonalProducts(string name)
        {
            return context.Product.Where(x => x.UserEmail == name)
                .OrderBy(x => x.FullName)
                .Include(x => x.Image)
                .Include(x => x.Price)
                .Include(x => x.Price.Offer)
                .Include(x => x.Price.PriceMinAmmount)
                .Include(x => x.Price.PriceMaxAmmount)
                .Include(x => x.Review).ToList();
        }

        public List<Product> GetAllProducts()
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
}
