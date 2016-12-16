using System.Threading.Tasks;
using OnlinerTask.Data.ElasticSearch.LoggerModels;

namespace OnlinerTask.Data.ElasticSearch.UserActivityLogger
{
    public interface IActivityLogger
    {
        Task LogRequest(WebRequest request);
    }
}
