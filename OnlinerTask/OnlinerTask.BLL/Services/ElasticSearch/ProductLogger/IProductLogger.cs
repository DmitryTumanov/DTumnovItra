using System.Threading.Tasks;
using OnlinerTask.Data.SearchModels;

namespace OnlinerTask.BLL.Services.ElasticSearch.ProductLogger
{
    public interface IProductLogger
    {
        Task LogAdding(ProductModel productModel);

        Task RemoveLog(int productId);
    }
}
