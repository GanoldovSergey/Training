using System.Web.Mvc;
using Training.WebApp.Infrastructure;
using Training.BAL;
using System.Threading.Tasks;
using System.Net;
using Training.BAL.Entities;
using System.Web;

namespace Training.WebApp.Controllers
{
    [Authorize(Roles = "Admin")]
    [RoutePrefix("AdminPage"), Route("{action=IndexAsync}"), LocalOnly]
    public class AdminController : HomeController
    {
        private readonly IUserService _userService;

        public AdminController(IUserService userService) : base(userService)
        {
            _userService = userService;
        }

        [HttpGet, Route("CreateNewUser")]
        public ActionResult CreateAsync()
        {
            return View("Create");
        }

        [HttpPost, Route("CreateNewUser/{user?}"), ValidateAntiForgeryToken]
        public async Task<ActionResult> CreateAsync(UserEntity user)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.CreateUserAsync(user);
                if (result.Success)
                    return RedirectToAction("IndexAsync");
                else TempData["Error"] = result.ErrorMessage;
            }

            return View("Create", user);
        }

        [HttpPost, Route("UpdateUser/{user?}"), ValidateAntiForgeryToken]
        public async Task<ActionResult> EditAsync(UserEntity user)
        {
            if (ModelState.IsValid)
            {
                UserEntity userToUpd = await _userService.GetUserByIdAsync(user.Id);
                var result = await _userService.UpdateUserAsync(user.Id, user);
                if (result.Success)
                {
                    return (userToUpd.Name == HttpContext.GetOwinContext().Authentication.User.Identity.Name) ?
                        RedirectToAction("SignOut", "Account") : RedirectToAction("IndexAsync");
                }
                else TempData["Error"] = result.ErrorMessage;
            }

            return View("Edit", user);
        }

        [HttpGet, Route("UpdateUser")]
        public async Task<ActionResult> EditAsync(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            UserEntity user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            return View("Edit", user);
        }

        [Route("DeleteUser/{id}")]
        public async Task<ActionResult> ConfirmDeleteAsync(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            UserEntity user = await _userService.GetUserByIdAsync(id);
            if (user == null)
            {
                return HttpNotFound();
            }

            return View("Delete", user);
        }

        [HttpPost]
        [Route("DeleteUser/{id}")]
        public async Task<ActionResult> DeleteAsync(string id)
        {
            var user = await _userService.GetUserByIdAsync(id);
            await _userService.DeleteUserAsync(id);
            return (user.Name == HttpContext.GetOwinContext().Authentication.User.Identity.Name)?
                RedirectToAction("SignOut", "Account") : RedirectToAction("IndexAsync");
        }
    }
}