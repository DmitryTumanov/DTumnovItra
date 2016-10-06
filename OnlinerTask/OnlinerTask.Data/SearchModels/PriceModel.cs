using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlinerTask.DAL.SearchModels
{
    public class Price
    {
        [JsonProperty("price_min")]
        public PriceAmmount PriceMin { get; set; }
        [JsonProperty("price_max")]
        public PriceAmmount PriceMax { get; set; }
        [JsonProperty("html_url")]
        public string HtmlUrl { get; set; }
        [JsonProperty("offers")]
        public Offers Offers { get; set; }
    }
}
