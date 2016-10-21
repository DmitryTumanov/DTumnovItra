using System.Web.Mvc;
using FluentScheduler;
using OnlinerTask.BLL.Services.Job;

namespace OnlinerTask.WEB.TimeRegistry
{
    public class ProductRefreshJob : IJob
    {
        private readonly IProductJob productJob;

        public ProductRefreshJob()
        {
            productJob = DependencyResolver.Current.GetService<IProductJob>();
        }

        public async void Execute()
        {
            await productJob.Execute();
        }
    }
}
