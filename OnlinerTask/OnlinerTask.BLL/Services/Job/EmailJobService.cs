using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OnlinerTask.BLL.Services.Job.Interfaces;
using OnlinerTask.Data.Extensions;
using OnlinerTask.Data.RedisManager;
using OnlinerTask.Data.ScheduleModels;

namespace OnlinerTask.BLL.Services.Job
{
    public class EmailJobService : IEmailJob
    {
        private readonly IEmailManager emailManager;

        public EmailJobService(IEmailManager emailManager)
        {
            this.emailManager = emailManager;
        }

        public async Task Execute()
        {
            await GetUsersUpdateEmails().ForEachAsync(async x =>
            {
                await emailManager.SendMail(x.UserEmail, x.ProductName);
                emailManager.Delete(x);
            });
        }

        public IEnumerable<UsersUpdateEmail> GetUsersUpdateEmails()
        {
            var actualtime = DateTime.Now.TimeOfDay;
            var interval = TimeSpan.FromSeconds(30);
            return emailManager.GetAll<UsersUpdateEmail>().Where(x=>x.Time < actualtime - interval);
        }
    }
}
