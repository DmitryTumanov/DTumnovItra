using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;
using System.Web.Http;
using OnlinerTask.Models;
using OnlinerTask.Services;

namespace OnlinerTask.Controllers
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
