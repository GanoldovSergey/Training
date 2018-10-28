using System.Web.Mvc;
using Training.BAL;

namespace Training.WebApp.Controllers
{
    [Authorize(Roles = "Teacher, Student")]
    [RoutePrefix("UserPage"), Route("{action=IndexAsync}")]
    public class UserController : HomeController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService) : base(userService)
        {
            _userService = userService;
        }       
    }
}
