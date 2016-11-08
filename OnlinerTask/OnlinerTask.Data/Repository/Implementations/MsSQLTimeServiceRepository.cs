using System.Data.Entity;
using System.Threading.Tasks;
using OnlinerTask.Data.DataBaseModels;
using OnlinerTask.Data.EntityMappers;
using OnlinerTask.Data.IdentityModels;
using OnlinerTask.Data.Requests;
using OnlinerTask.Data.SearchModels;

namespace OnlinerTask.Data.Repository.Implementations
{
    public class MsSqlTimeServiceRepository : ITimeServiceRepository
    {
        public readonly IRepository repository;

        public MsSqlTimeServiceRepository(IRepository repository, IProductMapper<Product, ProductModel> productMapper)
        {
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
    }
}
