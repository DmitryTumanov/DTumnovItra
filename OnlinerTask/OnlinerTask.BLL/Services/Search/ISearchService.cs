using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using OnlinerTask.Data.Requests;
using OnlinerTask.Data.SearchModels;

namespace OnlinerTask.BLL.Services.Search
{
    public interface ISearchService
    {
        SearchResult ProductsFromOnliner(HttpWebResponse webResponse);

        HttpWebRequest OnlinerRequest(string strRequest);

        Task<List<ProductModel>> GetProducts(SearchRequest responce, string userName);
    }
}
