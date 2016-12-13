using System;
using System.Web.Mvc;
using OnlinerTask.BLL.Services.Logger;
using OnlinerTask.Data.ElasticSearch.LoggerModels;

namespace OnlinerTask.WEB.Filters
{
    public class MvcLogActionAttribute : FilterAttribute, IActionFilter
    {
        private readonly ILogger logger;

        public MvcLogActionAttribute()
        {
            logger = DependencyResolver.Current.GetService<ILogger>();
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var request = filterContext.HttpContext.Request;
            var requestTime = filterContext.RequestContext.HttpContext.Timestamp;
            var userName = filterContext.HttpContext.User.Identity.Name;
            logger.LogObject(new WebRequest(request.HttpMethod, request.Url?.OriginalString, requestTime, userName));
        }

        public void OnActionExecuted(ActionExecutedContext filterContext) {}
    }
}