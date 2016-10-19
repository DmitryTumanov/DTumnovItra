using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OnlinerTask.BLL.Services.Job;
using OnlinerTask.BLL.Services.Search;
using OnlinerTask.Data.DataBaseModels;
using OnlinerTask.Data.EntityMappers;
using OnlinerTask.Data.IdentityModels;
using OnlinerTask.Data.RedisManager;
using OnlinerTask.Data.Repository;
using OnlinerTask.Data.Requests;
using OnlinerTask.Data.ScheduleModels;
using ServiceStack.Redis;

namespace OnlinerTask.Tests
{
    [TestClass]
    public class ServicesTest
    {
        private class TestClass
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public string Surname { get; set; }
        }

        [TestMethod]
        public async Task ProductJobTest()
        {
            using (var db = new OnlinerProducts())
            {
                var firstitem = db.Product.First();
                firstitem.Price.PriceMaxAmmount.Amount += 1;
                db.SaveChanges();
                await GetProductJob().Execute();
                var redisItems = GetEmailCacheManager().GetAll<UsersUpdateEmail>();
                var newelem =
                    redisItems.FirstOrDefault(
                        x => x.ProductName == firstitem.FullName && x.UserEmail == firstitem.UserEmail);
                Assert.AreNotEqual(newelem, null);
            }
        }

        [TestMethod]
        public async Task EmailJobTest()
        {
            var actualtime = DateTime.Now.TimeOfDay;
            using (var db = new OnlinerProducts())
            {
                using (var udb = new ApplicationDbContext())
                {
                    var firstitem = db.Product.First();
                    var user = udb.Users.FirstOrDefault(x => x.Email == firstitem.UserEmail);
                    await GetEmailJob().Execute();
                    var redisItems = GetEmailCacheManager().GetAll<UsersUpdateEmail>();
                    var newelem =
                        redisItems.FirstOrDefault(
                            x => x.ProductName == firstitem.FullName && x.UserEmail == firstitem.UserEmail);
                    if (user.EmailTime > actualtime && user.EmailTime < actualtime - TimeSpan.FromSeconds(30))
                    {
                        Assert.IsNull(newelem);
                    }
                    else
                    {
                        Assert.IsNotNull(newelem);
                    }
                }
            }
        }

        [TestMethod]
        public void ManagerSetTest()
        {
            var newitem = new TestClass() { Id = new Random().Next(), Name = "Test", Surname = "Test" };
            var manager = GetEmailCacheManager();
            var oldcount = manager.GetAll<TestClass>().Count();
            manager.Set<TestClass>(newitem);
            var newcount = manager.GetAll<TestClass>().Count();
            Assert.AreEqual(oldcount + 1, newcount);
            manager.Delete(newitem);
        }

        [TestMethod]
        public void ManagerDeleteTest()
        {
            var newitem = new TestClass() { Id = new Random().Next(), Name = "Test", Surname = "Test" };
            var manager = GetEmailCacheManager();
            var oldcount = manager.GetAll<TestClass>().Count();
            manager.Set<TestClass>(newitem);
            manager.Delete(newitem);
            var newcount = manager.GetAll<TestClass>().Count();
            Assert.AreEqual(oldcount, newcount);
        }

        [TestMethod]
        public async Task SearchServiceTest()
        {
            var service = GetSearchService();
            var result = await service.GetProducts(new SearchRequest("apple"), "test");
            Assert.IsNotNull(result);
            Assert.IsFalse(result.Count == 0);
        }

        [TestMethod]
        public void OnlinerRequestTest()
        {
            var service = GetSearchService();
            var result = service.OnlinerRequest("apple");
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public async Task GetProductsTest()
        {
            var service = GetSearchService();
            var request = service.OnlinerRequest("apple");
            var webResponse = (HttpWebResponse)(await request.GetResponseAsync());
            var result = service.ProductsFromOnliner(webResponse);
            Assert.IsNotNull(result);
            Assert.IsFalse(result.Products.Count == 0);
        }

        [TestMethod]
        public void GetUserUpdatesTest()
        {
            var guid = Guid.NewGuid().ToString();
            var time = DateTime.Now.TimeOfDay - TimeSpan.FromSeconds(15);
            var item = new UsersUpdateEmail() {Id = guid, ProductName = "Test", Time = time, UserEmail = "Test"};
            var emailJob = GetEmailJob();
            var cacheManager = GetEmailCacheManager();
            cacheManager.Set(item);
            var result = emailJob.GetUsersUpdateEmails();
            Assert.IsNotNull(result.FirstOrDefault(x=>x.Id == guid));
            cacheManager.Delete(item);
        }

        [TestMethod]
        public async Task CreateHostAndClientTest()
        {
            using (var db = new OnlinerProducts())
            {
                var product = db.Product.First();
                product.Price.PriceMaxAmmount.Amount += 1;
                db.SaveChanges();
                var productJob = GetProductJob();
                productJob.CreateAppHost();
                await productJob.GetAndPublishUpdates(productJob.CreateClient());
                var cache = GetEmailCacheManager().GetAll<UsersUpdateEmail>();
                Assert.IsNotNull(cache.FirstOrDefault(x=>x.ProductName == product.FullName));
            }
        }

        private ProductJobService GetProductJob()
        {
            var productMapper = new ProductMapper(new ImageMapper(), new ReviewMapper(),
               new PriceMapper(new OfferMapper(), new PriceAmmountMapper()));
            var mssqlRepository = new MsSqlRepository(productMapper);
            return new ProductJobService(new MsSqlTimeServiceRepository(mssqlRepository, productMapper), new SearchService(new MsSqlProductRepository(mssqlRepository)));
        }

        private SearchService GetSearchService()
        {
            var productMapper = new ProductMapper(new ImageMapper(), new ReviewMapper(),
              new PriceMapper(new OfferMapper(), new PriceAmmountMapper()));
            return new SearchService(new MsSqlProductRepository(new MsSqlRepository(productMapper)));
        }

        private EmailCacheManager GetEmailCacheManager()
        {
            return new EmailCacheManager(new RedisClient("localhost", 6379));
        }

        private EmailJobService GetEmailJob()
        {
            return new EmailJobService(GetEmailCacheManager());
        }
    }
}
