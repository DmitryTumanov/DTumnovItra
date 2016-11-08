using System.Threading.Tasks;
using OnlinerTask.Data.Requests;

namespace OnlinerTask.BLL.Services.TimeChange
{
    public interface ITimeChanger
    {
        Task ChangePersonalTime(TimeRequest request, string userName);
    }
}
