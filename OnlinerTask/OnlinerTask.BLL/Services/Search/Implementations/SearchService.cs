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
        private readonly IRequestFactory requestFactory;
        private readonly IProductParser productParser;

        public SearchService(IProductRepository repository, IRequestFactory requestFactory, IProductParser productParser)
        {
            this.repository = repository;
            this.requestFactory = requestFactory;
            this.productParser = productParser;
        }

        public async Task<List<ProductModel>> GetProducts(SearchRequest searchRequest, string userName)
        {
            if (string.IsNullOrEmpty(userName) || string.IsNullOrEmpty(searchRequest?.SearchString) || searchRequest.PageNumber < 1)
            {
                return null;
            }
            var request = requestFactory.CreateRequest(Configurations.OnlinerApiPath,
                searchRequest.SearchString, Configurations.OnlinerPageVariable, searchRequest.PageNumber.ToString());
            if (request == null)
            {
                return null;
            }
            return await GetProductsFromRequest(request, userName);
        }

        private async Task<List<ProductModel>> GetProductsFromRequest(WebRequest request, string userName)
        {
            var webResponse = (HttpWebResponse)(await request.GetResponseAsync());
            var result = productParser.FromRequest(webResponse);
            return repository.CheckProducts(result.Products, userName);
        }
    }
}