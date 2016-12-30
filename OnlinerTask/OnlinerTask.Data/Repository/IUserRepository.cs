using System.Collections.Generic;
using System.Threading.Tasks;
using OnlinerTask.Data.UserModels;

namespace OnlinerTask.Data.Repository
{
    public interface IUserRepository
    {
        IEnumerable<UserModel> GetAllUsers();
        Task RemoveUser(string name);
        Task UpdateUserRole(UserModel user);
    }
}
