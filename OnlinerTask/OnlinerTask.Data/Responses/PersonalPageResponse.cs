using Newtonsoft.Json;
using OnlinerTask.Data.SearchModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlinerTask.Data.Responses
{
    public class PersonalPageResponse
    {
        [JsonProperty("products")]
        public List<ProductModel> Products { get; set; }

        [JsonProperty("emailtime")]
        public DateTime EmailTime { get; set; }
    }
}
