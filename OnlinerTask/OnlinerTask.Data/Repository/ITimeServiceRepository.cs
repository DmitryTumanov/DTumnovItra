using System.Threading.Tasks;
using OnlinerTask.Data.Requests;

namespace OnlinerTask.Data.Repository
{
    public interface ITimeServiceRepository
    {
        Task ChangeSendEmailTimeAsync(TimeRequest request, string userName);
    }
}
