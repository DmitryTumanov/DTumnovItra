using System.Web.Mvc;
using OnlinerTask.WEB.Filters;

namespace OnlinerTask.WEB.Controllers
{
    [Authorize]
    [MvcLogAction]
    public class SearchController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}