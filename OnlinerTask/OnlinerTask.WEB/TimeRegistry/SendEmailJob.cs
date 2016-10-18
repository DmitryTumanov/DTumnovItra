using FluentScheduler;
using System.Web.Mvc;
using OnlinerTask.BLL.Services.Job.Interfaces;

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