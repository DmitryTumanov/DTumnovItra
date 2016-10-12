using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlinerTask.Data.SearchModels;
using OnlinerTask.Data.DataBaseModels;
using System.Data.Entity;
using OnlinerTask.Data.EntityMappers;
using System;
using OnlinerTask.Data.Extensions;
using OnlinerTask.Data.IdentityModels;
using OnlinerTask.Data.Responses;
using OnlinerTask.Data.Requests;

namespace OnlinerTask.Data.Repository
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
            if (item == null)
            {
                return false;
            }
            using (var db = new OnlinerProducts())
            {
                var product = db.Product.Where(x => x.ProductId == item.ProductId).FirstOrDefault();
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
                var time = db.Users.Where(x => x.Email == item.UserEmail).FirstOrDefault();
                if (time != null)
                {
                    WriteUpdateToProduct(item, time.EmailTime);
                }
            }
        }

        public PersonalPageResponse PersonalProductsResponse(string UserName)
        {
            var result = GetPersonalProducts(UserName).Select(x => new ProductMapper().ConvertToModel(x));
            using (var db = new ApplicationDbContext())
            {
                var time = DateTime.Now.TimeOfDay;
                var user = db.Users.Where(x => x.UserName == (UserName)).FirstOrDefault();
                if (user != null)
                {
                    time = user.EmailTime;
                }
                return new PersonalPageResponse() { EmailTime = DateTime.Now.Date + time, Products = result.ToList() };
            }
        }

        public void ChangeSendEmailTime(TimeRequest request, string UserName)
        {
            using (var db = new ApplicationDbContext())
            {
                var user = db.Users.FirstOrDefault(x => x.UserName == UserName);
                if (user != null && request != null)
                {
                    user.EmailTime = request.Time.TimeOfDay;
                    db.SaveChanges();
                }
            }
        }
    }
}
