using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlinerTask.DAL.SearchModels;
using OnlinerTask.Data.DBModels;
using System.Data.Entity;

namespace OnlinerTask.BLL.Repository
{
    public class MsSQLRepository : IRepository
    {
        public bool CheckItem(int ItemId, string Username)
        {
            using(var context = new OnlinerProducts())
            {
                return context.Products.Where(x => x.ProductId == ItemId && x.UserEmail == Username).FirstOrDefault() != null ? true : false;
            }
        }

        public bool CreateOnlinerProduct(ProductModel model, string UserEmail)
        {
            if (model == null) return false;
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
            if (price == null) return -1;
            else
            {
                using (var context = new OnlinerProducts())
                {
                    context.PriceAmmounts.Add(price);
                    context.SaveChanges();
                    return price.Id;
                }
            }
        }

        public bool CreateProduct(Product product)
        {
            if (product == null) return false;
            else
            {
                using (var context = new OnlinerProducts())
                {
                    context.Products.Add(product);
                    context.SaveChanges();
                    return true;
                }
            }
        }

        public bool RemoveOnlinerProduct(int itemId, string name)
        {
            using (var context = new OnlinerProducts())
            {
                var model = context.Products.Where(x => x.UserEmail == name && x.ProductId == itemId).FirstOrDefault();
                if (model != null)
                {
                    RemovePriceAmount(model.Price.PriceMaxId, model.Price.PriceMinId);
                    context.Products.Remove(model);
                    context.SaveChanges();
                    return true;
                }
                return false;
            }
        }

        public void RemovePriceAmount(int? priceMaxId, int? priceMinId)
        {
            using (var context = new OnlinerProducts())
            {
                var minprice = context.PriceAmmounts.Where(x => x.Id == priceMinId).FirstOrDefault();
                var maxprice = context.PriceAmmounts.Where(x => x.Id == priceMaxId).FirstOrDefault();
                if (maxprice != null) context.PriceAmmounts.Remove(maxprice);
                if (minprice != null) context.PriceAmmounts.Remove(minprice);
            }
        }

        private Product ModelToDB(ProductModel model, string UserEmail, int maxid, int minid)
        {
            return new Product()
            {
                Description = model.Description,
                FullName = model.FullName,
                HtmlUrl = model.HtmlUrl,
                ProductId = model.Id,
                Name = model.Name,
                ProductKey = model.Key,
                ReviewUrl = model.ReviewUrl,
                UserEmail = UserEmail,
                Image = new Image()
                {
                    Header = model.Images.Header
                },
                Review = new Review()
                {
                    Count = model.Reviews.Count,
                    HtmlUrl = model.Reviews.HtmlUrl,
                    Rating = model.Reviews.Rating
                },
                Price = new Price()
                {
                    HtmlUrl = model.Prices.HtmlUrl,
                    Offer = new Offer()
                    {
                        Count = model.Prices.Offers.Count
                    },
                    PriceMaxId = maxid,
                    PriceMinId = minid
                }
            };
        }
    }
}
