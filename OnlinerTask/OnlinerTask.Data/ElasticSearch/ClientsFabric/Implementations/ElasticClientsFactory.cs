﻿using Nest;

namespace OnlinerTask.Data.ElasticSearch.ClientsFabric.Implementations
{
    public class ElasticClientsFactory : IClientsFactory
    {
        public ElasticClient CreateClient(ConnectionSettings settings, string defaultIndex = "")
        {
            var elasticClient = new ElasticClient(settings);
            if (!string.IsNullOrEmpty(defaultIndex) && !elasticClient.IndexExists(defaultIndex).Exists)
            {
                elasticClient.CreateIndex(defaultIndex);
            }
            return elasticClient;
        }
    }
}
