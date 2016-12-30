using System.Collections.Generic;
using OnlinerTask.Data.Repository;
using OnlinerTask.Data.UserModels;
using System.Threading.Tasks;
using System;

namespace OnlinerTask.BLL.Services.Users.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository userRepository;

        public UserService(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public async Task ChangeUserRole(UserModel user)
        {
            await userRepository.UpdateUserRole(user);
        }

        public IEnumerable<UserModel> GetAllUsers()
        {
            return userRepository.GetAllUsers();
        }

        public async Task RemoveUser(string userName)
        {
            await userRepository.RemoveUser(userName);
        }
    }
}
