using OnlinerTask.Models;
using System.IO;
using System.Net;
using System.Runtime.Serialization.Json;

namespace OnlinerTask.Services
{
    public class SearchService
    {
        public static SearchResult ProductsFromOnliner(HttpWebResponse webResponse)
        {
            Stream responseStream = webResponse.GetResponseStream();
            DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(SearchResult));
            return (SearchResult)jsonSerializer.ReadObject(responseStream);
        }

        public static HttpWebRequest OnlinerRequest(string strRequest)
        {
            HttpWebRequest webRequest = (HttpWebRequest)WebRequest.Create("https://catalog.api.onliner.by/search/products?query=" + strRequest);
            webRequest.Method = "GET";
            webRequest.ContentType = webRequest.Accept = webRequest.MediaType = "application/json";
            return webRequest;
        }
    }
}