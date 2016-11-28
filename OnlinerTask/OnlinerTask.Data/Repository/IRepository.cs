using System.Collections.Generic;
using System.Threading.Tasks;
using OnlinerTask.Data.DataBaseModels;
using OnlinerTask.Data.SearchModels;

namespace OnlinerTask.Data.Repository
{
    public interface IRepository
    {
        Task<string> RemoveOnlinerProduct(int itemId, string name);
        bool CreateOnlinerProduct(ProductModel model, string userEmail);
        List<Product> GetPersonalProducts(string name);
        List<Product> GetAllProducts();
    }
}
