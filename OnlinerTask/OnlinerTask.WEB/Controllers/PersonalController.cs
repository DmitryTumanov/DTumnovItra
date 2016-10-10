using OnlinerTask.BLL.Repository;
using OnlinerTask.BLL.Services;
using OnlinerTask.DAL.SearchModels;
using OnlinerTask.Data.EntityMappers;
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

        public IEnumerable<ProductModel> Get()
        {
            return repository.GetPersonalProducts(User.Identity.Name).Select(x => ProductMapper.ConvertToModel(x));
        }

        public void Post()
        {

        }

        public async void Put(Request responce)
        {
            var result = (await search_service.GetProducts(responce, repository, User.Identity.Name)).FirstOrDefault();
            repository.CreateOnlinerProduct(result, User.Identity.Name);
        }

        public void Delete(DeleteRequest request)
        {
            repository.RemoveOnlinerProduct(request.ItemId, User.Identity.Name);
            return;
        }
    }
}
