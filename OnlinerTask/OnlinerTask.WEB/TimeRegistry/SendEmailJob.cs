using FluentScheduler;
using OnlinerTask.Data.Repository.Interfaces;
using System;
using System.Diagnostics;
using System.Web.Mvc;
using OnlinerTask.Data.RedisManager;
using OnlinerTask.Data.ScheduleModels;
using ServiceStack;

namespace OnlinerTask.WEB.TimeRegistry
{
    public class SendEmailJob : IJob
    {
        private readonly IEmailManager emailManager;

        public SendEmailJob()
        {
            emailManager = DependencyResolver.Current.GetService<IEmailManager>();
        }
        public SendEmailJob(IEmailManager emailManager)
        {
            this.emailManager = emailManager;
        }

        public async void Execute()
        {
            var userList = emailManager.GetAll<UsersUpdateEmail>();
            var date = DateTime.Now.TimeOfDay;
            foreach (var item in userList)
            {
                if (item.Time < date)
                {
                    await emailManager.SendMail(item.UserEmail, item.ProductName);
                    emailManager.Delete(item);
                }
            }
        }
    }
}