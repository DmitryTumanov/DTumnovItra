﻿using System.IO;
using System.Net;
using Newtonsoft.Json;
using OnlinerTask.Data.SearchModels;

namespace OnlinerTask.BLL.Services.Search.ProductParser.Implementations
{
    public class OnlinerProductParser : IProductParser
    {
        public SearchResult FromResponse(HttpWebResponse webResponse)
        {
            var responseStream = webResponse.GetResponseStream();
            var serializer = new JsonSerializer();
            if (responseStream == null)
            {
                return null;
            }
            using (var sr = new StreamReader(responseStream))
            {
                using (var textReader = new JsonTextReader(sr))
                {
                    return serializer.Deserialize<SearchResult>(textReader);
                }
            }
        }
    }
}
