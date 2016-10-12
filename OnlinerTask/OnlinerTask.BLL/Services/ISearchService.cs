using OnlinerTask.DAL.SearchModels;
using OnlinerTask.Data.Repository;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace OnlinerTask.BLL.Services
{
    public interface ISearchService
    {
        SearchResult ProductsFromOnliner(HttpWebResponse webResponse);

        HttpWebRequest OnlinerRequest(string strRequest);

        Task<List<ProductModel>> GetProducts(Request responce, IRepository repository, string UserName);
    }
}
