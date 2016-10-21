using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using OnlinerTask.Data.DataBaseModels;
using OnlinerTask.Data.EntityMappers;
using OnlinerTask.Data.IdentityModels;
using OnlinerTask.Data.Requests;
using OnlinerTask.Data.Responses;
using OnlinerTask.Data.SearchModels;

namespace OnlinerTask.Data.Repository.Implementations
{
    public class MsSqlPersonalRepository : IPersonalRepository
    {
        private readonly IProductMapper<Product, ProductModel> productMapper;
        private readonly IRepository repository;

        public MsSqlPersonalRepository(IProductMapper<Product, ProductModel> productMapper, IRepository repository)
        {
            this.productMapper = productMapper;
            this.repository = repository;
        }
        
        public async Task ChangeSendEmailTimeAsync(TimeRequest request, string userName)
        {
            using (var db = new ApplicationDbContext())
            {
                var user = await db.Users.FirstOrDefaultAsync(x => x.UserName == userName);
                if (user != null && request != null)
                {
                    user.EmailTime = request.Time.TimeOfDay;
                    await db.SaveChangesAsync();
                }
            }
        }

        public bool CreateOnlinerProduct(ProductModel model, string userEmail)
        {
            return repository.CreateOnlinerProduct(model, userEmail);
        }

        public PersonalPageResponse PersonalProductsResponse(string userName)
        {
            var result =repository.GetPersonalProducts(userName).Select(x => productMapper.ConvertToModel(x));
            using (var db = new ApplicationDbContext())
            {
                var time = DateTime.Now.TimeOfDay;
                var user = db.Users.FirstOrDefault(x => x.UserName == (userName));
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
