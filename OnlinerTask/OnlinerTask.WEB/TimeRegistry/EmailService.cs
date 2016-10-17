using System.Diagnostics;
using System.Web.Mvc;
using OnlinerTask.Data.RedisManager;
using OnlinerTask.Data.ScheduleModels;
using ServiceStack;

namespace OnlinerTask.WEB.TimeRegistry
{
    public class EmailService : Service
    {
        private readonly IEmailManager manager;
        public EmailService()
        {
            this.manager = DependencyResolver.Current.GetService<IEmailManager>();
        }
        public object Any(UsersUpdateEmail req)
        {
            Debug.WriteLine($"##########################Get req {req.UserEmail} {req.ProductName}");
            manager.Set(req);
            return new object();
        }
    }
}
