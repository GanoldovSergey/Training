using System.Web.Mvc;
using System.Threading.Tasks;
using Training.BAL;

namespace Training.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUserService _userService;

        public HomeController(IUserService userService)
        {
            _userService = userService;
        }

        public ActionResult Index()
        {
            return View("Index");
        }

        [HttpGet]
        public async Task<ActionResult> IndexAsync()
        {
            var items = await _userService.GetUsersAsync();
            return View("Index", items);
        }

        [HttpPost]
        public async Task<ActionResult> IndexAsyncPost()
        {
            var items = await _userService.GetUsersAsync();
            return Json(items);
        }
    }
}