using NUnit.Framework;
using Training.WebApp.Controllers;
using Moq;
using Training.BAL;
using System.Threading.Tasks;

namespace Training.WebApp.Tests.ControllersTests
{
    public class HomeControllerTests
    {
        [Test]
        public void Index_Pozitive()
        {
            Mock<IUserService> service = new Mock<IUserService>();
            var controller = new HomeController(service.Object);
            controller.Index();
        }

        [Test]
        public async Task IndexAsyncGet_Pozitive()
        {
            Mock<IUserService> service = new Mock<IUserService>();
            var controller = new HomeController(service.Object);
            await controller.IndexAsync();

            service.Verify(m => m.GetUsersAsync(), Times.Exactly(1));
        }

        [Test]
        public async Task IndexAsyncPost_Pozitive()
        {
            Mock<IUserService> service = new Mock<IUserService>();
            var controller = new HomeController(service.Object);
            await controller.IndexAsyncPost();

            service.Verify(m => m.GetUsersAsync(), Times.Exactly(1));
        }
    }
}
