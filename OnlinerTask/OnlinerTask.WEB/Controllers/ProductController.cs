using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using OnlinerTask.Data.SearchModels;
using System.Linq;
using System.Net.Http;
using OnlinerTask.BLL.Services.Job.Interfaces;
using OnlinerTask.BLL.Services.Search;
using OnlinerTask.Data.Requests;
using OnlinerTask.Data.Repository.Interfaces;

namespace OnlinerTask.WEB.Controllers
{
    [Authorize]
    public class ProductController : ApiController
    {
        private readonly ISearchService searchService;
        private readonly IProductRepository repository;
        private readonly INotification notification;

        public ProductController(ISearchService service, IProductRepository repo, INotification notification)
        {
            searchService = service;
            repository = repo;
            this.notification = notification;
        }

        public HttpResponseMessage Get()
        {
            return Request.CreateResponse(HttpStatusCode.OK);
        }
        
        public async Task<List<ProductModel>> Post(SearchRequest request)
        {
            return await searchService.GetProducts(request, User.Identity.Name);
        }

        public async Task Put(PutRequest request, string testname = null)
        {
            var result = (await Post(request)).FirstOrDefault();
            repository.CreateOnlinerProduct(result, testname ?? User.Identity.Name);
            notification.AddProductFromSearch(result.FullName);
        }
        
        public async Task Delete(DeleteRequest request, string testname = null)
        {
            var name = await repository.RemoveOnlinerProduct(request.ItemId, testname ?? User.Identity.Name);
            notification.DeleteProductFromSearch(name);
        }
    }
}
