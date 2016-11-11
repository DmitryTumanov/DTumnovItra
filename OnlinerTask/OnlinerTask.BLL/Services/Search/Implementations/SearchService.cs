using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using OnlinerTask.BLL.Services.Search.ProductParser;
using OnlinerTask.BLL.Services.Search.Request;
using OnlinerTask.Data.Repository;
using OnlinerTask.Data.Requests;
using OnlinerTask.Data.Resources;
using OnlinerTask.Data.SearchModels;

namespace OnlinerTask.BLL.Services.Search.Implementations
{
    public class SearchService : ISearchService
    {
        private readonly IProductRepository repository;
        private readonly IRequestFactory requestCreator;
        private readonly IProductParser productParser;

        public SearchService(IProductRepository repository, IRequestFactory requestCreator, IProductParser productParser)
        {
            this.repository = repository;
            this.requestCreator = requestCreator;
            this.productParser = productParser;
        }

        public async Task<List<ProductModel>> GetProducts(SearchRequest searchRequest, string userName)
        {
            if (string.IsNullOrEmpty(searchRequest?.SearchString) || searchRequest.PageNumber < 1)
            {
                return null;
            }
            var request = requestCreator.CreateRequest(searchRequest.SearchString + Configurations.OnlinerPageVariable + searchRequest.PageNumber);
            var webResponse = (HttpWebResponse)(await request.GetResponseAsync());
            var result = productParser.FromRequest(webResponse);
            return repository.CheckProducts(result.Products, userName);
        }
    }
}