using OnlinerTask.Data.Requests;
using OnlinerTask.Data.Responses;
using OnlinerTask.Data.SearchModels;
using System.Threading.Tasks;

namespace OnlinerTask.Data.Repository.Interfaces
{
    public interface IPersonalRepository
    {
        Task ChangeSendEmailTimeAsync(TimeRequest request, string UserName);
        PersonalPageResponse PersonalProductsResponse(string UserName);
        Task<bool> RemoveOnlinerProduct(int itemId, string name);
        bool CreateOnlinerProduct(ProductModel model, string UserEmail);
    }
}
