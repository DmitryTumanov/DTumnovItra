using OnlinerTask.Data.Repository;
using OnlinerTask.BLL.Services;
using OnlinerTask.Data.SearchModels;
using OnlinerTask.Data.Requests;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using OnlinerTask.Data.Responses;

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

        public PersonalPageResponse Get(string testname = null)
        {
            return repository.PersonalProductsResponse(testname ?? User.Identity.Name);
        }

        public HttpResponseMessage Post(TimeRequest request, string testname = null)
        {
            repository.ChangeSendEmailTime(request, testname ?? User.Identity.Name);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public async Task Put(PutRequest request)
        {
            var result = (await search_service.GetProducts(request, repository, User.Identity.Name)).FirstOrDefault();
            repository.CreateOnlinerProduct(result, User.Identity.Name);
        }

        public async Task Delete(DeleteRequest request)
        {
            await repository.RemoveOnlinerProduct(request.ItemId, User.Identity.Name);
        }
    }
}
