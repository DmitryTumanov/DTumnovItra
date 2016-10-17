using OnlinerTask.Data.DataBaseModels;
using OnlinerTask.Data.ScheduleModels;
using System;
using System.Collections.Generic;
using OnlinerTask.Data.SearchModels;

namespace OnlinerTask.Data.Repository.Interfaces
{
    public interface ITimeServiceRepository
    {
        void DeleteUserAndProduct(int id, string userEmail);
        bool UpdateProduct(Product item, ProductModel model);
        UsersUpdateEmail WriteUpdateToProduct(ProductModel item, TimeSpan time);
        UsersUpdateEmail WriteUpdate(ProductModel item, string useremail);
        IEnumerable<UsersUpdateEmail> GetUsersEmails();
        List<Product> GetAllProducts();
    }
}
