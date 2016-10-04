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

namespace OnlinerTask.Controllers
{
    [Authorize]
    public class ProductController : ApiController
    {
        // GET: api/Product
        public string Get()
        {
            return "value";
        }

        // POST: api/Product
        public async Task<List<ProductModel>> Post(Responce responce)
        {
            if (responce == null || String.IsNullOrEmpty(responce.SearchString))
                return null;
            HttpWebRequest request = Service.OnlinerRequest(responce.SearchString);
            HttpWebResponse webResponse = (HttpWebResponse)(await request.GetResponseAsync());
            var result = Service.ProductsFromOnliner(webResponse);
            return result.products;
        }

        // PUT: api/Product/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Product/5
        public void Delete(int id)
        {
        }
    }
    public class SearchResult
    {
        public List<ProductModel> products { get; set; }
    }
    public class Responce
    {
        public string SearchString { get; set; }
    }
    public class ProductModel
    {
        public class Image
        {
            public string header { get; set; }
            public string icon { get; set; }
        }
        public class Review
        {
            public int rating { get; set; }
            public int count { get; set; }
            public string html_url { get; set; }
            public string url { get; set; }
        }
        public class Price
        {
            public class Offers
            {
                public int count { get; set; }
            }
            public class PriceAmmount
            {
                public double amount { get; set; }
                public string currency { get; set; }
            }
            public PriceAmmount price_min { get; set; }
            public PriceAmmount price_max { get; set; }
            public string html_url { get; set; }
            public string url { get; set; }
            public Offers offers { get; set; }
        }
        public int id { get; set; }
        public string key { get; set; }
        public string name { get; set; }
        public string full_name { get; set; }
        public Image images { get; set; }
        public string description { get; set; }
        public string html_url { get; set; }
        public Review reviews { get; set; }
        public string review_url { get; set; }
        public string url { get; set; }
        public Price prices { get; set; }
    }

    public class Service
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
