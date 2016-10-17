using System;
using System.Collections.Generic;
using System.Linq;
using OnlinerTask.Data.DataBaseModels;
using OnlinerTask.Data.EntityMappers.Interfaces;
using OnlinerTask.Data.IdentityModels;
using OnlinerTask.Data.Repository.Interfaces;
using OnlinerTask.Data.ScheduleModels;
using OnlinerTask.Data.SearchModels;

namespace OnlinerTask.Data.Repository
{
    public class MsSqlTimeServiceRepository : ITimeServiceRepository
    {
        public readonly IRepository repository;

        public MsSqlTimeServiceRepository(IRepository repository, IProductMapper<Product, ProductModel> productMapper)
        {
            this.repository = repository;
        }

        public bool UpdateProduct(Product item, ProductModel model)
        {
            if (item == null)
            {
                return false;
            }
            using (var db = new OnlinerProducts())
            {
                var product = db.Product.FirstOrDefault(x => x.ProductId == item.ProductId);
                if (product == null)
                {
                    return false;
                }
                product.Price.PriceMaxAmmount.Amount = model.Prices.PriceMax.Amount;
                product.Price.PriceMinAmmount.Amount = model.Prices.PriceMin.Amount;
                db.SaveChanges();
                return true;
            }
        }

        public UsersUpdateEmail WriteUpdateToProduct(ProductModel model, TimeSpan time)
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
                    Id = model.Id,
                    ProductName = model.FullName,
                    UserEmail = dbmodel.UserEmail,
                    Time = time
                };
            }
        }

        public IEnumerable<UsersUpdateEmail> GetUsersEmails()
        {
            using (var db = new OnlinerProducts())
            {
                var userslist = new List<UsersUpdateEmail>();
                var updatelist = db.UpdatedProducts.ToList();
                foreach (var i in updatelist)
                {
                    var model = db.Product.Where(x => x.UserEmail == i.UserEmail && x.Id == i.ProductId).Select(x => x.Name).FirstOrDefault();
                    userslist.Add(new UsersUpdateEmail() { ProductName = model, UserEmail = i.UserEmail, Id = i.Id, Time = (TimeSpan)i.TimeToSend });
                };
                return userslist;
            }
        }

        public void DeleteUserAndProduct(int id, string userEmail)
        {
            using (var db = new OnlinerProducts())
            {
                var model = db.UpdatedProducts.FirstOrDefault(x => x.UserEmail == userEmail && x.Id == id);
                if (model == null)
                {
                    return;
                }
                db.UpdatedProducts.Remove(model);
                db.SaveChanges();
            }
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

        public List<Product> GetAllProducts()
        {
            return repository.GetAllProducts();
        }
    }
}
