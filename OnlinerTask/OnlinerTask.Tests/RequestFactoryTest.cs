using System.Linq;
using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OnlinerTask.BLL.Services.Search.ProductParser.Implementations;
using OnlinerTask.BLL.Services.Search.Request.Implementations;

namespace OnlinerTask.Tests
{
    [TestClass]
    public class RequestFactoryTest
    {
        private readonly string searchString = "Apple";
        private readonly string onlinerApiPath = "https://catalog.api.onliner.by/search/products?query=";
        private readonly string onlinerPageVariable = "&page=";

        [TestMethod]
        public void OnlinerRequestArgumentsTest()
        {
            var factory = new OnlinerRequestFactory();
            var parser = new OnlinerProductParser();
            var firstRequest = factory.CreateRequest(onlinerApiPath, searchString);
            var secondRequest = factory.CreateRequest(onlinerApiPath, 
                searchString, onlinerPageVariable, 1.ToString());
            Assert.IsNotNull(firstRequest);
            Assert.IsNotNull(secondRequest);
            var firstResponse = parser.FromRequest((HttpWebResponse)firstRequest.GetResponse());
            var secondResponse = parser.FromRequest((HttpWebResponse)secondRequest.GetResponse());
            Assert.IsNotNull(firstResponse);
            Assert.IsNotNull(secondResponse);
            Assert.AreEqual(firstResponse.Products.Count(), secondResponse.Products.Count());
            Assert.AreEqual(firstResponse.Products.ElementAt(0).Id, secondResponse.Products.ElementAt(0).Id);
        }

        [TestMethod]
        public void OnlinerRequestNullTest()
        {
            var factory = new OnlinerRequestFactory();
            var firstRequest = factory.CreateRequest(null);
            var secondRequest = factory.CreateRequest(null, null);
            var thirdRequest = factory.CreateRequest(null, null, null);
            var forthRequest = factory.CreateRequest(onlinerApiPath);
            var fifthRequest = factory.CreateRequest(onlinerApiPath, null);
            var sixthRequest = factory.CreateRequest(onlinerApiPath, null, null);
            Assert.IsNull(firstRequest);
            Assert.IsNull(secondRequest);
            Assert.IsNull(thirdRequest);
            Assert.IsNotNull(forthRequest);
            Assert.IsNotNull(fifthRequest);
            Assert.IsNotNull(sixthRequest);
            Assert.AreEqual(forthRequest.Address.AbsoluteUri, fifthRequest.Address.AbsoluteUri);
            Assert.AreEqual(forthRequest.Address.AbsoluteUri, sixthRequest.Address.AbsoluteUri);
        }

        [TestMethod]
        public void OnlinerRequestDifferentPagesTest()
        {
            var factory = new OnlinerRequestFactory();
            var parser = new OnlinerProductParser();
            var firstRequest = factory.CreateRequest(onlinerApiPath,
                searchString, onlinerPageVariable, 1.ToString());
            var secondRequest = factory.CreateRequest(onlinerApiPath,
                searchString, onlinerPageVariable, 2.ToString());
            Assert.IsNotNull(firstRequest);
            Assert.IsNotNull(secondRequest);
            var firstResponse = parser.FromRequest((HttpWebResponse)firstRequest.GetResponse());
            var secondResponse = parser.FromRequest((HttpWebResponse)secondRequest.GetResponse());
            Assert.IsNotNull(firstResponse);
            Assert.IsNotNull(secondResponse);
            Assert.AreEqual(firstResponse.Products.Count(), secondResponse.Products.Count());
            Assert.AreNotEqual(firstResponse.Products.ElementAt(0).Id, secondResponse.Products.ElementAt(0).Id);
        }
    }
}
