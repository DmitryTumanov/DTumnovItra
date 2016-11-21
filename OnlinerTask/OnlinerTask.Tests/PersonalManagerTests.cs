using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NUnit.Framework;
using OnlinerTask.BLL.Services.Notification;
using OnlinerTask.BLL.Services.Products;
using OnlinerTask.BLL.Services.Products.Implementations;
using OnlinerTask.BLL.Services.Search;
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
        public async Task RemoveProduct_DifferentInput_ServicesCalled(int itemId, string username)
        {
            var productManager = GetProductManager();
            var request = new DeleteRequest(itemId);

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
    }
}
