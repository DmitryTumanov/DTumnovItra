using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using OnlinerTask.Data.SearchModels;
using System.Net.Http;
using OnlinerTask.BLL.Services.Products;
using OnlinerTask.BLL.Services.Search;
using OnlinerTask.Data.Requests;

namespace OnlinerTask.WEB.Controllers
{
    [Authorize]
    public class ProductController : ApiController
    {
        private readonly ISearchService searchService;
        private readonly IManager manager;

        public ProductController(ISearchService searchService, IManager manager)
        {
            this.searchService = searchService;
            this.manager = manager;
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
            await manager.AddProduct(request, testname ?? User.Identity.Name);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
        
        public async Task<HttpResponseMessage> Delete(DeleteRequest request, string testname = null)
        {
            await manager.RemoveProduct(request, testname ?? User.Identity.Name);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
