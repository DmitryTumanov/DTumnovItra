using FluentScheduler;
using System;
using System.Web.Mvc;
using OnlinerTask.BLL.Services.Job;
using OnlinerTask.Data.RedisManager;
using OnlinerTask.Data.ScheduleModels;

namespace OnlinerTask.WEB.TimeRegistry
{
    public class SendEmailJob : IJob
    {
        private readonly IEmailJob emailJob;

        public SendEmailJob()
        {
            emailJob = DependencyResolver.Current.GetService<IEmailJob>();
        }

        public async void Execute()
        {
            await emailJob.Execute();
        }
    }
}