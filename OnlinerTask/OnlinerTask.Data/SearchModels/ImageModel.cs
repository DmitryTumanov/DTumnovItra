using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlinerTask.Data.SearchModels
{
    public class ImageModel
    {
        [JsonProperty("header")]
        public string Header { get; set; }
    }
}
