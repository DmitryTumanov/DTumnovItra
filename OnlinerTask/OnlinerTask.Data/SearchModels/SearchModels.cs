using Newtonsoft.Json;
using System.Collections.Generic;

namespace OnlinerTask.DAL.SearchModels
{
    public class SearchResult
    {
        [JsonProperty("products")]
        public List<ProductModel> Products { get; set; }
    }
}