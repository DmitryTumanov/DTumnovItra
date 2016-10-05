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

        public string Get()
        {
            return "value";
        }
        
        public async Task<List<ProductModel>> Post(Responce responce)
        {
            if (responce == null || String.IsNullOrEmpty(responce.SearchString))
                return null;
            HttpWebRequest request = SearchService.OnlinerRequest(responce.SearchString);
            HttpWebResponse webResponse = (HttpWebResponse)(await request.GetResponseAsync());
            var result = SearchService.ProductsFromOnliner(webResponse);
            return result.products;
        }
        
        public void Put(int id, [FromBody]string value)
        {
        }
        
        public void Delete(int id)
        {
        }
    }
}
