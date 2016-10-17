using System.Web.Mvc;

namespace OnlinerTask.WEB.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
