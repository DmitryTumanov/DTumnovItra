using Newtonsoft.Json;

namespace OnlinerTask.Data.SearchModels
{
    public class PriceAmmountModel
    {
        [JsonProperty("amount")]
        public double Amount { get; set; }
        [JsonProperty("currency")]
        public string Currency { get; set; }
    }
}
