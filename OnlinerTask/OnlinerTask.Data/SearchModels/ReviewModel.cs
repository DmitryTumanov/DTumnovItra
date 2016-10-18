using Newtonsoft.Json;

namespace OnlinerTask.Data.SearchModels
{
    public class ReviewModel
    {
        [JsonProperty("rating")]
        public int Rating { get; set; }
        [JsonProperty("count")]
        public int Count { get; set; }
        [JsonProperty("html_url")]
        public string HtmlUrl { get; set; }
    }
}
