using System.Collections.Generic;
using System.Threading.Tasks;
using OnlinerTask.Data.Requests;
using OnlinerTask.Data.SearchModels;

namespace OnlinerTask.BLL.Services.Search
{
    public interface ISearchService
    {
        Task<List<ProductModel>> GetProducts(SearchRequest responce, string userName);
    }
}
