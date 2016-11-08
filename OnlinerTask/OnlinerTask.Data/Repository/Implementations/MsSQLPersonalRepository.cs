using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using OnlinerTask.Data.DataBaseContexts;
using OnlinerTask.Data.DataBaseModels;
using OnlinerTask.Data.EntityMappers;
using OnlinerTask.Data.Responses;
using OnlinerTask.Data.SearchModels;

namespace OnlinerTask.Data.Repository.Implementations
{
    public class MsSqlPersonalRepository : IPersonalRepository
    {
        private readonly IProductMapper<Product, ProductModel> productMapper;
        private readonly IRepository repository;
        private readonly IUserContext context;

        public MsSqlPersonalRepository(IProductMapper<Product, ProductModel> productMapper, IRepository repository, IUserContext context)
        {
            this.productMapper = productMapper;
            this.repository = repository;
            this.context = context;
        }

        public async Task<PersonalPageResponse> PersonalProductsResponse(string userName)
        {
            var result = repository.GetPersonalProducts(userName).Select(x => productMapper.ConvertToModel(x));
            var time = DateTime.Now.TimeOfDay;
            var user = await context.Users.FirstOrDefaultAsync(x => x.UserName == (userName));
            if (user != null)
            {
                time = user.EmailTime;
            }
            return new PersonalPageResponse() { EmailTime = DateTime.Now.Date + time, Products = result.ToList() };
        }
    }
}
