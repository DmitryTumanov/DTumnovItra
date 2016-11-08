using System.Threading.Tasks;
using OnlinerTask.Data.Responses;

namespace OnlinerTask.Data.Repository
{
    public interface IPersonalRepository
    {
        Task<PersonalPageResponse> PersonalProductsResponse(string userName);
    }
}
