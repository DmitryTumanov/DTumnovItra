using Newtonsoft.Json;
using OnlinerTask.Data.SearchModels;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using OnlinerTask.Data.Requests;
using OnlinerTask.Data.Repository.Interfaces;

namespace OnlinerTask.BLL.Services
{
    public class SearchService: ISearchService
    {
        private readonly IProductRepository repository;
        public SearchService(IProductRepository repository)
        {
            this.repository = repository;
        }

        public SearchResult ProductsFromOnliner(HttpWebResponse webResponse)
        {
            var responseStream = webResponse.GetResponseStream();
            var serializer = new JsonSerializer();
            if (responseStream != null)
            {
                using (var sr = new StreamReader(responseStream))
                {
                    using (var textReader = new JsonTextReader(sr))
                    {
                        return serializer.Deserialize<SearchResult>(textReader);
                    }
                }
            }
            else
            {
                return null;
            }
        }

        public HttpWebRequest OnlinerRequest(string strRequest)
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create("https://catalog.api.onliner.by/search/products?query=" + strRequest);
            webRequest.Method = "GET";
            webRequest.ContentType = webRequest.Accept = webRequest.MediaType = "application/json";
            return webRequest;
        }

        public async Task<List<ProductModel>> GetProducts(SearchRequest responce, string userName)
        {
            if (string.IsNullOrEmpty(responce?.SearchString))
                return null;
            var request = OnlinerRequest(responce.SearchString);
            var webResponse = (HttpWebResponse)(await request.GetResponseAsync());
            var result = ProductsFromOnliner(webResponse);
            return await repository.CheckProducts(result.Products, userName);
        }
    }
}