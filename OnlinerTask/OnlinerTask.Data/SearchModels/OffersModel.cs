using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlinerTask.Data.SearchModels
{
    public class OffersModel
    {
        [JsonProperty("count")]
        public int Count { get; set; }
    }
}
