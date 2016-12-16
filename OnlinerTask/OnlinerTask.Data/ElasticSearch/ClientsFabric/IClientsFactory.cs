using Nest;

namespace OnlinerTask.Data.ElasticSearch.ClientsFabric
{
    public interface IClientsFactory
    {
        ElasticClient CreateClient(ConnectionSettings settings, string defaultIndex = "");
    }
}
