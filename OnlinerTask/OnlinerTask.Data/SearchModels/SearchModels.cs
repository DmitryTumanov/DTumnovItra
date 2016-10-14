using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace OnlinerTask.Data.SearchModels
{
    public class SearchResult
    {
        [JsonProperty("products")]
        public List<ProductModel> Products { get; set; }
    }
}