using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using OnlinerTask.Data.DataBaseModels;
using OnlinerTask.Data.IdentityModels;
using OnlinerTask.Data.ScheduleModels;
using OnlinerTask.Data.SearchModels;
using OnlinerTask.Extensions.Extensions;

namespace OnlinerTask.Data.Repository.Implementations
{
    public class MsSqlProductRepository : IProductRepository
    {
        public async Task<List<ProductModel>> CheckProducts(List<ProductModel> products, string userName)
        {
            await products.ForEachAsync(async i =>
            {
                i.IsChecked = await CheckItem(i.Id, userName);
            });
            return products;
        }

        public UsersUpdateEmail WriteUpdate(ProductModel item, string useremail)
        {
            using (var db = new ApplicationDbContext())
            {
                var time = db.Users.FirstOrDefault(x => x.Email == useremail);
                if (time != null)
                {
                    return WriteUpdateToProduct(item, time.EmailTime);
                }
            }
            return null;
        }

        private async Task<bool> CheckItem(int itemId, string username)
        {
            using (var context = new OnlinerProducts())
            {
                return await context.Product.FirstOrDefaultAsync(x => x.ProductId == itemId && x.UserEmail == username) != null;
            }
        }

        private void UpdateProduct(Product item, ProductModel model)
        {
            if (item == null)
            {
                return;
            }
            using (var db = new OnlinerProducts())
            {
                var product = db.Product.FirstOrDefault(x => x.ProductId == item.ProductId);
                if (product == null)
                {
                    return;
                }
                product.Price.PriceMaxAmmount.Amount = model.Prices.PriceMax.Amount;
                product.Price.PriceMinAmmount.Amount = model.Prices.PriceMin.Amount;
                db.SaveChanges();
            }
        }

        private UsersUpdateEmail WriteUpdateToProduct(ProductModel model, TimeSpan time)
        {
            if (model == null)
            {
                return null;
            }
            using (var db = new OnlinerProducts())
            {
                var dbmodel = db.Product.FirstOrDefault(x => x.ProductId == model.Id);
                UpdateProduct(dbmodel, model);
                return new UsersUpdateEmail()
                {
                    ProductName = model.FullName,
                    UserEmail = dbmodel.UserEmail,
                    Time = time
                };
            }
        }
    }
}
