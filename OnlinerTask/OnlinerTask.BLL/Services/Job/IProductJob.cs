using System.Threading.Tasks;
using ServiceStack.Messaging;

namespace OnlinerTask.BLL.Services.Job
{
    public interface IProductJob: IJobExecute
    {
        Task GetAndPublishUpdates(IMessageQueueClient mqClient);
    }
}
