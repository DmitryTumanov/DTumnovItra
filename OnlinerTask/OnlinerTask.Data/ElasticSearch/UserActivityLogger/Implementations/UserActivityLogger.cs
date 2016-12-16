using System.Threading.Tasks;
using Nest;
using OnlinerTask.Data.ElasticSearch.ClientsFabric;
using OnlinerTask.Data.ElasticSearch.ConnectionFabric;
using OnlinerTask.Data.ElasticSearch.LoggerModels;
using OnlinerTask.Data.Resources;

namespace OnlinerTask.Data.ElasticSearch.UserActivityLogger.Implementations
{
    public class UserActivityLogger:IActivityLogger
    {
        private readonly ElasticClient elasticClient;

        public UserActivityLogger(IClientsFactory clientsFabric, IConnectionFactory connectionFabric)
        {
            var settings = connectionFabric.CreateConnection(Configurations.ElasticSearchUrl, Configurations.ActivitiesLogIndex);
            elasticClient = clientsFabric.CreateClient(settings, Configurations.ActivitiesLogIndex);
        }

        public async Task LogRequest(WebRequest request)
        {
            var defaultIndex = elasticClient.ConnectionSettings.DefaultIndex;
            var indexType = Configurations.UserActivityIndexType;

            await elasticClient.IndexAsync(request,
                i => i.Index(defaultIndex).Type(indexType).Refresh());
        }
    }
}
