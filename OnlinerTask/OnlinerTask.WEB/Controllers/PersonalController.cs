using OnlinerTask.Data.Requests;
using System.Linq;
using System.Net;
using System.Net.Http;
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

        public async Task<HttpResponseMessage> Post(TimeRequest request, string testname = null)
        {
            await repository.ChangeSendEmailTimeAsync(request, testname ?? User.Identity.Name);
            notification.ChangeSettings(request.Time);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public async Task<HttpResponseMessage> Put(PutRequest request)
        {
            var result = (await searchService.GetProducts(request, User.Identity.Name)).FirstOrDefault();
            repository.CreateOnlinerProduct(result, User.Identity.Name);
            notification.AddProduct(result.FullName);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public async Task<HttpResponseMessage> Delete(DeleteRequest request)
        {
            var name = await repository.RemoveOnlinerProduct(request.ItemId, User.Identity.Name);
            notification.DeleteProduct(name);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
