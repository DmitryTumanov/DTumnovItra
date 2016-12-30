using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using OnlinerTask.Data.DataBaseContexts;
using OnlinerTask.Data.IdentityModels;
using OnlinerTask.Data.UserModels;

namespace OnlinerTask.Data.Repository.Implementations
{
    public class MsSqlUserRepository : IUserRepository
    {
        private readonly IUserContext context;
        private readonly UserStore<ApplicationUser> userStore;
        private readonly UserManager<ApplicationUser> userManager;

        public MsSqlUserRepository(IUserContext context)
        {
            this.context = context;
            userStore = new UserStore<ApplicationUser>((ApplicationDbContext)this.context);
            userManager = new UserManager<ApplicationUser>(userStore);
        }

        public IEnumerable<UserModel> GetAllUsers()
        {
            var users = new List<UserModel>();
            
            foreach (var user in userStore.Users)
            {
                var userModel = new UserModel
                {
                    Name = user.UserName
                };
                users.Add(userModel);
            }
            foreach (var user in users)
            {
                user.IsAdmin = userManager.GetRoles(userStore.Users.First(s => s.UserName == user.Name).Id).Contains("Admin");
            }

            return users;
        }

        public async Task RemoveUser(string name)
        {
            var model = await context.Users.FirstOrDefaultAsync(x => x.UserName == name);

            if (model == null)
            {
                return;
            }
            context.Users.Remove(model);
            await context.SaveChangesAsync();
        }

        public async Task UpdateUserRole(UserModel user)
        {
            var model = await context.Users.FirstOrDefaultAsync(x => x.UserName == user.Name);

            if (model == null)
            {
                return;
            }
            await userManager.RemoveFromRoleAsync(model.Id, user.IsAdmin ? "Admin" : "User");
            await userManager.AddToRoleAsync(model.Id, !user.IsAdmin ? "Admin" : "User");
        }
    }
}
