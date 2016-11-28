using System.Collections.Generic;
using OnlinerTask.Data.ScheduleModels;
using OnlinerTask.Data.SearchModels;

namespace OnlinerTask.Data.Repository
{
    public interface IProductRepository
    {
        List<ProductModel> CheckProducts(List<ProductModel> products, string userName);
        UsersUpdateEmail WriteUpdate(ProductModel item, string useremail);
    }
}
