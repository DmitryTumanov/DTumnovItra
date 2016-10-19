﻿using System.Diagnostics;
using System.Net;
using System.Web.Mvc;
using FluentScheduler;
using OnlinerTask.BLL.Services.Job.Interfaces;

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
            try
            {
                await productJob.Execute();
            }
            catch (HttpListenerException ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }
    }
}
