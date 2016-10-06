using OnlinerTask.DAL.SearchModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace OnlinerTask.BLL.Services
{
    public interface ISearchService
    {
        SearchResult ProductsFromOnliner(HttpWebResponse webResponse);

        HttpWebRequest OnlinerRequest(string strRequest);
    }
}
