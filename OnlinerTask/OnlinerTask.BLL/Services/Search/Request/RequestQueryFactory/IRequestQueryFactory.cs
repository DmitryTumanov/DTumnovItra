using System.Collections.Generic;
using OnlinerTask.Data.Requests;

namespace OnlinerTask.BLL.Services.Search.Request.RequestQueryFactory
{
    public interface IRequestQueryFactory
    {
        IDictionary<string, object> FromRequest(SearchRequest searchRequest);
    }
}
