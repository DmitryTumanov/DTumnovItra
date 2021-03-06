﻿using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using OnlinerTask.BLL.Services.Job.ProductJob;
using OnlinerTask.BLL.Services.Job.ProductJob.Implementations;
using OnlinerTask.BLL.Services.Job.ProductJob.ProductUpdate;
using OnlinerTask.Data.DataBaseModels;
using OnlinerTask.Data.MqConstituents;
using OnlinerTask.Data.Repository;
using OnlinerTask.Data.ScheduleModels;
using OnlinerTask.Data.SearchModels;
using ServiceStack.Messaging;

namespace OnlinerTask.Tests
{
    [TestClass]
    public class ProductJobTests
    {
        private Mock<IProductRepository> productRepositoryMock;
        private Mock<IRepository> repositoryMock;
        private Mock<IProductUpdater> productUpdaterMock;
        private Mock<IMqConstituentsFactory> constituentsFactoryMock;

        private readonly List<Product> productsList = new List<Product>()
        {
            new Product(),
            new Product(),
            new Product()
        };

        private IProductJob GetProductJob()
        {
            productRepositoryMock = new Mock<IProductRepository>();
            repositoryMock = new Mock<IRepository>();
            productUpdaterMock = new Mock<IProductUpdater>();
            constituentsFactoryMock = new Mock<IMqConstituentsFactory>();
            
            repositoryMock.Setup(mock => mock.GetAllProducts()).Returns(productsList);
            productUpdaterMock.Setup(mock => mock.GetUpdate(It.IsAny<Product>())).ReturnsAsync(new ProductModel());

            return new ProductJobService(productRepositoryMock.Object, productUpdaterMock.Object, repositoryMock.Object, constituentsFactoryMock.Object);
        }

        [TestMethod]
        public void Execute_NothingSend_ConstituentServiceCalled()
        {
            var service = GetProductJob();

            service.Execute();

            constituentsFactoryMock.Verify(mock=>mock.CreateAppHost(), Times.Once);
            constituentsFactoryMock.Verify(mock => mock.CreateClient(), Times.Once);
        }

        [TestMethod]
        public async Task GetAndPublishUpdates_SendNull_UpdateServiceSeveralTimesCalled()
        {
            var service = GetProductJob();

            await service.GetAndPublishUpdates(null);
            var productCount = repositoryMock.Object.GetAllProducts().Count;

            productUpdaterMock.Verify(mock => mock.GetUpdate(It.IsAny<Product>()), Times.Exactly(productCount));
        }

        [TestMethod]
        public async Task GetAndPublishUpdates_GetUpdateReturnObject_ProductRepositorySeveralTimesCalled()
        {
            var service = GetProductJob();

            await service.GetAndPublishUpdates(null);
            var productCount = repositoryMock.Object.GetAllProducts().Count;

            productRepositoryMock.Verify(mock => mock.WriteUpdate(It.IsAny<ProductModel>(), It.IsAny<string>()), Times.Exactly(productCount));
        }

        [TestMethod]
        public async Task GetAndPublishUpdates_GetUpdateReturnNull_ProductRepositoryNeverCalled()
        {
            var service = GetProductJob();
            productUpdaterMock.Setup(mock => mock.GetUpdate(It.IsAny<Product>())).ReturnsAsync((ProductModel)null);

            await service.GetAndPublishUpdates(null);

            productRepositoryMock.Verify(mock => mock.WriteUpdate(It.IsAny<ProductModel>(), It.IsAny<string>()), Times.Never);
        }

        [TestMethod]
        public async Task GetAndPublishUpdates_SendClient_ClientPublishMessage()
        {
            var service = GetProductJob();
            var client = new Mock<IMessageQueueClient>();

            await service.GetAndPublishUpdates(client.Object);
            var productCount = repositoryMock.Object.GetAllProducts().Count;

            client.Verify(mock => mock.Publish(It.IsAny<UsersUpdateEmail>()), Times.Exactly(productCount));
        }

        [TestMethod]
        public async Task GetAndPublishUpdates_GetUpdateReturnNull_ClientDoNothing()
        {
            var service = GetProductJob();
            var client = new Mock<IMessageQueueClient>();
            productUpdaterMock.Setup(mock => mock.GetUpdate(It.IsAny<Product>())).ReturnsAsync((ProductModel)null);

            await service.GetAndPublishUpdates(client.Object);

            client.Verify(mock => mock.Publish(It.IsAny<UsersUpdateEmail>()), Times.Never);
        }
    }
}
