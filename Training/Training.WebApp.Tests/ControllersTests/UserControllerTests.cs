using NUnit.Framework;
using Moq;
using Training.BAL;
using Training.WebApp.Controllers;

namespace Training.WebApp.Tests.ControllersTests
{
    public class UserControllerTests
    {
        [Test]
        public void UserController_Pozitive()
        {
            Mock<IUserService> service = new Mock<IUserService>();
            var controller = new UserController(service.Object);
        }
    }
}
