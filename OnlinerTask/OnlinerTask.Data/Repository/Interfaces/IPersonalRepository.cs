using OnlinerTask.Data.Requests;
using OnlinerTask.Data.Responses;
using OnlinerTask.Data.SearchModels;
using System.Threading.Tasks;

namespace OnlinerTask.Data.Repository.Interfaces
{
    public interface IPersonalRepository
    {
        Task ChangeSendEmailTimeAsync(TimeRequest request, string userName);
        PersonalPageResponse PersonalProductsResponse(string userName);
        Task<string> RemoveOnlinerProduct(int itemId, string name);
        bool CreateOnlinerProduct(ProductModel model, string userEmail);
    }
}
