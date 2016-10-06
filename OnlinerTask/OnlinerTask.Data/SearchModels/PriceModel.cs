using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlinerTask.DAL.SearchModels
{
    public class PriceModel
    {
        [JsonProperty("price_min")]
        public PriceAmmountModel PriceMin { get; set; }
        [JsonProperty("price_max")]
        public PriceAmmountModel PriceMax { get; set; }
        [JsonProperty("html_url")]
        public string HtmlUrl { get; set; }
        [JsonProperty("offers")]
        public OffersModel Offers { get; set; }
    }
}
