using OnlinerTask.Data.Requests;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using OnlinerTask.BLL.Services.Job.Interfaces;
using OnlinerTask.BLL.Services.Search;
using OnlinerTask.Data.Responses;
using OnlinerTask.Data.Repository.Interfaces;

namespace OnlinerTask.WEB.Controllers
{
    [Authorize]
    public class PersonalController : ApiController
    {
        private readonly ISearchService searchService;
        private readonly IPersonalRepository repository;
        private readonly INotification notification;

        public PersonalController(ISearchService service, IPersonalRepository repository, INotification notification)
        {
            searchService = service;
            this.repository = repository;
            this.notification = notification;
        }

        public PersonalPageResponse Get(string testname = null)
        {
            return repository.PersonalProductsResponse(testname ?? User.Identity.Name);
        }

        public async Task Post(TimeRequest request, string testname = null)
        {
            await repository.ChangeSendEmailTimeAsync(request, testname ?? User.Identity.Name);
            notification.ChangeSettings(request.Time);
        }

        public async Task Put(PutRequest request)
        {
            var result = (await searchService.GetProducts(request, User.Identity.Name)).FirstOrDefault();
            repository.CreateOnlinerProduct(result, User.Identity.Name);
            notification.AddProduct(result.FullName);
        }

        public async Task Delete(DeleteRequest request)
        {
            var name = await repository.RemoveOnlinerProduct(request.ItemId, User.Identity.Name);
            notification.DeleteProduct(name);
        }
    }
}
