using System.Data.Entity;
using System.Threading.Tasks;
using OnlinerTask.Data.DataBaseContexts;
using OnlinerTask.Data.IdentityModels;
using OnlinerTask.Data.Requests;

namespace OnlinerTask.Data.Repository.Implementations
{
    public class MsSqlTimeServiceRepository : ITimeServiceRepository
    {
        public readonly IRepository repository;
        private readonly IUserContext context;

        public MsSqlTimeServiceRepository(IRepository repository, IUserContext context)
        {
            this.repository = repository;
            this.context = context;
        }

        public async Task ChangeSendEmailTimeAsync(TimeRequest request, string userName)
        {
            var user = await context.Users.FirstOrDefaultAsync(x => x.UserName == userName);
            if (user != null && request != null)
            {
                user.EmailTime = request.Time.TimeOfDay;
                await context.SaveChangesAsync();
            }
        }
    }
}
