using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using OnlinerTask.WEB.Controllers;
using OnlinerTask.BLL.Repository;
using OnlinerTask.BLL.Services;
using System.Data.Entity;
using OnlinerTask.Data.EntityMappers;
using System.Threading.Tasks;
using OnlinerTask.WEB.Models;
using OnlinerTask.Data.SearchModels;
using OnlinerTask.Data.Repository;
using OnlinerTask.Data.Requests;
using OnlinerTask.Data.IdentityModels;
using OnlinerTask.Data.DataBaseModels;

namespace OnlinerTask.Tests
{
    [TestClass]
    public class WebApiControllerTest
    {
        private readonly string testname = "TEST";

        [TestMethod]
        public async Task ProductPOST()
        {
            var testproducts = ProductsFromDB();
            var productMapper = new ProductMapper(new ImageMapper(), new ReviewMapper(), new PriceMapper(new OfferMapper(), new PriceAmmountMapper()));
            var repository = new MsSQLProductRepository(productMapper);
            var controller = new ProductController(new SearchService(repository), repository);
            var item = await controller.Post(new SearchRequest() { SearchString = testproducts[0].ProductKey });
            var testitem = productMapper.ConvertToModel(testproducts[0]);
            Assert.AreEqual(testitem.Description, item[0].Description);
        }

        [TestMethod]
        public async Task ProductPUT()
        {
            var testproducts = ProductsFromDB();
            var productMapper = new ProductMapper(new ImageMapper(), new ReviewMapper(), new PriceMapper(new OfferMapper(), new PriceAmmountMapper()));
            var repository = new MsSQLProductRepository(productMapper);
            var controller = new ProductController(new SearchService(repository), repository);
            await controller.Put(new PutRequest() { SearchString = testproducts[0].ProductKey }, testname);
            var testcount = ProductsFromDB().Count();
            Assert.AreNotEqual(testproducts.Count, testcount);
        }

        [TestMethod]
        public async Task ProductDELETE()
        {
            var testproducts = ProductsFromDB();
            var productMapper = new ProductMapper(new ImageMapper(), new ReviewMapper(), new PriceMapper(new OfferMapper(), new PriceAmmountMapper()));
            var repository = new MsSQLProductRepository(productMapper);
            var controller = new ProductController(new SearchService(repository), repository);
            var deleteitems = testproducts.Where(x => x.UserEmail == testname).ToList();
            foreach (var item in deleteitems)
            {
                await controller.Delete(new DeleteRequest() { ItemId = (int)item.ProductId }, testname);
            }
            var testcount = ProductsFromDB().Count();
            Assert.AreEqual(testproducts.Count - deleteitems.Count, testcount);
        }

        [TestMethod]
        public void PersonalGET()
        {
            var testemail = ProductsFromDB().First().UserEmail;
            var productMapper = new ProductMapper(new ImageMapper(), new ReviewMapper(), new PriceMapper(new OfferMapper(), new PriceAmmountMapper()));
            var productRepository = new MsSQLProductRepository(productMapper);
            var personalRepository = new MsSQLPersonalRepository(productMapper, productRepository);
            var controller = new PersonalController(new SearchService(productRepository), personalRepository);
            var controllerresponce = controller.Get(testemail);

            using (var db = new ApplicationDbContext())
            {
                var testtime = db.Users.FirstOrDefault(x => x.Email == testemail).EmailTime;
                var actualtime = controllerresponce.EmailTime;
                Assert.AreEqual(testtime, actualtime.TimeOfDay);
            }
            using (var mdb = new OnlinerProducts())
            {
                var testproducts = ProductsFromDB().Where(x => x.UserEmail == testemail).Select(x => productMapper.ConvertToModel(x)).ToList();
                var actualproducts = controllerresponce.Products;
                Assert.AreEqual(testproducts.Count, actualproducts.Count);
            }
        }

        [TestMethod]
        public async Task PersonalPOST()
        {
            var testemail = ProductsFromDB().First().UserEmail;
            var actualtime = DateTime.Now;
            var productMapper = new ProductMapper(new ImageMapper(), new ReviewMapper(), new PriceMapper(new OfferMapper(), new PriceAmmountMapper()));
            var productRepository = new MsSQLProductRepository(productMapper);
            var personalRepository = new MsSQLPersonalRepository(productMapper, productRepository);
            var controller = new PersonalController(new SearchService(productRepository), personalRepository);
            using (var db = new ApplicationDbContext())
            {
                await controller.Post(new TimeRequest() { Time = actualtime }, testemail);
                var usertime = db.Users.FirstOrDefault(x => x.Email == testemail).EmailTime;
                Assert.AreEqual(usertime, actualtime.TimeOfDay);
            }
        }

        public List<Product> ProductsFromDB()
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
