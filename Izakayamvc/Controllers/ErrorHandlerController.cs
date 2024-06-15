using System.Web.Mvc;

namespace Izakayamvc.Controllers
{
    public class ErrorHandlerController : Controller
    {
        public ActionResult NotFound()
        {
            return View();
        }
    }
}