﻿using System.Text.RegularExpressions;
using FluentScheduler;
using OnlinerTask.WEB.TimeRegistry;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.WebPages;

namespace OnlinerTask.WEB
{
    public class MvcApplication : HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            JobManager.Initialize(new ProductRegistry());
            InitializeDisplayModeProviders();
        }

        protected void InitializeDisplayModeProviders()
        {
            var mobile = new DefaultDisplayMode("Mobile")
            {
                ContextCondition = x=>x.GetOverriddenUserAgent()!=null &&
                    (Regex.IsMatch(x.GetOverriddenUserAgent(), @"mobile|android|kindle|silk|midp", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant) ||
                    x.GetOverriddenUserAgent().Contains("IPhone"))
            };

            DisplayModeProvider.Instance.Modes.Insert(0, mobile);
        }
    }
}
