using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using OnlinerTask.WEB.Controllers;
using System.Data.Entity;
using System.Threading.Tasks;
using OnlinerTask.BLL.Services.Search;
using OnlinerTask.Data.Repository;
using OnlinerTask.Data.Requests;
using OnlinerTask.Data.IdentityModels;
using OnlinerTask.Data.DataBaseModels;
using OnlinerTask.Data.EntityMappers.Implementations;

namespace OnlinerTask.Tests
{
    [TestClass]
    public class WebApiControllerTest
    {
        private const string Testname = "TEST";

        [TestMethod]
        public async Task ProductPost()
        {
            var testproducts = ProductsFromDb();
            var productMapper = new ProductMapper(new ImageMapper(), new ReviewMapper(), new PriceMapper(new OfferMapper(), new PriceAmmountMapper()));
            var repository = new MsSqlRepository(productMapper);
            var productRepository = new MsSqlProductRepository(repository);
            var controller = new ProductController(new SearchService(productRepository), productRepository);
            var item = await controller.Post(new SearchRequest(testproducts[0].ProductKey ));
            var testitem = productMapper.ConvertToModel(testproducts[0]);
            Assert.AreEqual(testitem.Description, item[0].Description);
        }

        [TestMethod]
        public async Task ProductPut()
        {
            var testproducts = ProductsFromDb();
            var productMapper = new ProductMapper(new ImageMapper(), new ReviewMapper(), new PriceMapper(new OfferMapper(), new PriceAmmountMapper()));
            var repository = new MsSqlRepository(productMapper);
            var productRepository = new MsSqlProductRepository(repository);
            var controller = new ProductController(new SearchService(productRepository), productRepository);
            await controller.Put(new PutRequest(testproducts[0].ProductKey ), Testname);
            var testcount = ProductsFromDb().Count;
            Assert.AreNotEqual(testproducts.Count, testcount);
        }

        [TestMethod]
        public async Task ProductDelete()
        {
            var testproducts = ProductsFromDb();
            var productMapper = new ProductMapper(new ImageMapper(), new ReviewMapper(), new PriceMapper(new OfferMapper(), new PriceAmmountMapper()));
            var repository = new MsSqlRepository(productMapper);
            var productRepository = new MsSqlProductRepository(repository);
            var controller = new ProductController(new SearchService(productRepository), productRepository);
            var deleteitems = testproducts.Where(x => x.UserEmail == Testname).ToList();
            foreach (var item in deleteitems)
            {
                await controller.Delete(new DeleteRequest() { ItemId = (int)item.ProductId }, Testname);
            }
            var testcount = ProductsFromDb().Count;
            Assert.AreEqual(testproducts.Count - deleteitems.Count, testcount);
        }

        [TestMethod]
        public void PersonalGet()
        {
            var testemail = ProductsFromDb().First().UserEmail;
            var productMapper = new ProductMapper(new ImageMapper(), new ReviewMapper(), new PriceMapper(new OfferMapper(), new PriceAmmountMapper()));
            var repository = new MsSqlRepository(productMapper);
            var productRepository = new MsSqlProductRepository(repository);
            var personalRepository = new MsSqlPersonalRepository(productMapper, repository);
            var controller = new PersonalController(new SearchService(productRepository), personalRepository);
            var controllerresponce = controller.Get(testemail);

            using (var db = new ApplicationDbContext())
            {
                var firstOrDefault = db.Users.FirstOrDefault(x => x.Email == testemail);
                if (firstOrDefault != null)
                {
                    var testtime = firstOrDefault.EmailTime;
                    var actualtime = controllerresponce.EmailTime;
                    Assert.AreEqual(testtime, actualtime.TimeOfDay);
                }
            }
            using (new OnlinerProducts())
            {
                var testproducts = ProductsFromDb().Where(x => x.UserEmail == testemail).Select(x => productMapper.ConvertToModel(x)).ToList();
                var actualproducts = controllerresponce.Products;
                Assert.AreEqual(testproducts.Count, actualproducts.Count);
            }
        }

        [TestMethod]
        public async Task PersonalPost()
        {
            var testemail = ProductsFromDb().First().UserEmail;
            var actualtime = DateTime.Now;
            var productMapper = new ProductMapper(new ImageMapper(), new ReviewMapper(), new PriceMapper(new OfferMapper(), new PriceAmmountMapper()));
            var repository = new MsSqlRepository(productMapper);
            var productRepository = new MsSqlProductRepository(repository);
            var personalRepository = new MsSqlPersonalRepository(productMapper, repository);
            var controller = new PersonalController(new SearchService(productRepository), personalRepository);
            using (var db = new ApplicationDbContext())
            {
                await controller.Post(new TimeRequest() { Time = actualtime }, testemail);
                var actualUser = db.Users.FirstOrDefault(x => x.Email == testemail);
                if (actualUser != null)
                {
                    var usertime = actualUser.EmailTime;
                    Assert.AreEqual(usertime, actualtime.TimeOfDay);
                }
            }
        }

        public List<Product> ProductsFromDb()
        {
            using (var db = new OnlinerProducts())
            {
                return db.Product.Include(x => x.Image).
                    Include(x => x.Price).
                    Include(x => x.Price.Offer).
                    Include(x => x.Price.PriceMinAmmount).
                    Include(x => x.Price.PriceMaxAmmount).
                    Include(x => x.Review).ToList();
            }
        }
    }
}
