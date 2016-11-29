using System;
using Nest;

namespace OnlinerTask.BLL.Services.ElasticSearch.ProductLogger.ConnectionFabric.Implementations
{
    public class ElasticConnectionFactory : IConnectionFactory
    {
        public ConnectionSettings CreateConnection(string urlPath, string defaultIndex = "")
        {
            var node = new Uri(urlPath);
            return new ConnectionSettings(node).DefaultIndex(defaultIndex);
        }
    }
}
