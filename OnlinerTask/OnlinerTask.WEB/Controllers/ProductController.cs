using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using OnlinerTask.DAL.SearchModels;
using OnlinerTask.BLL.Services;

namespace OnlinerTask.WEB.Controllers
{
    [Authorize]
    public class ProductController : ApiController
    {
        private ISearchService search_service;

        public ProductController(ISearchService service)
        {
            search_service = service;
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
            return result.Products;
        }
        
        public void Put(int id, [FromBody]string value)
        {
        }
        
        public void Delete(int id)
        {
        }
    }
}
