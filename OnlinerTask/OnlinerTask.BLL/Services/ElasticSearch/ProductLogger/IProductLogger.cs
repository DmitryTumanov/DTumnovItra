using System.Threading.Tasks;
using OnlinerTask.Data.SearchModels;

namespace OnlinerTask.BLL.Services.ElasticSearch.ProductLogger
{
    public interface IProductLogger
    {
        Task LogAdding(int productId, ProductModel productModel);

        Task RemoveLog(int productId);
    }
}
