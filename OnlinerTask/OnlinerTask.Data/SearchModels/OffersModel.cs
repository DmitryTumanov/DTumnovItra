using Newtonsoft.Json;

namespace OnlinerTask.Data.SearchModels
{
    public class OffersModel
    {
        [JsonProperty("count")]
        public int Count { get; set; }
    }
}
