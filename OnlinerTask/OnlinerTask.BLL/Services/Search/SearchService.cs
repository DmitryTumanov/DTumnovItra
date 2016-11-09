using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using OnlinerTask.BLL.Services.Search.ProductParser;
using OnlinerTask.BLL.Services.Search.Request;
using OnlinerTask.Data.Repository;
using OnlinerTask.Data.Requests;
using OnlinerTask.Data.SearchModels;

namespace OnlinerTask.BLL.Services.Search
{
    public class SearchService : ISearchService
    {
        private readonly IProductRepository repository;
        private readonly IRequestCreator requestCreator;
        private readonly IProductParser productParser;

        public SearchService(IProductRepository repository, IRequestCreator requestCreator, IProductParser productParser)
        {
            this.repository = repository;
            this.requestCreator = requestCreator;
            this.productParser = productParser;
        }

        public async Task<List<ProductModel>> GetProducts(SearchRequest searchRequest, string userName)
        {
            if (string.IsNullOrEmpty(searchRequest?.SearchString))
            {
                return null;
            }
            var request = requestCreator.CreateRequest(searchRequest.SearchString);
            var webResponse = (HttpWebResponse)(await request.GetResponseAsync());
            var result = productParser.ParseProductsFromRequest(webResponse);
            return repository.CheckProducts(result.Products, userName);
        }
    }
}