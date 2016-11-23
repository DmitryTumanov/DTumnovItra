using System.Linq;
using System.Threading.Tasks;
using OnlinerTask.BLL.Services.Search;
using OnlinerTask.Data.DataBaseModels;
using OnlinerTask.Data.Requests;
using OnlinerTask.Data.SearchModels;

namespace OnlinerTask.BLL.Services.Job.ProductJob.ProductUpdate.Implementations
{
    public class OnlinerProductUpdater : IProductUpdater
    {
        private readonly ISearchService searchService;

        public OnlinerProductUpdater(ISearchService searchService)
        {
            this.searchService = searchService;
        }

        public async Task<ProductModel> GetUpdate(Product databaseModel)
        {
            if (databaseModel == null)
            {
                return null;
            }
            var product = (await searchService.GetProducts(new SearchRequest(databaseModel.ProductKey), databaseModel.UserEmail))?.FirstOrDefault();
            return Check(databaseModel, product) ? product : null;
        }

        private static bool Check(Product databaseModel, ProductModel onlinerModel)
        {
            if (onlinerModel == null || IsModelsPricesNull(databaseModel, onlinerModel))
            {
                return false;
            }
            var isMaxAmountUpdate = onlinerModel.Prices?.PriceMax?.Amount != databaseModel.Price?.PriceMaxAmmount?.Amount;
            var isMinAmountUpdate = onlinerModel.Prices?.PriceMin?.Amount != databaseModel.Price?.PriceMinAmmount?.Amount;
            return isMaxAmountUpdate || isMinAmountUpdate;
        }

        private static bool IsModelsPricesNull(Product databaseModel, ProductModel onlinerModel)
        {
            return databaseModel.Price == null && onlinerModel.Prices == null;
        }
    }
}
