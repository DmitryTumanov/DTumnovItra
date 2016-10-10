using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlinerTask.DAL.SearchModels;
using OnlinerTask.Data.DBModels;
using System.Data.Entity;
using OnlinerTask.Data.EntityMappers;
using OnlinerTask.BLL.Extensions;
using System;

namespace OnlinerTask.BLL.Repository
{
    public class MsSQLRepository : IRepository
    {
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

        public bool CreateOnlinerProduct(ProductModel model, string UserEmail)
        {
            if (model == null)
            {
                return false;
            }
            else
            {
                int maxid = CreatePriceAmmount(new PriceAmmount() { Amount = model.Prices.PriceMax.Amount, Currency = model.Prices.PriceMax.Currency });
                int minid = CreatePriceAmmount(new PriceAmmount() { Amount = model.Prices.PriceMin.Amount, Currency = model.Prices.PriceMin.Currency });
                var product = ModelToDB(model, UserEmail, maxid, minid);
                CreateProduct(product);
                return true;
            }
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
            else
            {
                using (var context = new OnlinerProducts())
                {
                    context.Product.Add((Product)product);
                    context.SaveChanges();
                    return true;
                }
            }
        }

        public bool RemoveOnlinerProduct(int itemId, string name)
        {
            using (var context = new OnlinerProducts())
            {
                var model = context.Product.Where(x => x.UserEmail == name && x.ProductId == itemId).FirstOrDefault();
                if (model == null)
                {
                    return false;
                }
                RemovePriceAmount(model.Price.PriceMaxId, model.Price.PriceMinId);
                context.Product.Remove(model);
                context.SaveChanges();
                return true;
            }
        }

        public void RemovePriceAmount(int? priceMaxId, int? priceMinId)
        {
            using (var context = new OnlinerProducts())
            {
                var minprice = context.PriceAmmount.Where(x => x.Id == priceMinId).FirstOrDefault();
                var maxprice = context.PriceAmmount.Where(x => x.Id == priceMaxId).FirstOrDefault();
                if (maxprice != null)
                {
                    context.PriceAmmount.Remove(maxprice);
                }
                if (minprice != null)
                {
                    context.PriceAmmount.Remove(minprice);
                }
            }
        }

        private Product ModelToDB(ProductModel model, string UserEmail, int maxid, int minid)
        {
            return new ProductMapper().ConvertToModel(model, UserEmail, maxid, minid);
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

        public bool UpdateProduct(Product item)
        {
            if(item == null)
            {
                return false;
            }
            using(var db = new OnlinerProducts())
            {
                var product = db.Product.Where(x => x.ProductId == item.ProductId).FirstOrDefault();
                if(product == null)
                {
                    return false;
                }
                product = item;
                db.SaveChanges();
                return true;
            }
        }

        public bool WriteUpdateToProduct(Product item, DateTime time)
        {
            if(item == null)
            {
                return false;
            }
            using(var db = new OnlinerProducts())
            {
                var model = db.UpdatedProducts.FirstOrDefault(x => x.ProductId == item.Id && x.UserEmail == item.UserEmail);
                if (model != null)
                {
                    model.TimeToSend = time;
                }
                else
                {
                    db.UpdatedProducts.Add(new UpdatedProducts()
                    {
                        ProductId = item.Id,
                        UserEmail = item.UserEmail,
                        TimeToSend = time
                    });
                }
                return UpdateProduct(item);
            }
        }
    }
}
