using System;
using System.Threading.Tasks;
using OnlinerTask.Data.DataBaseModels;
using OnlinerTask.Data.EntityMappers.Interfaces;
using OnlinerTask.Data.Requests;
using OnlinerTask.Data.Responses;
using OnlinerTask.Data.SearchModels;
using System.Linq;
using OnlinerTask.Data.IdentityModels;
using System.Data.Entity;
using OnlinerTask.Data.Repository.Interfaces;

namespace OnlinerTask.Data.Repository
{
    public class MsSQLPersonalRepository : IPersonalRepository
    {
        private IProductMapper<Product, ProductModel> productMapper;
        private IRepository repository;

        public MsSQLPersonalRepository(IProductMapper<Product, ProductModel> productMapper, IRepository repository)
        {
            this.productMapper = productMapper;
            this.repository = repository;
        }
        
        public async Task ChangeSendEmailTimeAsync(TimeRequest request, string UserName)
        {
            using (var db = new ApplicationDbContext())
            {
                var user = await db.Users.FirstOrDefaultAsync(x => x.UserName == UserName);
                if (user != null && request != null)
                {
                    user.EmailTime = request.Time.TimeOfDay;
                    await db.SaveChangesAsync();
                }
            }
        }

        public bool CreateOnlinerProduct(ProductModel model, string UserEmail)
        {
            return repository.CreateOnlinerProduct(model, UserEmail);
        }

        public PersonalPageResponse PersonalProductsResponse(string UserName)
        {
            var result =repository.GetPersonalProducts(UserName).Select(x => productMapper.ConvertToModel(x));
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

        public Task<bool> RemoveOnlinerProduct(int itemId, string name)
        {
            return repository.RemoveOnlinerProduct(itemId, name);
        }
    }
}
