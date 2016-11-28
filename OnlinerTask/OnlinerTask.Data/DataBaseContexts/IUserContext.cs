using System.Data.Entity;
using System.Threading.Tasks;
using OnlinerTask.Data.IdentityModels;

namespace OnlinerTask.Data.DataBaseContexts
{
    public interface IUserContext
    {
        IDbSet<ApplicationUser> Users { get; set; }

        Task SaveChangesAsync();
    }
}
