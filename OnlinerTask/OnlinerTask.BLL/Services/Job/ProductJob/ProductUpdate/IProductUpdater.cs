using System.Threading.Tasks;
using OnlinerTask.Data.DataBaseModels;
using OnlinerTask.Data.SearchModels;

namespace OnlinerTask.BLL.Services.Job.ProductJob.ProductUpdate
{
    public interface IProductUpdater
    {
        Task<ProductModel> GetUpdate(Product databaseModel);
    }
}
