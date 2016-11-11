﻿using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NUnit.Framework;
using OnlinerTask.BLL.Services.Search;
using OnlinerTask.BLL.Services.Search.Implementations;
using OnlinerTask.BLL.Services.Search.Request;
using OnlinerTask.Data.Requests;

namespace OnlinerTask.Tests
{
    [TestClass]
    public class SearchServiceTest
    {
        private ISearchService service;
        private Mock<IRequestFactory> requestFactory;

        public ISearchService GetSearchService()
        {
            requestFactory = new Mock<IRequestFactory>();
            service = new SearchService(null, requestFactory.Object, null);
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
            await GetSearchService().GetProducts(new SearchRequest(searchString, pagenumber), username);
            requestFactory.Verify(mock=>mock.CreateRequest(It.IsAny<string>(), It.IsAny<string[]>()), Times.Never);
        }
        
        [TestCase("t", "t", 1)]
        [TestCase("test", "test", 2)]
        [TestCase("https://catalog.api.onliner.by/search/products?query=", "tumanov.97.dima@gmail.com", 20)]
        public async Task GetProducts_WithoutNulls_RequestCreated(string searchString, string username, int pagenumber)
        {
            await GetSearchService().GetProducts(new SearchRequest(searchString, pagenumber), username);
            requestFactory.Verify(mock => mock.CreateRequest(It.IsAny<string>(), It.IsAny<string[]>()), Times.Once);
        }
    }
}
