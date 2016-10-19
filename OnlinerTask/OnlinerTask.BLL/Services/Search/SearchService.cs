using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using Newtonsoft.Json;
using OnlinerTask.Data.Repository.Interfaces;
using OnlinerTask.Data.Requests;
using OnlinerTask.Data.SearchModels;

namespace OnlinerTask.BLL.Services.Search
{
    public class SearchService : ISearchService
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
            if (responseStream == null)
            {
                return null;
            }
            using (var sr = new StreamReader(responseStream))
            {
                using (var textReader = new JsonTextReader(sr))
                {
                    return serializer.Deserialize<SearchResult>(textReader);
                }
            }
        }

        public HttpWebRequest OnlinerRequest(string strRequest)
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create("https://catalog.api.onliner.by/search/products?query=" + strRequest);
            webRequest.Method = "GET";
            webRequest.ContentType = webRequest.Accept = webRequest.MediaType = "application/json";
            return webRequest;
        }

        public async Task<List<ProductModel>> GetProducts(SearchRequest searchRequest, string userName)
        {
            if (string.IsNullOrEmpty(searchRequest?.SearchString))
            {
                return null;
            }
            var request = OnlinerRequest(searchRequest.SearchString);
            var webResponse = (HttpWebResponse)(await request.GetResponseAsync());
            var result = ProductsFromOnliner(webResponse);
            return await repository.CheckProducts(result.Products, userName);
        }
    }
}