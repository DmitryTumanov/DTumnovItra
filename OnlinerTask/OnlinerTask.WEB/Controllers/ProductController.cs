using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using OnlinerTask.DAL.SearchModels;
using OnlinerTask.BLL.Services;
using System.Linq;
using OnlinerTask.BLL.Repository;

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

        public void Get()
        {

        }
        
        public async Task<List<ProductModel>> Post(Request responce)
        {
            return await search_service.GetProducts(responce, repository, User.Identity.Name);
        }

        public async void Put(Request responce)
        {
            var result = (await Post(responce)).FirstOrDefault();
            repository.CreateOnlinerProduct(result, User.Identity.Name);
        }
        
        public void Delete(DeleteRequest request)
        {
            repository.RemoveOnlinerProduct(request.ItemId, User.Identity.Name);
            return;
        }
    }
}
