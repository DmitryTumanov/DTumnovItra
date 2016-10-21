using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OnlinerTask.Data.Repository;
using OnlinerTask.Data.Repository.Implementations;

namespace OnlinerTask.Tests
{
    [TestClass]
    public class TimeRepositoryTest
    {
        [TestMethod]
        public void UpdateProductTest()
        {
            var msSqlTimeServiceRepository = new MsSqlTimeServiceRepository
                (null, null);
            var b = msSqlTimeServiceRepository.UpdateProduct
                (null, null);
            Assert.AreEqual(false, b);
            Assert.IsNotNull(msSqlTimeServiceRepository);
            Assert.IsNull(msSqlTimeServiceRepository.repository);
        }

        [TestMethod]
        public void WriteUpdateToProductTest()
        {
            var msSqlTimeServiceRepository = new MsSqlTimeServiceRepository
                (null, null);
            var usersUpdateEmail = msSqlTimeServiceRepository.WriteUpdateToProduct
                (null, default(TimeSpan));
            Assert.IsNull(usersUpdateEmail);
            Assert.IsNotNull(msSqlTimeServiceRepository);
            Assert.IsNull(
                msSqlTimeServiceRepository.repository);
        }

        [TestMethod]
        public void WriteUpdateTest()
        {
            var msSqlTimeServiceRepository = new MsSqlTimeServiceRepository(null, null);
            msSqlTimeServiceRepository.WriteUpdate(null, null);
        }

        [TestMethod]
        public void GetAllProductsTest()
        {
            var msSqlRepository = new MsSqlRepository(null);
            var msSqlTimeServiceRepository = new MsSqlTimeServiceRepository
                (msSqlRepository, null);
            msSqlTimeServiceRepository.GetAllProducts();
        }
    }
}
