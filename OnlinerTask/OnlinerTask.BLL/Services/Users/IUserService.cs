using System.Collections.Generic;
using System.Threading.Tasks;
using OnlinerTask.Data.UserModels;

namespace OnlinerTask.BLL.Services.Users
{
    public interface IUserService
    {
        IEnumerable<UserModel> GetAllUsers();
        Task RemoveUser(string userName);
        Task ChangeUserRole(UserModel user);
    }
}
