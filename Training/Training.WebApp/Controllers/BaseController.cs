using System.Web.Mvc;

namespace Training.WebApp.Controllers
{
    [RoutePrefix("Error")]
    public class BaseController : Controller
    {
        [Route("Oops")]
        public ActionResult Oops()
        {
            return View();
        }

        [Route("NotFound")]
        public ActionResult NotFound()
        {
            return View();
        }

    }
}