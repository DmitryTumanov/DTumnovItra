﻿using System.Linq;
using System.Threading.Tasks;
using OnlinerTask.BLL.Services.ElasticSearch.ProductLogger;
using OnlinerTask.BLL.Services.Search;
using OnlinerTask.Data.Repository;
using OnlinerTask.Data.Requests;
using OnlinerTask.Data.Responses;

namespace OnlinerTask.BLL.Services.Products.Implementations
{
    public abstract class Manager : IManager
    {
        private readonly ISearchService searchService;
        private readonly IRepository repository;
        private readonly IProductLogger productLogger;

        protected Manager(ISearchService searchService, IRepository repository, IProductLogger productLogger)
        {
            this.searchService = searchService;
            this.repository = repository;
            this.productLogger = productLogger;
        }

        public async Task AddProduct(PutRequest request, string name)
        {
            var result = (await searchService.GetProducts(request, name))?.FirstOrDefault();
            if (result == null)
            {
                return;
            }
            var productId = repository.CreateOnlinerProduct(result, name);
            AddNotify(result.FullName);
            await productLogger.LogAdding(productId, result);
        }

        public async Task RemoveProduct(DeleteRequest request, string name)
        {
            var product = await repository.RemoveOnlinerProduct(request.ItemId, name);
            RemoveNotify(product.FullName);
            await productLogger.RemoveLog(product.Id);
        }
        public virtual Task<PersonalPageResponse> GetProducts(string name)
        {
            return null;
        }

        public abstract void AddNotify(string name);
        public abstract void RemoveNotify(string name);
    }
}
