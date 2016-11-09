using System.Net;
using OnlinerTask.Data.SearchModels;

namespace OnlinerTask.BLL.Services.Search.ProductParser
{
    public interface IProductParser
    {
        SearchResult ParseProductsFromRequest(HttpWebResponse webResponse);
    }
}
