using OnlinerTask.Data.SearchModels;
using OnlinerTask.Data.Repository;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using OnlinerTask.Data.Requests;

namespace OnlinerTask.BLL.Services
{
    public interface ISearchService
    {
        SearchResult ProductsFromOnliner(HttpWebResponse webResponse);

        HttpWebRequest OnlinerRequest(string strRequest);

        Task<List<ProductModel>> GetProducts(SearchRequest responce, string UserName);
    }
}
