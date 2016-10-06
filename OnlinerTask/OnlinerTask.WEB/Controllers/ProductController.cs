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

        public string Get()
        {
            return "value";
        }
        
        public async Task<List<ProductModel>> Post(Request responce)
        {
            if (responce == null || String.IsNullOrEmpty(responce.SearchString))
                return null;
            HttpWebRequest request = search_service.OnlinerRequest(responce.SearchString);
            HttpWebResponse webResponse = (HttpWebResponse)(await request.GetResponseAsync());
            var result = search_service.ProductsFromOnliner(webResponse);
            result.Products.ForEach(i =>
            {
                if (repository.CheckItem(i.Id, User.Identity.Name)) i.IsChecked = true;
            });
            return result.Products;
        }
        
        public async void Put(Request responce)
        {
            var result = (await Post(responce)).FirstOrDefault();
            repository.CreateOnlinerProduct(result, User.Identity.Name);
            return;
        }
        
        public void Delete(DeleteRequest request)
        {
            repository.RemoveOnlinerProduct(request.ItemId, User.Identity.Name);
            return;
        }
    }
}
