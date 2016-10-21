using System.Threading.Tasks;
using ServiceStack.Messaging;

namespace OnlinerTask.BLL.Services.Job
{
    public interface IProductJob: IJobExecute
    {
        IMessageQueueClient CreateClient();

        void CreateAppHost();

        Task GetAndPublishUpdates(IMessageQueueClient mqClient);
    }
}
