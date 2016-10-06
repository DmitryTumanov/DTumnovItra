using Newtonsoft.Json;
using OnlinerTask.DAL.SearchModels;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;

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
            //    DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(SearchResult));
            //return (SearchResult)jsonSerializer.ReadObject(responseStream);
        }

        public HttpWebRequest OnlinerRequest(string strRequest)
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create("https://catalog.api.onliner.by/search/products?query=" + strRequest);
            webRequest.Method = "GET";
            webRequest.ContentType = webRequest.Accept = webRequest.MediaType = "application/json";
            return webRequest;
        }
    }
}