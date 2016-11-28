using System.Threading.Tasks;
using OnlinerTask.Data.Requests;
using OnlinerTask.Data.Responses;

namespace OnlinerTask.BLL.Services.Products
{
    public interface IManager
    {
        Task AddProduct(PutRequest request, string name);
        Task RemoveProduct(DeleteRequest request, string name);
        Task<PersonalPageResponse> GetProducts(string name);
    }
}
