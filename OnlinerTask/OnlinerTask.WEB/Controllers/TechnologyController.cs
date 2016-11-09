using System.Net;
using System.Net.Http;
using System.Web.Http;
using OnlinerTask.BLL.Services.ConfigChange;
using OnlinerTask.Data.Requests;
using OnlinerTask.Data.Resources;

namespace OnlinerTask.WEB.Controllers
{
    public class TechnologyController : ApiController
    {
        private readonly IConfigChanger changer;

        public TechnologyController(IConfigChanger changer)
        {
            this.changer = changer;
        }

        public HttpResponseMessage Post(TechnologyRequest request)
        {
            changer.TechnologySwap(request.Technology);
            return Request.CreateResponse(HttpStatusCode.OK);
        }
    }
}
