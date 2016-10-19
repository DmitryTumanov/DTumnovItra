using System;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OnlinerTask.Data.DataBaseModels;
using OnlinerTask.Data.EntityMappers.Interfaces;
using OnlinerTask.Data.Repository;

namespace OnlinerTask.Tests
{
    [TestClass]
    public class RepositoryTest
    {
        [TestMethod]
        public void CreateOnlinerProductTest()
        {
            var msSqlRepository = new MsSqlRepository(null);
            var b = msSqlRepository.CreateOnlinerProduct(null, null);
            Assert.AreEqual(false, b);
            Assert.IsNotNull(msSqlRepository);
        }

        [TestMethod]
        public void CreateProductTest()
        {
            var msSqlRepository = new MsSqlRepository(null);
            var b = msSqlRepository.CreateProduct(null);
            Assert.AreEqual(false, b);
            Assert.IsNotNull(msSqlRepository);
        }

        [TestMethod]
        public void CreatePriceAmmountTest()
        {
            var msSqlRepository = new MsSqlRepository(null);
            var i = msSqlRepository.CreatePriceAmmount(null);
            Assert.AreEqual(-1, i);
            Assert.IsNotNull(msSqlRepository);
        }

        [TestMethod]
        public async Task RemoveOnlinerProductTest()
        {
            var msSqlRepository = new MsSqlRepository(null);
            await msSqlRepository.RemoveOnlinerProduct(0, null);
        }

        [TestMethod]
        public void GetPersonalProductsTest()
        {
            var msSqlRepository = new MsSqlRepository(null);
            msSqlRepository.GetPersonalProducts(null);
        }

        [TestMethod]
        public void GetAllProductsTest()
        {
            var msSqlRepository = new MsSqlRepository(null);
            msSqlRepository.GetAllProducts();
        }
    }
}
