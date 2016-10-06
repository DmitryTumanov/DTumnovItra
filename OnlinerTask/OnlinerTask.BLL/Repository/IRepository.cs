using OnlinerTask.DAL.SearchModels;
using OnlinerTask.Data.DBModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlinerTask.BLL.Repository
{
    public interface IRepository
    {
        bool CreateOnlinerProduct(ProductModel model, string UserEmail);
        int CreatePriceAmmount(PriceAmmount price);
        bool CreateProduct(Product product);
        bool RemoveOnlinerProduct(int itemId, string name);
        void RemovePriceAmount(int? priceMaxId, int? priceMinId);
        bool CheckItem(int ItemId, string Username);
    }
}
