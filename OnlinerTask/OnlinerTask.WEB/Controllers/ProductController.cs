using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using OnlinerTask.Data.SearchModels;
using System.Linq;
using System.Net.Http;
using OnlinerTask.BLL.Services.Job;
using OnlinerTask.BLL.Services.Search;
using OnlinerTask.Data.Repository;
using OnlinerTask.Data.Requests;

namespace OnlinerTask.WEB.Controllers
{
    [Authorize]
    public class ProductController : ApiController
    {
        private readonly ISearchService searchService;
        private readonly IProductRepository repository;
        private readonly INotification notify;

        public ProductController(ISearchService service, IProductRepository repo, INotification notify)
        {
            searchService = service;
            repository = repo;
            this.notify = notify;
        }

        public HttpResponseMessage Get()
        {
            return Request.CreateResponse(HttpStatusCode.OK);
        }
        
        public async Task<List<ProductModel>> Post(SearchRequest request)
        {
            return await searchService.GetProducts(request, User.Identity.Name);
        }

        public async Task<HttpResponseMessage> Put(PutRequest request, string testname = null)
        {
            var result = (await Post(request)).FirstOrDefault();
            repository.CreateOnlinerProduct(result, testname ?? User.Identity.Name);
            notify.AddProductFromSearch(result.FullName);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
        
        public async Task<HttpResponseMessage> Delete(DeleteRequest request, string testname = null)
        {
            var name = await repository.RemoveOnlinerProduct(request.ItemId, testname ?? User.Identity.Name);
            notify.DeleteProductFromSearch(name);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
