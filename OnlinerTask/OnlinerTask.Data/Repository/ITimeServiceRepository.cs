using System;
using System.Collections.Generic;
using OnlinerTask.Data.DataBaseModels;
using OnlinerTask.Data.ScheduleModels;
using OnlinerTask.Data.SearchModels;

namespace OnlinerTask.Data.Repository
{
    public interface ITimeServiceRepository
    {
        bool UpdateProduct(Product item, ProductModel model);
        UsersUpdateEmail WriteUpdateToProduct(ProductModel item, TimeSpan time);
        UsersUpdateEmail WriteUpdate(ProductModel item, string useremail);
        List<Product> GetAllProducts();
    }
}
