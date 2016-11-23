using System.Net;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OnlinerTask.BLL.Services.Search.ProductParser;
using OnlinerTask.BLL.Services.Search.ProductParser.Implementations;

namespace OnlinerTask.Tests
{
    [TestClass]
    public class ProductParserTests
    {
        private IProductParser GetProductParser()
        {
            return new OnlinerProductParser();
        }

        [TestMethod]
        public void FromResponse_WithMock_NullStreamCreated()
        {
            var parser = GetProductParser();
            var responseMock = new Mock<HttpWebResponse>();

            parser.FromResponse(responseMock.Object);

            responseMock.Verify(mock=>mock.GetResponseStream(), Times.Once);
        }
    }
}
