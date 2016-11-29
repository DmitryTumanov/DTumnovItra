using Nest;

namespace OnlinerTask.BLL.Services.ElasticSearch.ClientsFabric
{
    public interface IClientsFactory
    {
        ElasticClient CreateClient(ConnectionSettings settings, string defaultIndex = "");
    }
}
