using OnlinerTask.Data.DataBaseModels;
using System;
using System.Collections.Generic;

namespace OnlinerTask.Data.Repository.Interfaces
{
    public interface ITimeServiceRepository
    {
        void DeleteUserAndProduct(int id, string userEmail);
        bool UpdateProduct(Product item);
        bool WriteUpdateToProduct(Product item, TimeSpan time);
        void WriteUpdate(Product item);
        IEnumerable<UsersUpdateEmail> GetUsersEmails();
        List<Product> GetAllProducts();
    }
}
