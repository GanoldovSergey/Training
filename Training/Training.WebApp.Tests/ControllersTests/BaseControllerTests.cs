using NUnit.Framework;
using Training.WebApp.Controllers;

namespace Training.WebApp.Tests.ControllersTests
{
    public class BaseControllerTests
    {
        [Test]
        public void Oops_Pozitive()
        {
            var controller = new BaseController();
            controller.Oops();
        }

        [Test]
        public void NotFound_Pozitive()
        {
            var controller = new BaseController();
            controller.NotFound();
        }
    }
}
