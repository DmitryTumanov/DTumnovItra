using Newtonsoft.Json;
using OnlinerTask.Data.SearchModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using OnlinerTask.Data.Repository;
using OnlinerTask.Data.Requests;

namespace OnlinerTask.BLL.Services
{
    public class SearchService: ISearchService
    {
        public SearchResult ProductsFromOnliner(HttpWebResponse webResponse)
        {
            Stream responseStream = webResponse.GetResponseStream();
            var serializer = new JsonSerializer();
            using (var sr = new StreamReader(responseStream))
            {
                using(var text_reader = new JsonTextReader(sr))
                {
                    return serializer.Deserialize<SearchResult>(text_reader);
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

        public async Task<List<ProductModel>> GetProducts(SearchRequest responce, IRepository repository, string UserName)
        {
            if (responce == null || string.IsNullOrEmpty(responce.SearchString))
                return null;
            HttpWebRequest request = OnlinerRequest(responce.SearchString);
            HttpWebResponse webResponse = (HttpWebResponse)(await request.GetResponseAsync());
            var result = ProductsFromOnliner(webResponse);
            return await repository.CheckProducts(result.Products, UserName);
        }
    }
}