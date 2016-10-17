using Newtonsoft.Json;

namespace OnlinerTask.Data.SearchModels
{
    public class ImageModel
    {
        [JsonProperty("header")]
        public string Header { get; set; }
    }
}
