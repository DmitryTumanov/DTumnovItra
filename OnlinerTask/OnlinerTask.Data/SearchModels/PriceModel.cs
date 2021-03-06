﻿using Newtonsoft.Json;

namespace OnlinerTask.Data.SearchModels
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
