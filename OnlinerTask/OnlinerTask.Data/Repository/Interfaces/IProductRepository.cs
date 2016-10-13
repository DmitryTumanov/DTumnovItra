using OnlinerTask.Data.SearchModels;
using OnlinerTask.Data.DataBaseModels;
using OnlinerTask.Data.Responses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OnlinerTask.Data.Requests;

namespace OnlinerTask.Data.Repository.Interfaces
{
    public interface IProductRepository
    {
        Task<bool> CheckItem(int ItemId, string Username);
        Task<List<ProductModel>> CheckProducts(List<ProductModel> products, string UserName);
        List<Product> GetPersonalProducts(string name);
        List<Product> GetAllProducts();
        Task<bool> RemoveOnlinerProduct(int itemId, string name);
        bool CreateOnlinerProduct(ProductModel model, string UserEmail);
    }
}
