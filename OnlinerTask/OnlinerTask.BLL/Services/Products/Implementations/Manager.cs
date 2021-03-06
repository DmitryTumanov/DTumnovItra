﻿using System;
using System.Linq;
using System.Threading.Tasks;
using Ninject;
using OnlinerTask.BLL.Services.Logger;
using OnlinerTask.BLL.Services.Search;
using OnlinerTask.Data.ElasticSearch.LoggerModels;
using OnlinerTask.Data.Repository;
using OnlinerTask.Data.Requests;
using OnlinerTask.Data.Responses;

namespace OnlinerTask.BLL.Services.Products.Implementations
{
    public abstract class Manager : IManager
    {
        private readonly ISearchService searchService;
        private readonly IRepository repository;

        [Inject]
        public ILogger Logger { get; set; }

        protected Manager(ISearchService searchService, IRepository repository)
        {
            this.searchService = searchService;
            this.repository = repository;
        }

        public async Task AddProduct(PutRequest request, string name)
        {
            var result = (await searchService.GetProducts(request, name))?.FirstOrDefault();
            if (result == null)
            {
                return;
            }
            repository.CreateOnlinerProduct(result, name);
            AddNotify(result.FullName);
            Logger?.LogObject(new AddedProductModel(result, DateTime.Now));
        }

        public async Task RemoveProduct(DeleteRequest request, string name)
        {
            var product = await repository.RemoveOnlinerProduct(request.ItemId, name);
            if (product == null)
            {
                return;
            }
            RemoveNotify(product.FullName);
            Logger?.LogObject(new RemovedProduct(product, DateTime.Now));
        }
        public virtual Task<PersonalPageResponse> GetProducts(string name)
        {
            return null;
        }

        public abstract void AddNotify(string name);
        public abstract void RemoveNotify(string name);
    }
}
