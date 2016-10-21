using System.Collections.Generic;
using System.Threading.Tasks;
using OnlinerTask.Data.DataBaseModels;
using OnlinerTask.Data.SearchModels;

namespace OnlinerTask.Data.Repository
{
    public interface IProductRepository
    {
        Task<bool> CheckItem(int itemId, string username);
        Task<List<ProductModel>> CheckProducts(List<ProductModel> products, string userName);
        List<Product> GetPersonalProducts(string name);
        List<Product> GetAllProducts();
        Task<bool> RemoveOnlinerProduct(int itemId, string name);
        bool CreateOnlinerProduct(ProductModel model, string userEmail);
    }
}
