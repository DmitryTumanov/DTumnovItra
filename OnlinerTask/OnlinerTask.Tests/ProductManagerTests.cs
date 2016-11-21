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
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace OnlinerTask.Tests
{
    [TestClass]
    public class ProductManagerTests
    {
        private Mock<ISearchService> searchServiceMock;
        private Mock<IRepository> repositoryMock;
        private Mock<INotification> notificationMock;

        private IManager GetProductManager()
        {
            searchServiceMock = new Mock<ISearchService>();
            repositoryMock = new Mock<IRepository>();
            notificationMock = new Mock<INotification>();
            return new ProductManager(searchServiceMock.Object, repositoryMock.Object, notificationMock.Object);
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
            notificationMock.Verify(mock => mock.AddProductFromSearch(It.IsAny<string>()), Times.Once);
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
            notificationMock.Verify(mock => mock.DeleteProductFromSearch(It.IsAny<string>()), Times.Once);
        }

        [TestCase(null)]
        [TestCase("test")]
        public void GetProducts_DifferentInput_AlwaysReturnNull(string username)
        {
            var productManager = GetProductManager();

            var result = productManager.GetProducts(username);
            
            Assert.IsNull(result);
        }
    }
}
