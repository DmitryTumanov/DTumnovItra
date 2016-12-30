using System.Web.Mvc;

namespace OnlinerTask.WEB.Controllers
{
    [Authorize]
    public class SearchController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}