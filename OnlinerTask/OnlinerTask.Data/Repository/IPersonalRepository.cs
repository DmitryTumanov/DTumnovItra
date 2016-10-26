using System.Threading.Tasks;
using OnlinerTask.Data.Requests;
using OnlinerTask.Data.Responses;
using OnlinerTask.Data.SearchModels;

namespace OnlinerTask.Data.Repository
{
    public interface IPersonalRepository
    {
        Task ChangeSendEmailTimeAsync(TimeRequest request, string userName);
        PersonalPageResponse PersonalProductsResponse(string userName);
        Task<string> RemoveOnlinerProduct(int itemId, string name);
        bool CreateOnlinerProduct(ProductModel model, string userEmail);
    }
}
