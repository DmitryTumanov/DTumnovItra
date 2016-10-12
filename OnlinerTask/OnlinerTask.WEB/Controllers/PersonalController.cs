using OnlinerTask.Data.Repository;
using OnlinerTask.BLL.Services;
using OnlinerTask.DAL.SearchModels;
using OnlinerTask.Data.EntityMappers;
using OnlinerTask.Data.SearchModels;
using OnlinerTask.WEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace OnlinerTask.WEB.Controllers
{
    [Authorize]
    public class PersonalController : ApiController
    {
        private ISearchService search_service;
        private IRepository repository;
        public PersonalController(ISearchService service, IRepository repository)
        {
            search_service = service;
            this.repository = repository;
        }

        public SearchResult Get(string testname = null)
        {
            var result = repository.GetPersonalProducts(testname ?? User.Identity.Name).Select(x => new ProductMapper().ConvertToModel(x));
            using (var db = new ApplicationDbContext())
            {
                var time = DateTime.Now;
                var user = db.Users.Where(x => x.UserName == (testname ?? User.Identity.Name)).FirstOrDefault();
                if (user != null) {
                    time = user.EmailTime;
                }
                return new SearchResult() { EmailTime = time, Products = result.ToList()};
            }
        }

        public void Post(TimeRequest request, string testname = null)
        {
            using (var db = new ApplicationDbContext())
            {
                var user = db.Users.FirstOrDefault(x => x.UserName == (testname ?? User.Identity.Name));
                if (user != null && request != null)
                {
                    user.EmailTime = request.Time;
                    db.SaveChanges();
                }
            }
        }

        public async Task Put(Request responce)
        {
            var result = (await search_service.GetProducts(responce, repository, User.Identity.Name)).FirstOrDefault();
            repository.CreateOnlinerProduct(result, User.Identity.Name);
        }

        public async Task Delete(DeleteRequest request)
        {
            await repository.RemoveOnlinerProduct(request.ItemId, User.Identity.Name);
        }
    }
}
