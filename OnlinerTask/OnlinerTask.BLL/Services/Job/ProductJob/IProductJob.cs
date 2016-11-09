using System.Threading.Tasks;
using ServiceStack.Messaging;

namespace OnlinerTask.BLL.Services.Job.ProductJob
{
    public interface IProductJob: IJobExecute
    {
        Task GetAndPublishUpdates(IMessageQueueClient mqClient);
    }
}
