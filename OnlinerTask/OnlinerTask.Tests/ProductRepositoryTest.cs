using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OnlinerTask.Data.Repository;
using OnlinerTask.Data.Repository.Implementations;

namespace OnlinerTask.Tests
{
    [TestClass]
    public class ProductRepositoryTest
    {
        [TestMethod]
        public void CheckProductsTest()
        {
            var msSqlProductRepository = new MsSqlProductRepository(null);
            var task = msSqlProductRepository.CheckProducts(null, null);
            task.Dispose();
            Assert.IsNotNull(task);
            Assert.AreEqual(TaskStatus.Faulted, task.Status);
            Assert.AreEqual(false, task.IsCanceled);
            Assert.IsNull(task.AsyncState);
            Assert.AreEqual(true, task.IsFaulted);
            Assert.IsNotNull(msSqlProductRepository);
        }

        [TestMethod]
        public async Task CheckItemTest()
        {
            var msSqlProductRepository = new MsSqlProductRepository(null);
            await msSqlProductRepository.CheckItem(0, null);
        }

        [TestMethod]
        public void GetAllProductsTest()
        {
            var msSqlRepository = new MsSqlRepository(null);
            var msSqlProductRepository = new MsSqlProductRepository(msSqlRepository);
            msSqlProductRepository.GetAllProducts();
        }

        [TestMethod]
        public void GetPersonalProductsTest()
        {
            var msSqlRepository = new MsSqlRepository(null);
            var msSqlProductRepository = new MsSqlProductRepository(msSqlRepository);
            msSqlProductRepository.GetPersonalProducts(null);
        }

        [TestMethod]
        public async Task RemoveOnlinerProductTest()
        {
            var msSqlRepository = new MsSqlRepository(null);
            var msSqlProductRepository = new MsSqlProductRepository(msSqlRepository);
            await msSqlProductRepository.RemoveOnlinerProduct(0, null);
        }

        [TestMethod]
        public void CreateOnlinerProductTest()
        {
            var msSqlRepository = new MsSqlRepository(null);
            var msSqlProductRepository = new MsSqlProductRepository(msSqlRepository);
            var b = msSqlProductRepository.CreateOnlinerProduct(null, null);
            Assert.AreEqual(false, b);
            Assert.IsNotNull(msSqlProductRepository);
        }
    }
}
