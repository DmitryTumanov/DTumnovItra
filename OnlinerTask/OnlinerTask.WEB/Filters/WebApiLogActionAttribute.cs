using System;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http.Controllers;
using System.Web.Mvc;
using OnlinerTask.BLL.Services.Logger;
using OnlinerTask.Data.ElasticSearch.LoggerModels;
using IActionFilter = System.Web.Http.Filters.IActionFilter;

namespace OnlinerTask.WEB.Filters
{
    public class WebApiLogActionAttribute : Attribute, IActionFilter
    {
        private readonly ILogger logger;

        public WebApiLogActionAttribute()
        {
            logger = DependencyResolver.Current.GetService<ILogger>();
        }

        public async Task<HttpResponseMessage> ExecuteActionFilterAsync(HttpActionContext actionContext, CancellationToken cancellationToken, Func<Task<HttpResponseMessage>> continuation)
        {
            var request = actionContext.Request;
            var userName = actionContext.ControllerContext.RequestContext.Principal.Identity.Name;
            logger.LogObject(new WebRequest(request.Method.Method, request.RequestUri.OriginalString, DateTime.Now, userName));
            return await continuation();
        }

        public bool AllowMultiple => false;
    }
}