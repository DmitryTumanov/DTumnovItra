﻿using System.Collections.Generic;
using OnlinerTask.Data.Requests;

namespace OnlinerTask.BLL.Services.Search.Request.RequestQueryFactory.Implementations
{
    public class OnlinerRequestQueryFactory : IRequestQueryFactory
    {
        public IDictionary<string, object> FromRequest(SearchRequest searchRequest)
        {
            if (searchRequest == null)
            {
                return null;
            }
            return new Dictionary<string, object>
            {
                {"query", searchRequest.SearchString},
                {"page", searchRequest.PageNumber}
            };
        }
    }
}
