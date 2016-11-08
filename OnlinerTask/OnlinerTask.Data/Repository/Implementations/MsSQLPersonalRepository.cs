using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using OnlinerTask.Data.DataBaseModels;
using OnlinerTask.Data.EntityMappers;
using OnlinerTask.Data.IdentityModels;
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

        public async Task<PersonalPageResponse> PersonalProductsResponse(string userName)
        {
            var result = repository.GetPersonalProducts(userName).Select(x => productMapper.ConvertToModel(x));
            using (var db = new ApplicationDbContext())
            {
                var time = DateTime.Now.TimeOfDay;
                var user = await db.Users.FirstOrDefaultAsync(x => x.UserName == (userName));
                if (user != null)
                {
                    time = user.EmailTime;
                }
                return new PersonalPageResponse() { EmailTime = DateTime.Now.Date + time, Products = result.ToList() };
            }
        }
    }
}
