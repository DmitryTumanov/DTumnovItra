using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OnlinerTask.Data.Repository;
using OnlinerTask.Data.Repository.Implementations;

namespace OnlinerTask.Tests
{
    [TestClass]
    public class PersonalRepositoryTest
    {
        [TestMethod]
        public async Task ChangeSendEmailTimeAsyncTest()
        {
            var msSqlPersonalRepository = new MsSqlPersonalRepository(null, null);
            await msSqlPersonalRepository.ChangeSendEmailTimeAsync(null, null);
        }

        [TestMethod]
        public void CreateOnlinerProductTest()
        {
            var msSqlRepository = new MsSqlRepository(null);
            var msSqlPersonalRepository = new MsSqlPersonalRepository(null, msSqlRepository);
            var b = msSqlPersonalRepository.CreateOnlinerProduct(null, null);
            Assert.AreEqual(false, b);
            Assert.IsNotNull(msSqlPersonalRepository);
        }

        [TestMethod]
        public void PersonalProductsResponseTest()
        {
            var msSqlRepository = new MsSqlRepository(null);
            var msSqlPersonalRepository = new MsSqlPersonalRepository(null, msSqlRepository);
            msSqlPersonalRepository.PersonalProductsResponse(null);
        }

        [TestMethod]
        public async Task RemoveOnlinerProduct278()
        {
            var msSqlRepository = new MsSqlRepository(null);
            var msSqlPersonalRepository = new MsSqlPersonalRepository(null, msSqlRepository);
            await msSqlPersonalRepository.RemoveOnlinerProduct(0, null);
        }
    }
}
