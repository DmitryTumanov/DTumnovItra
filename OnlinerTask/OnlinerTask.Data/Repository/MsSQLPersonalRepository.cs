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
    public class MsSqlPersonalRepository : IPersonalRepository
    {
        private readonly IProductMapper<Product, ProductModel> _productMapper;
        private readonly IRepository _repository;

        public MsSqlPersonalRepository(IProductMapper<Product, ProductModel> productMapper, IRepository repository)
        {
            _productMapper = productMapper;
            _repository = repository;
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
            return _repository.CreateOnlinerProduct(model, userEmail);
        }

        public PersonalPageResponse PersonalProductsResponse(string userName)
        {
            var result =_repository.GetPersonalProducts(userName).Select(x => _productMapper.ConvertToModel(x));
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
            return _repository.RemoveOnlinerProduct(itemId, name);
        }
    }
}
