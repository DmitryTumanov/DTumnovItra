using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NUnit.Framework;
using OnlinerTask.BLL.Services.Notification;
using OnlinerTask.BLL.Services.Products;
using OnlinerTask.BLL.Services.Products.Implementations;
using OnlinerTask.BLL.Services.Search;
using OnlinerTask.Data.DataBaseModels;
using OnlinerTask.Data.Repository;
using OnlinerTask.Data.Requests;
using OnlinerTask.Data.SearchModels;

namespace OnlinerTask.Tests
{
    [TestClass]
    public class PersonalManagerTests
    {
        private Mock<ISearchService> searchServiceMock;
        private Mock<IRepository> repositoryMock;
        private Mock<INotification> notificationMock;
        private Mock<IPersonalRepository> personalRepositoryMock;

        private IManager GetProductManager()
        {
            searchServiceMock = new Mock<ISearchService>();
            repositoryMock = new Mock<IRepository>();
            notificationMock = new Mock<INotification>();
            personalRepositoryMock = new Mock<IPersonalRepository>();

            searchServiceMock.Setup(mock => mock.GetProducts(It.IsAny<SearchRequest>(), It.IsAny<string>()))
                .ReturnsAsync(new List<ProductModel> { new ProductModel() { FullName = "test" } });

            return new PersonalManager(searchServiceMock.Object, personalRepositoryMock.Object, repositoryMock.Object, notificationMock.Object);
        }

        [TestCase(null, null)]
        [TestCase("test", null)]
        [TestCase(null, "test")]
        [TestCase("test", "test")]
        public async Task AddProduct_DifferentInput_ServicesCalled(string searchString, string username)
        {
            var productManager = GetProductManager();
            var request = new PutRequest(searchString);

            await productManager.AddProduct(request, username);

            searchServiceMock.Verify(mock => mock.GetProducts(request, username), Times.Once);
            repositoryMock.Verify(mock => mock.CreateOnlinerProduct(It.IsAny<ProductModel>(), username), Times.Once);
            notificationMock.Verify(mock => mock.AddProduct(It.IsAny<string>()), Times.Once);
        }

        [TestCase(null, null)]
        [TestCase(1, null)]
        [TestCase(null, "test")]
        [TestCase(1, "test")]
        public async Task RemoveProduct_DifferentInput_NotAllServicesCalled(int itemId, string username)
        {
            var productManager = GetProductManager();
            var request = new DeleteRequest(itemId);

            await productManager.RemoveProduct(request, username);

            repositoryMock.Verify(mock => mock.RemoveOnlinerProduct(itemId, username), Times.Once);
            notificationMock.Verify(mock => mock.DeleteProduct(It.IsAny<string>()), Times.Never);
        }

        [TestCase(null, null)]
        [TestCase(1, null)]
        [TestCase(null, "test")]
        [TestCase(1, "test")]
        public async Task RemoveProduct_DifferentInputWithValidReturn_ServicesCalled(int itemId, string username)
        {
            var productManager = GetProductManager();
            var request = new DeleteRequest(itemId);
            repositoryMock.Setup(x => x.RemoveOnlinerProduct(itemId, username)).ReturnsAsync(new Product());

            await productManager.RemoveProduct(request, username);

            repositoryMock.Verify(mock => mock.RemoveOnlinerProduct(itemId, username), Times.Once);
            notificationMock.Verify(mock => mock.DeleteProduct(It.IsAny<string>()), Times.Once);
        }

        [TestCase(null)]
        [TestCase("test")]
        public async Task GetProducts_DifferentInput_PageResponceReturned(string username)
        {
            var productManager = GetProductManager();

            await productManager.GetProducts(username);

            personalRepositoryMock.Verify(mock=>mock.PersonalProductsResponse(username), Times.Once);
        }

        [TestMethod]
        public async Task AddProduct_SearchReturnsNull_ServicesNotCalled()
        {
            var productManager = GetProductManager();
            var request = new PutRequest("");
            searchServiceMock.Setup(mock => mock.GetProducts(It.IsAny<SearchRequest>(), It.IsAny<string>()))
                .ReturnsAsync((List<ProductModel>)null);

            await productManager.AddProduct(request, "");

            searchServiceMock.Verify(mock => mock.GetProducts(It.IsAny<PutRequest>(), It.IsAny<string>()), Times.Once);
            repositoryMock.Verify(mock => mock.CreateOnlinerProduct(It.IsAny<ProductModel>(), It.IsAny<string>()), Times.Never);
            notificationMock.Verify(mock => mock.AddProduct(It.IsAny<string>()), Times.Never);
        }
    }
}
