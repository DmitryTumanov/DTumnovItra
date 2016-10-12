using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OnlinerTask.Data.DBModels;
using System.Collections.Generic;
using System.Linq;
using OnlinerTask.WEB.Controllers;
using OnlinerTask.BLL.Repository;
using OnlinerTask.BLL.Services;
using OnlinerTask.DAL.SearchModels;
using System.Data.Entity;
using OnlinerTask.Data.EntityMappers;
using System.Threading.Tasks;
using OnlinerTask.WEB.Models;

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
            var controller = new ProductController(new SearchService(), new MsSQLRepository());
            var item = await controller.Post(new Request() { SearchString = testproducts[0].ProductKey });
            var testitem = new ProductMapper().ConvertToModel(testproducts[0]);
            Assert.AreEqual(testitem.Description, item[0].Description);
        }

        [TestMethod]
        public async Task ProductPUT()
        {
            var testproducts = ProductsFromDB();
            var controller = new ProductController(new SearchService(), new MsSQLRepository());
            await controller.Put(new Request() { SearchString = testproducts[0].ProductKey }, testname);
            var testcount = ProductsFromDB().Count();
            Assert.AreNotEqual(testproducts.Count, testcount);
        }

        [TestMethod]
        public async Task ProductDELETE()
        {
            var testproducts = ProductsFromDB();
            var controller = new ProductController(new SearchService(), new MsSQLRepository());
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
            var controller = new PersonalController(new SearchService(), new MsSQLRepository());
            var controllerresponce = controller.Get();
            using (var db = new ApplicationDbContext())
            {
                var testtime = db.Users.FirstOrDefault(x => x.Email == testemail).EmailTime;
                var actualtime = controllerresponce.EmailTime;
                Assert.AreEqual(testtime, actualtime);
            }
            using (var mdb = new OnlinerProducts())
            {
                var testproducts = ProductsFromDB().Where(x => x.UserEmail == testemail).Select(x => new ProductMapper().ConvertToModel(x));
                var actualproducts = controllerresponce.Products;
                Assert.AreEqual(testproducts, actualproducts);
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
