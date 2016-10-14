﻿using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using OnlinerTask.Data.SearchModels;
using OnlinerTask.BLL.Services;
using System.Linq;
using System.Net.Http;
using OnlinerTask.Data.Requests;
using OnlinerTask.Data.Repository.Interfaces;

namespace OnlinerTask.WEB.Controllers
{
    [Authorize]
    public class ProductController : ApiController
    {
        private readonly ISearchService _searchService;
        private readonly IProductRepository _repository;

        public ProductController(ISearchService service, IProductRepository repo)
        {
            _searchService = service;
            _repository = repo;
        }

        public HttpResponseMessage Get()
        {
            return Request.CreateResponse(HttpStatusCode.OK);
        }
        
        public async Task<List<ProductModel>> Post(SearchRequest request)
        {
            return await _searchService.GetProducts(request, User.Identity.Name);
        }

        public async Task Put(PutRequest request, string testname = null)
        {
            var result = (await Post(request)).FirstOrDefault();
            _repository.CreateOnlinerProduct(result, testname ?? User.Identity.Name);
        }
        
        public async Task Delete(DeleteRequest request, string testname = null)
        {
            await _repository.RemoveOnlinerProduct(request.ItemId, testname ?? User.Identity.Name);
        }
    }
}
