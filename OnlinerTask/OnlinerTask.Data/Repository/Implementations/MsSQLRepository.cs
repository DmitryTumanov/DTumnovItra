using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using OnlinerTask.Data.DataBaseModels;
using OnlinerTask.Data.EntityMappers;
using OnlinerTask.Data.SearchModels;

namespace OnlinerTask.Data.Repository.Implementations
{
    public class MsSqlRepository : IRepository
    {
        private readonly IProductMapper<Product, ProductModel> productMapper;

        public MsSqlRepository(IProductMapper<Product, ProductModel> productMapper)
        {
            this.productMapper = productMapper;
        }

        public bool CreateOnlinerProduct(ProductModel model, string userEmail)
        {
            Product product;
            if (model == null)
            {
                return false;
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

        public int CreatePriceAmmount(PriceAmmount price)
        {
            if (price == null)
            {
                return -1;
            }
            using (var context = new OnlinerProducts())
            {
                context.PriceAmmount.Add(price);
                context.SaveChanges();
                return price.Id;
            }
        }

        public bool CreateProduct(Product product)
        {
            if (product == null)
            {
                return false;
            }
            using (var context = new OnlinerProducts())
            {
                context.Product.Add(product);
                context.SaveChanges();
                return true;
            }
        }

        public async Task<string> RemoveOnlinerProduct(int itemId, string name)
        {
            using (var context = new OnlinerProducts())
            {
                var model = await context.Product.FirstOrDefaultAsync(x => x.UserEmail == name && x.ProductId == itemId);
                if (model == null)
                {
                    return "";
                }
                if (model.Price != null)
                {
                    await RemovePriceAmount(context, model.Price.PriceMaxId, model.Price.PriceMinId);
                }
                context.Product.Remove(model);
                await context.SaveChangesAsync();
                return model.FullName;
            }
        }

        private async Task RemovePriceAmount(OnlinerProducts context, int? priceMaxId, int? priceMinId)
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
            using (var context = new OnlinerProducts())
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
    }
}
