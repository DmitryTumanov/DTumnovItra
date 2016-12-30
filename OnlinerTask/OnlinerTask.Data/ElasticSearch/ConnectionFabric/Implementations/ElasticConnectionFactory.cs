using System;
using Nest;
using OnlinerTask.Data.Resources;

namespace OnlinerTask.Data.ElasticSearch.ConnectionFabric.Implementations
{
    public class ElasticConnectionFactory : IConnectionFactory
    {
        public ConnectionSettings CreateConnection(string urlPath, string defaultIndex = "")
        {
            var node = new Uri(urlPath);
            var settings = new ConnectionSettings(node).DefaultIndex(defaultIndex)
                                        .BasicAuthentication(Configurations.ElasticLogin, Configurations.ElasticPassword);
            return settings;
        }
    }
}
