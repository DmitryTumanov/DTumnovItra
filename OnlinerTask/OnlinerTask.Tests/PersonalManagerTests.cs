using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NUnit.Framework;
using OnlinerTask.BLL.Services.Logger;
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
        private Mock<ILogger> loggerMock;

        private IManager GetProductManager()
        {
            searchServiceMock = new Mock<ISearchService>();
            repositoryMock = new Mock<IRepository>();
            notificationMock = new Mock<INotification>();
            personalRepositoryMock = new Mock<IPersonalRepository>();
            loggerMock = new Mock<ILogger>();

            var personalManager = new PersonalManager(searchServiceMock.Object, personalRepositoryMock.Object,
                repositoryMock.Object, notificationMock.Object) {Logger = loggerMock.Object};
            return personalManager;
        }

        private void ValidSearchServiceSetup()
        {
            searchServiceMock.Setup(mock => mock.GetProducts(It.IsAny<SearchRequest>(), It.IsAny<string>()))
                .ReturnsAsync(new List<ProductModel> { new ProductModel() { FullName = "test" } });
        }

        private void NotValidSearchServiceSetup()
        {
            searchServiceMock.Setup(mock => mock.GetProducts(It.IsAny<SearchRequest>(), It.IsAny<string>()))
                .ReturnsAsync((List<ProductModel>)null);
        }

        private void NotValidRepositorySetup()
        {
            repositoryMock.Setup(x => x.RemoveOnlinerProduct(It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync((Product)null);
        }

        private void ValidRepositorySetup()
        {
            repositoryMock.Setup(x => x.RemoveOnlinerProduct(It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync(new Product());
        }

        [TestMethod]
        public async Task AddProduct_UsualScenario_SearchServiceCalled()
        {
            var productManager = GetProductManager();
            var request = new PutRequest("test");

            await productManager.AddProduct(request, "test");

            searchServiceMock.Verify(mock => mock.GetProducts(request, It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        public async Task RemoveProduct_UsualScenario_RepositoryCalled()
        {
            var productManager = GetProductManager();
            var request = new DeleteRequest(1);

            await productManager.RemoveProduct(request, "test");

            repositoryMock.Verify(mock => mock.RemoveOnlinerProduct(It.IsAny<int>(), It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        public async Task AddProduct_SearchReturnsNull_RepositoryNotCalled()
        {
            var productManager = GetProductManager();
            var request = new PutRequest("");
            NotValidSearchServiceSetup();

            await productManager.AddProduct(request, "");

            repositoryMock.Verify(mock => mock.CreateOnlinerProduct(It.IsAny<ProductModel>(), It.IsAny<string>()), Times.Never);
        }

        [TestMethod]
        public async Task AddProduct_SearchReturnsValid_RepositoryCalled()
        {
            var productManager = GetProductManager();
            var request = new PutRequest("test");
            ValidSearchServiceSetup();

            await productManager.AddProduct(request, "test");

            repositoryMock.Verify(mock => mock.CreateOnlinerProduct(It.IsAny<ProductModel>(), It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        public async Task AddProduct_SearchReturnsNull_LoggerNotCalled()
        {
            var productManager = GetProductManager();
            var request = new PutRequest("test");
            NotValidSearchServiceSetup();

            await productManager.AddProduct(request, "");

            loggerMock.Verify(mock => mock.LogObject(It.IsAny<object>()), Times.Never);
        }

        [TestMethod]
        public async Task AddProduct_SearchReturnsValid_LoggerCalled()
        {
            var productManager = GetProductManager();
            var request = new PutRequest("test");
            ValidSearchServiceSetup();

            await productManager.AddProduct(request, "test");

            loggerMock.Verify(mock => mock.LogObject(It.IsAny<object>()), Times.Once);
        }

        [TestMethod]
        public async Task RemoveProduct_NotSuccessRemove_LoggerNotCalled()
        {
            var productManager = GetProductManager();
            var request = new DeleteRequest(1);
            NotValidRepositorySetup();

            await productManager.RemoveProduct(request, "test");

            loggerMock.Verify(mock => mock.LogObject(It.IsAny<object>()), Times.Never);
        }

        [TestMethod]
        public async Task RemoveProduct_SuccessRemove_LoggerCalled()
        {
            var productManager = GetProductManager();
            var request = new DeleteRequest(1);
            ValidRepositorySetup();

            await productManager.RemoveProduct(request, "test");

            loggerMock.Verify(mock => mock.LogObject(It.IsAny<object>()), Times.Once);
        }

        [TestMethod]
        public async Task AddProduct_SearchReturnsNull_NotificationNotCalled()
        {
            var productManager = GetProductManager();
            var request = new PutRequest("test");
            NotValidSearchServiceSetup();

            await productManager.AddProduct(request, "");

            notificationMock.Verify(mock => mock.AddProduct(It.IsAny<string>()), Times.Never);
        }

        [TestMethod]
        public async Task AddProduct_SearchReturnsValid_NotificationCalled()
        {
            var productManager = GetProductManager();
            var request = new PutRequest("test");
            ValidSearchServiceSetup();

            await productManager.AddProduct(request, "test");

            notificationMock.Verify(mock => mock.AddProduct(It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        public async Task RemoveProduct_NotSuccessRemove_NotificationNotCalled()
        {
            var productManager = GetProductManager();
            var request = new DeleteRequest(1);
            NotValidRepositorySetup();

            await productManager.RemoveProduct(request, "test");

            notificationMock.Verify(mock => mock.AddProduct(It.IsAny<string>()), Times.Never);
        }

        [TestMethod]
        public async Task RemoveProduct_SuccessRemove_NotificationCalled()
        {
            var productManager = GetProductManager();
            var request = new DeleteRequest(1);
            ValidRepositorySetup();

            await productManager.RemoveProduct(request, "test");

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
    }
}
