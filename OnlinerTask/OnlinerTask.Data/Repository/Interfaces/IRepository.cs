using OnlinerTask.Data.DataBaseModels;
using OnlinerTask.Data.SearchModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace OnlinerTask.Data.Repository.Interfaces
{
    public interface IRepository
    {
        Task<bool> RemoveOnlinerProduct(int itemId, string name);
        bool CreateOnlinerProduct(ProductModel model, string UserEmail);
        int CreatePriceAmmount(PriceAmmount price);
        bool CreateProduct(Product product);
        List<Product> GetPersonalProducts(string name);
    }
}
