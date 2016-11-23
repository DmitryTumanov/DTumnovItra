using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using NUnit.Framework;
using OnlinerTask.BLL.Services.Job.ProductJob.ProductUpdate;
using OnlinerTask.BLL.Services.Job.ProductJob.ProductUpdate.Implementations;
using OnlinerTask.BLL.Services.Search;
using OnlinerTask.Data.DataBaseModels;
using OnlinerTask.Data.Requests;
using OnlinerTask.Data.SearchModels;
using Assert = Microsoft.VisualStudio.TestTools.UnitTesting.Assert;

namespace OnlinerTask.Tests
{
    [TestClass]
    public class ProductUpdateTests
    {
        private Mock<ISearchService> searchServiceMock;

        private static readonly object[] sameObjects =
        {
            new object[] {
                new Product()
                {
                    Price = new Price()
                    {
                        PriceMinAmmount = new PriceAmmount()
                        {
                            Amount = 10
                        },
                        PriceMaxAmmount = new PriceAmmount()
                        {
                            Amount = 20
                        }
                    }
                },
                new ProductModel()
                {
                    Prices = new PriceModel()
                    {
                        PriceMin = new PriceAmmountModel()
                        {
                            Amount = 10
                        },
                        PriceMax = new PriceAmmountModel()
                        {
                            Amount = 20
                        }
                    }
                }
            }
        };

        private IProductUpdater GetProductUpdater()
        {
            searchServiceMock = new Mock<ISearchService>();

            searchServiceMock.Setup(mock => mock.GetProducts(It.IsAny<SearchRequest>(), It.IsAny<string>()))
                .ReturnsAsync((List<ProductModel>)null);

            return new OnlinerProductUpdater(searchServiceMock.Object);
        }

        [TestMethod]
        public async Task GetUpdate_SendNull_ReturnNull()
        {
            var updater = GetProductUpdater();

            var result = await updater.GetUpdate(null);

            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task GetUpdate_GetProductsReturnNull_ReturnNull()
        {
            var updater = GetProductUpdater();

            var result = await updater.GetUpdate(new Product());

            Assert.IsNull(result);
        }

        [TestCaseSource(nameof(sameObjects))]
        public async Task GetUpdate_PriceChange_ReturnProductModel(Product product, ProductModel productModel)
        {
            var updater = GetProductUpdater();
            productModel.Prices.PriceMax.Amount += 1;
            productModel.Prices.PriceMin.Amount += 1;
            product.Price.PriceMaxAmmount.Amount -= 1;
            product.Price.PriceMinAmmount.Amount -= 1;
            searchServiceMock.Setup(mock => mock.GetProducts(It.IsAny<SearchRequest>(), It.IsAny<string>()))
                .ReturnsAsync(new List<ProductModel> { productModel });

            var result = await updater.GetUpdate(product);

            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task GetUpdate_PriceAppeared_ReturnProductModel()
        {
            var updater = GetProductUpdater();
            searchServiceMock.Setup(mock => mock.GetProducts(It.IsAny<SearchRequest>(), It.IsAny<string>()))
                .ReturnsAsync(new List<ProductModel> { new ProductModel() {Prices = new PriceModel()} });

            var result = await updater.GetUpdate(new Product() {Price = null});

            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task GetUpdate_PriceDisAppeared_ReturnProductModel()
        {
            var updater = GetProductUpdater();
            searchServiceMock.Setup(mock => mock.GetProducts(It.IsAny<SearchRequest>(), It.IsAny<string>()))
                .ReturnsAsync(new List<ProductModel> { new ProductModel() { Prices = null } });

            var result = await updater.GetUpdate(new Product() { Price = new Price() });

            Assert.IsNull(result);
        }
    }
}
