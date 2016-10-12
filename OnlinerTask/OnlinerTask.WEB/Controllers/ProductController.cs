using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using OnlinerTask.Data.SearchModels;
using OnlinerTask.BLL.Services;
using System.Linq;
using OnlinerTask.Data.Repository;
using System.Net.Http;
using OnlinerTask.Data.Requests;

namespace OnlinerTask.WEB.Controllers
{
    [Authorize]
    public class ProductController : ApiController
    {
        private ISearchService search_service;
        private IRepository repository;

        public ProductController(ISearchService service, IRepository repo)
        {
            search_service = service;
            repository = repo;
        }

        public HttpResponseMessage Get()
        {
            return Request.CreateResponse(HttpStatusCode.OK);
        }
        
        public async Task<List<ProductModel>> Post(SearchRequest request)
        {
            return await search_service.GetProducts(request, repository, User.Identity.Name);
        }

        public async Task Put(PutRequest request, string testname = null)
        {
            var result = (await Post(request)).FirstOrDefault();
            repository.CreateOnlinerProduct(result, testname ?? User.Identity.Name);
        }
        
        public async Task Delete(DeleteRequest request, string testname = null)
        {
            await repository.RemoveOnlinerProduct(request.ItemId, testname ?? User.Identity.Name);
        }
    }
}
