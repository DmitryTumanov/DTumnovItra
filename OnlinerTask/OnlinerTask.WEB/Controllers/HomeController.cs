using System.Web.Mvc;
using OnlinerTask.WEB.Filters;

namespace OnlinerTask.WEB.Controllers
{
    [Authorize]
    [MvcLogAction]
    [Authorize(Roles = "Admin")]
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }
    }
}
