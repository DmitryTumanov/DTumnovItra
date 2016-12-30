using System;
using System.Diagnostics;
using FluentScheduler;
using System.Web.Mvc;
using OnlinerTask.BLL.Services.Job.EmailJob;

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
            try
            {
                await emailJob.Execute();
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception.InnerException);
            }
        }
    }
}