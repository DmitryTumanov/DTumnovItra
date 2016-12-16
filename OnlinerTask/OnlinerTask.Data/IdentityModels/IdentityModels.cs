using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using OnlinerTask.Data.DataBaseContexts;

namespace OnlinerTask.Data.IdentityModels
{
    public class ApplicationUser : IdentityUser
    {
        public TimeSpan EmailTime { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>, IUserContext
    {
        public ApplicationDbContext()
            : base("OnlinerProducts", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        async Task IUserContext.SaveChangesAsync()
        {
            await base.SaveChangesAsync();
        }
    }
}