using System.Net;
using System.Net.Http;
using System.Web.Http;
using OnlinerTask.Data.Requests;
using OnlinerTask.Data.Resources;

namespace OnlinerTask.WEB.Controllers
{
    public class TechnologyController : ApiController
    {
        public HttpResponseMessage Post(TechnologyRequest request)
        {
            Configurations.NotifyTechnology = request.Technology;
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
