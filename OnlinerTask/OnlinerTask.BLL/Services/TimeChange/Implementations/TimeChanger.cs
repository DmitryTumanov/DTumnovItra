using System.Threading.Tasks;
using OnlinerTask.BLL.Services.Job;
using OnlinerTask.Data.Repository;
using OnlinerTask.Data.Requests;

namespace OnlinerTask.BLL.Services.TimeChange.Implementations
{
    public class TimeChanger : ITimeChanger
    {
        private readonly IPersonalRepository repository;
        private readonly INotification notify;

        public TimeChanger(IPersonalRepository repository, INotification notify)
        {
            this.notify = notify;
            this.repository = repository;
        }

        public async Task ChangePersonalTime(TimeRequest request, string userName)
        {
            await repository.ChangeSendEmailTimeAsync(request, userName);
            notify.ChangeSettings(request.Time);
        }
    }
}
