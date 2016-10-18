using System.Threading.Tasks;
using ServiceStack.Messaging;

namespace OnlinerTask.BLL.Services.Job.Interfaces
{
    public interface IProductJob: IJobExecute
    {
        IMessageQueueClient CreateClient();

        void CreateAppHost();

        Task GetAndPublishUpdates(IMessageQueueClient mqClient);
    }
}
