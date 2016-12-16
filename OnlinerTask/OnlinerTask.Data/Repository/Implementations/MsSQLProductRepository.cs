using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using OnlinerTask.Data.DataBaseContexts;
using OnlinerTask.Data.DataBaseInterfaces;
using OnlinerTask.Data.DataBaseModels;
using OnlinerTask.Data.ScheduleModels;
using OnlinerTask.Data.SearchModels;

namespace OnlinerTask.Data.Repository.Implementations
{
    public class MsSqlProductRepository : IProductRepository
    {
        private readonly IUserContext userContext;
        private readonly IOnlinerContext onlinerContext;

        public MsSqlProductRepository(IUserContext userContext, IOnlinerContext onlinerContext)
        {
            this.userContext = userContext;
            this.onlinerContext = onlinerContext;
        }

        public List<ProductModel> CheckProducts(List<ProductModel> products, string userName)
        {
            products.ForEach(i =>
            {
                i.IsChecked = CheckItem(i.Id, userName);
            });
            return products;
        }

        public UsersUpdateEmail WriteUpdate(ProductModel item, string useremail)
        {
            var time = userContext.Users.FirstOrDefault(x => x.Email == useremail);
            if (time == null)
            {
                return null;
            }
            return WriteUpdateToProduct(item, time.EmailTime);
        }

        private bool CheckItem(int itemId, string username)
        {
            try
            {
                var checkResult = onlinerContext.Product.FirstOrDefault(x => x.ProductId == itemId && x.UserEmail == username);
                return checkResult != null;
            }
            catch(Exception exception)
            {
                Debug.WriteLine(exception.InnerException);
                return false;
            }
        }

        private void UpdateProduct(Product item, ProductModel model)
        {
            if (item == null)
            {
                return;
            }
            var product = onlinerContext.Product.FirstOrDefault(x => x.ProductId == item.ProductId);
            if (product == null)
            {
                return;
            }
            product.Price.PriceMaxAmmount.Amount = model.Prices.PriceMax.Amount;
            product.Price.PriceMinAmmount.Amount = model.Prices.PriceMin.Amount;
            onlinerContext.SaveChanges();
        }

        private UsersUpdateEmail WriteUpdateToProduct(ProductModel model, TimeSpan time)
        {
            if (model == null)
            {
                return null;
            }
            var dbmodel = onlinerContext.Product.FirstOrDefault(x => x.ProductId == model.Id);
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
