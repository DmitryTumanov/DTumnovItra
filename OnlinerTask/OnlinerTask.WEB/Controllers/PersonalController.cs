using OnlinerTask.BLL.Services;
using OnlinerTask.Data.Requests;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using OnlinerTask.Data.Responses;
using OnlinerTask.Data.Repository.Interfaces;

namespace OnlinerTask.WEB.Controllers
{
    [Authorize]
    public class PersonalController : ApiController
    {
        private ISearchService searchService;
        private IPersonalRepository repository;
        public PersonalController(ISearchService service, IPersonalRepository repository)
        {
            searchService = service;
            this.repository = repository;
        }

        public PersonalPageResponse Get(string testname = null)
        {
            return repository.PersonalProductsResponse(testname ?? User.Identity.Name);
        }

        public async Task Post(TimeRequest request, string testname = null)
        {
            await repository.ChangeSendEmailTimeAsync(request, testname ?? User.Identity.Name);
        }

        public async Task Put(PutRequest request)
        {
            var result = (await searchService.GetProducts(request, User.Identity.Name)).FirstOrDefault();
            repository.CreateOnlinerProduct(result, User.Identity.Name);
        }

        public async Task Delete(DeleteRequest request)
        {
            await repository.RemoveOnlinerProduct(request.ItemId, User.Identity.Name);
        }
    }
}
