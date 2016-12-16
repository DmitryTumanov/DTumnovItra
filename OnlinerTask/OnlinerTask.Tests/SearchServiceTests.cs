using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NUnit.Framework;
using OnlinerTask.BLL.Services.Search;
using OnlinerTask.BLL.Services.Search.Implementations;
using OnlinerTask.BLL.Services.Search.ProductParser;
using OnlinerTask.BLL.Services.Search.Request;
using OnlinerTask.BLL.Services.Search.Request.RequestQueryFactory;
using OnlinerTask.Data.Repository;
using OnlinerTask.Data.Requests;
using OnlinerTask.Data.SearchModels;

namespace OnlinerTask.Tests
{
    [TestClass]
    public class SearchServiceTests
    {
        private ISearchService service;
        private Mock<IRequestFactory> requestFactory;
        private Mock<IRequestQueryFactory> queryFactory;
        private Mock<IProductRepository> productRepository;
        private Mock<IProductParser> productParser;

        public ISearchService GetSearchService()
        {
            requestFactory = new Mock<IRequestFactory>();
            queryFactory = new Mock<IRequestQueryFactory>();
            productRepository = new Mock<IProductRepository>();
            productParser = new Mock<IProductParser>();

            service = new SearchService(productRepository.Object, requestFactory.Object, productParser.Object, queryFactory.Object);
            return service;
        }

        [TestCase("", "", 0)]
        [TestCase("", "", -1)]
        [TestCase("", "", 1)]
        [TestCase(null, null, 1)]
        [TestCase("https://catalog.api.onliner.by/search/products?query=", null, 1)]
        [TestCase(null, "tumanov.97.dima@gmail.com", 1)]
        [TestCase("https://catalog.api.onliner.by/search/products?query=", "tumanov.97.dima@gmail.com", 0)]
        [TestCase("https://catalog.api.onliner.by/search/products?query=", "tumanov.97.dima@gmail.com", -1)]
        public async Task GetProducts_WithNulls_RequestIsNotCreated(string searchString, string username, int pagenumber)
        {
            var searchService = GetSearchService();

            await searchService.GetProducts(new SearchRequest(searchString, pagenumber), username);

            requestFactory.Verify(mock=>mock.CreateRequest(It.IsAny<string>(), It.IsAny<IDictionary<string, object>>()), Times.Never);
            queryFactory.Verify(mock => mock.FromRequest(It.IsAny<SearchRequest>()), Times.Never);
        }
        
        [TestCase("t", "t", 1)]
        [TestCase("test", "test", 2)]
        [TestCase("https://catalog.api.onliner.by/search/products?query=", "tumanov.97.dima@gmail.com", 20)]
        public async Task GetProducts_WithoutNulls_RequestCreated(string searchString, string username, int pagenumber)
        {
            var searchService = GetSearchService();

            await searchService.GetProducts(new SearchRequest(searchString, pagenumber), username);

            requestFactory.Verify(mock => mock.CreateRequest(It.IsAny<string>(), It.IsAny<IDictionary<string, object>>()), Times.Once);
            queryFactory.Verify(mock => mock.FromRequest(It.IsAny<SearchRequest>()), Times.Once);
        }

        [TestMethod]
        public async Task GetProducts_NullSearchRequest_ProductRequestIsNotCreated()
        {
            var searchService = GetSearchService();

            await searchService.GetProducts(null, "test");

            requestFactory.Verify(mock => mock.CreateRequest(It.IsAny<string>(), It.IsAny<IDictionary<string, object>>()), Times.Never);
            queryFactory.Verify(mock => mock.FromRequest(It.IsAny<SearchRequest>()), Times.Never);
        }
        
        [TestMethod]
        public async Task GetProducts_ValidRequestWithoutParse_RequestNotCreated()
        {
            var searchService = GetSearchService();
            requestFactory.Setup(mock => mock.CreateRequest(It.IsAny<string>(), It.IsAny<IDictionary<string, object>>()))
                .Returns(new Mock<HttpWebRequest>().Object);

            await searchService.GetProducts(new SearchRequest("test"), "test");

            productParser.Verify(mock => mock.FromResponse(It.IsAny<HttpWebResponse>()), Times.Once);
            productRepository.Verify(mock => mock.CheckProducts(It.IsAny<List<ProductModel>>(), It.IsAny<string>()), Times.Never);
        }

        [TestMethod]
        public async Task GetProducts_ValidRequestAndParse_RequestCreated()
        {
            var searchService = GetSearchService();
            requestFactory.Setup(mock => mock.CreateRequest(It.IsAny<string>(), It.IsAny<IDictionary<string, object>>()))
                .Returns(new Mock<HttpWebRequest>().Object);
            productParser.Setup(mock => mock.FromResponse(It.IsAny<HttpWebResponse>())).Returns(new SearchResult());

            await searchService.GetProducts(new SearchRequest("test"), "test");

            productParser.Verify(mock => mock.FromResponse(It.IsAny<HttpWebResponse>()), Times.Once);
            productRepository.Verify(mock => mock.CheckProducts(It.IsAny<List<ProductModel>>(), It.IsAny<string>()), Times.Once);
        }
    }
}
