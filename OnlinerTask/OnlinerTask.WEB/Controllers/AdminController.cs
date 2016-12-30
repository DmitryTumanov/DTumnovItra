using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using OnlinerTask.BLL.Services.Users;
using OnlinerTask.Data.UserModels;

namespace OnlinerTask.WEB.Controllers
{
    [Authorize]
    public class AdminController : ApiController
    {
        private readonly IUserService userRepository;

        public AdminController(IUserService userRepository)
        {
            this.userRepository = userRepository;
        }

        public IEnumerable<UserModel> Get()
        {
            return userRepository.GetAllUsers();
        }

        public async Task<HttpResponseMessage> Delete(UserModel user)
        {
            await userRepository.RemoveUser(user.Name);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        public async Task<HttpResponseMessage> Put(UserModel user)
        {
            await userRepository.ChangeUserRole(user);
            return new HttpResponseMessage(HttpStatusCode.OK);
        }
    }
}