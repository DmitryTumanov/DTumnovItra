using System;
using System.Collections.Generic;
using System.Linq;
using OnlinerTask.Data.DataBaseModels;
using OnlinerTask.Data.IdentityModels;
using OnlinerTask.Data.Repository.Interfaces;
using OnlinerTask.Data.ScheduleModels;

namespace OnlinerTask.Data.Repository
{
    public class MsSqlTimeServiceRepository : ITimeServiceRepository
    {
        public readonly IRepository repository;

        public MsSqlTimeServiceRepository(IRepository repository)
        {
            this.repository = repository;
        }

        public bool UpdateProduct(Product item)
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
                product = item;
                db.SaveChanges();
                return true;
            }
        }

        public bool WriteUpdateToProduct(Product item, TimeSpan time)
        {
            if (item == null)
            {
                return false;
            }
            using (var db = new OnlinerProducts())
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

        public void WriteUpdate(Product item)
        {
            using (var db = new ApplicationDbContext())
            {
                var time = db.Users.FirstOrDefault(x => x.Email == item.UserEmail);
                if (time != null)
                {
                    WriteUpdateToProduct(item, time.EmailTime);
                }
            }
        }

        public List<Product> GetAllProducts()
        {
            return repository.GetAllProducts();
        }
    }
}
