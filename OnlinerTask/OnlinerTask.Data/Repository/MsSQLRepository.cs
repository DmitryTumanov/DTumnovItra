using System.Threading.Tasks;
using OnlinerTask.Data.DataBaseModels;
using OnlinerTask.Data.Repository.Interfaces;
using OnlinerTask.Data.SearchModels;
using System.Data.Entity;
using OnlinerTask.Data.EntityMappers.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace OnlinerTask.Data.Repository
{
    public class MsSqlRepository : IRepository
    {
        private readonly IProductMapper<Product, ProductModel> _productMapper;

        public MsSqlRepository(IProductMapper<Product, ProductModel> productMapper)
        {
            _productMapper = productMapper;
        }

        public bool CreateOnlinerProduct(ProductModel model, string userEmail)
        {
            if (model == null)
            {
                return false;
            }
            var maxid = CreatePriceAmmount(new PriceAmmount() { Amount = model.Prices.PriceMax.Amount, Currency = model.Prices.PriceMax.Currency });
            var minid = CreatePriceAmmount(new PriceAmmount() { Amount = model.Prices.PriceMin.Amount, Currency = model.Prices.PriceMin.Currency });
            var product = ModelToDb(model, userEmail, maxid, minid);
            CreateProduct(product);
            return true;
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

        public async Task<bool> RemoveOnlinerProduct(int itemId, string name)
        {
            using (var context = new OnlinerProducts())
            {
                var model = await context.Product.FirstOrDefaultAsync(x => x.UserEmail == name && x.ProductId == itemId);
                if (model == null)
                {
                    return false;
                }
                await RemovePriceAmount(context, model.Price.PriceMaxId, model.Price.PriceMinId);
                context.Product.Remove(model);
                await context.SaveChangesAsync();
                return true;
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

        private Product ModelToDb(ProductModel model, string userEmail, int maxid, int minid)
        {
            return _productMapper.ConvertToModel(model, userEmail, maxid, minid);
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
