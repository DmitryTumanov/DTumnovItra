using System.Collections.Generic;
using Newtonsoft.Json;

namespace OnlinerTask.Data.UserModels
{
    public class UserModel
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("isadmin")]
        public bool IsAdmin { get; set; }
    }
}
