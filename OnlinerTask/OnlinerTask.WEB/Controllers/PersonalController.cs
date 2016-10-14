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
        private readonly ISearchService _searchService;
        private readonly IPersonalRepository _repository;
        public PersonalController(ISearchService service, IPersonalRepository repository)
        {
            _searchService = service;
            _repository = repository;
        }

        public PersonalPageResponse Get(string testname = null)
        {
            return _repository.PersonalProductsResponse(testname ?? User.Identity.Name);
        }

        public async Task Post(TimeRequest request, string testname = null)
        {
            await _repository.ChangeSendEmailTimeAsync(request, testname ?? User.Identity.Name);
        }

        public async Task Put(PutRequest request)
        {
            var result = (await _searchService.GetProducts(request, User.Identity.Name)).FirstOrDefault();
            _repository.CreateOnlinerProduct(result, User.Identity.Name);
        }

        public async Task Delete(DeleteRequest request)
        {
            await _repository.RemoveOnlinerProduct(request.ItemId, User.Identity.Name);
        }
    }
}
