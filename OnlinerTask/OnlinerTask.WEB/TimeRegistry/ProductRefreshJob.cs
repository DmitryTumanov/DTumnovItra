﻿using System;
using System.Diagnostics;
using System.Net;
using System.Web.Mvc;
using FluentScheduler;
using OnlinerTask.BLL.Services.Job.ProductJob;

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
            catch (HttpListenerException exception)
            {
                Debug.WriteLine(exception.InnerException);
            }
        }
    }
}
