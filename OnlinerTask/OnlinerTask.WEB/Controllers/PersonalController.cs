using OnlinerTask.Data.Requests;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using OnlinerTask.BLL.Services.Products;
using OnlinerTask.BLL.Services.TimeChange;
using OnlinerTask.Data.Responses;

namespace OnlinerTask.WEB.Controllers
{
    [Authorize]
    public class PersonalController : ApiController
    {
        private readonly IManager manager;
        private readonly ITimeChanger timeChanger;

        public PersonalController(IManager manager, ITimeChanger timeChanger)
        {
            this.manager = manager;
            this.timeChanger = timeChanger;
        }

        public async Task<PersonalPageResponse> Get(string testname = null)
        {
            return await manager.GetProducts(testname ?? User.Identity.Name);
        }

        public async Task<HttpResponseMessage> Post(TimeRequest request, string testname = null)
        {
            await timeChanger.ChangePersonalTime(request, testname ?? User.Identity.Name);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public async Task<HttpResponseMessage> Put(PutRequest request)
        {
            await manager.AddProduct(request, User.Identity.Name);
            return Request.CreateResponse(HttpStatusCode.OK);
        }

        public async Task<HttpResponseMessage> Delete(DeleteRequest request)
        {
            await manager.RemoveProduct(request, User.Identity.Name);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
