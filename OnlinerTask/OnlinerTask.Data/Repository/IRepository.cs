using OnlinerTask.Data.SearchModels;
using OnlinerTask.Data.DataBaseModels;
using OnlinerTask.Data.Responses;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using OnlinerTask.Data.Requests;

namespace OnlinerTask.Data.Repository
{
    public interface IRepository
    {
        bool CreateOnlinerProduct(ProductModel model, string UserEmail);
        int CreatePriceAmmount(PriceAmmount price);
        bool CreateProduct(Product product);
        Task<bool> RemoveOnlinerProduct(int itemId, string name);
        Task<bool> CheckItem(int ItemId, string Username);
        Task<List<ProductModel>> CheckProducts(List<ProductModel> products, string UserName);
        IEnumerable<UsersUpdateEmail> GetUsersEmails();
        List<Product> GetPersonalProducts(string name);
        List<Product> GetAllProducts();
        void DeleteUserAndProduct(int id, string userEmail);
        bool UpdateProduct(Product item);
        PersonalPageResponse PersonalProductsResponse(string UserName);
        bool WriteUpdateToProduct(Product item, TimeSpan time);
        void WriteUpdate(Product item);
        void ChangeSendEmailTime(TimeRequest request, string UserName);
    }
}
