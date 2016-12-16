using Newtonsoft.Json;

namespace OnlinerTask.Data.SearchModels
{
    public class ProductModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("key")]
        public string Key { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("full_name")]
        public string FullName { get; set; }
        [JsonProperty("images")]
        public ImageModel Images { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("html_url")]
        public string HtmlUrl { get; set; }
        [JsonProperty("reviews")]
        public ReviewModel Reviews { get; set; }
        [JsonProperty("review_url")]
        public string ReviewUrl { get; set; }
        [JsonProperty("prices")]
        public PriceModel Prices { get; set; }
        [JsonProperty("is_checked")]
        public bool IsChecked { get; set; }
    }
}