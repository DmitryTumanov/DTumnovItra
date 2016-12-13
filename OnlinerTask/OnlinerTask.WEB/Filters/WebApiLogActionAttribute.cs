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
            logger.LogObject(new WebRequest(request.Method.Method, request.RequestUri.OriginalString));
            return await continuation();
        }

        public bool AllowMultiple => false;
    }
}