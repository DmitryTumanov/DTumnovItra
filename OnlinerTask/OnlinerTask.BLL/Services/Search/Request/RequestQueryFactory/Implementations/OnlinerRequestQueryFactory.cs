using System.Collections.Generic;
using OnlinerTask.Data.Requests;

namespace OnlinerTask.BLL.Services.Search.Request.RequestQueryFactory.Implementations
{
    public class OnlinerRequestQueryFactory : IRequestQueryFactory
    {
        public IDictionary<string, string> FromRequest(SearchRequest searchRequest)
        {
            return new Dictionary<string, string>
            {
                {"searchString", searchRequest.SearchString},
                {"pageVariable", "&page="},
                {"pageNumber", searchRequest.PageNumber.ToString()}
            };
        }
    }
}
