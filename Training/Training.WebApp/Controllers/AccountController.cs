using Microsoft.AspNet.Identity;
using Microsoft.Owin.Security;
using System;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Training.BAL;
using Training.BAL.Entities;
using Training.WebApp.Infrastructure;

namespace Training.WebApp.Controllers
{
    [RoutePrefix("AccountPage")]
    public class AccountController : Controller
    {
        private readonly IUserService _userService;
        private IAuthenticationManager AuthenticationManager => HttpContext.GetOwinContext().Authentication;

        public AccountController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet, Route("SignIn")]
        public ActionResult SignInAsync()
        {
            return View("SignIn");
        }

        [HttpPost, Route("SignIn"), ValidateAntiForgeryToken]
        public async Task<ActionResult> SignInAsync(UserEntity user)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.GetUserAsync(user);
                if (result.Id != null)
                {
                    AuthenticationManager.SignIn(new AuthenticationProperties()
                    {
                        AllowRefresh = true,
                        IsPersistent = false,
                        ExpiresUtc = DateTime.UtcNow.AddDays(7)
                    }, CreateRequiredClaims.Create(result));

                    return RedirectToAction("Index", "Home");
                }
            }
            ModelState.AddModelError("", Resource.ErrorInvalidNameOrPassword);
            return View("SignIn", user);

        }

        [HttpGet, Route("SignUp")]
        public ActionResult SignUpAsync()
        {
            return View("SignUp");
        }

        [HttpPost, Route("SignUp"), ValidateAntiForgeryToken]
        public async Task<ActionResult> SignUpAsync(UserEntity user)
        {
            if (!ModelState.IsValid) return View("SignUp", user);

            var result = await _userService.CreateUserAsync(user);

            if (result.Success)
            {
                await SignInAsync(user);
                return RedirectToAction("Index", "Home");
            }
            else TempData["Error"] = result.ErrorMessage;
            return View("SignUp");
        }

        [Authorize, Route("SignOut")]
        public ActionResult SignOut()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie, DefaultAuthenticationTypes.ExternalCookie);
            return RedirectToAction("Index", "Home");
        }
    }
}