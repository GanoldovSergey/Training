using NUnit.Framework;
using Moq;
using Training.BAL;
using Training.WebApp.Controllers;
using System.Threading.Tasks;
using Training.BAL.Entities;
using System.Web.Mvc;
using System.Web;
using System.Web.Routing;
using System.Collections.Generic;

namespace Training.WebApp.Tests.ControllersTests
{
    [TestFixture]
    public class AccountControllerTests
    {
        private Mock<IUserService> _service;
        private AccountController _controller;

        [SetUp]
        public void Init()
        {
            _service = new Mock<IUserService>();
            _controller = new AccountController(_service.Object);
        }

        [Test]
        public void SignInAsyncGet_Pozitive()
        {
            _controller.SignInAsync();
        }

        [Test]
        public async Task SignInAsyncPost_InvalidUser()
        {
            _service.Setup(m => m.GetUserAsync(It.IsAny<UserEntity>())).ReturnsAsync(() => new UserEntity());
            await _controller.SignInAsync(new UserEntity());
        }

        [Test]
        public async Task SignInAsyncPost_ValidUser()
        {
            Mock<HttpContextBase> moqContext = new Mock<HttpContextBase>();
            moqContext.Setup(m => m.Items[It.IsAny<string>()]).Returns(new Dictionary<string, object>() { { "a", "b" } });
            _controller.ControllerContext = new ControllerContext(moqContext.Object, new RouteData(), _controller);
            _service.Setup(m => m.GetUserAsync(It.IsAny<UserEntity>())).ReturnsAsync(() => new UserEntity { Id = "", Name = "", Password = "", Role = Roles.Student });

            await _controller.SignInAsync(new UserEntity());
        }

        [Test]
        public async Task SignInAsyncPost_ValidAdmin()
        {
            Mock<HttpContextBase> moqContext = new Mock<HttpContextBase>();
            moqContext.Setup(m => m.Items[It.IsAny<string>()]).Returns(new Dictionary<string, object>(){{"a", "b"}});
            _controller.ControllerContext = new ControllerContext(moqContext.Object, new RouteData(), _controller);
            _service.Setup(m => m.GetUserAsync(It.IsAny<UserEntity>())).ReturnsAsync(() => new UserEntity { Id = "", Name = "",Password = "" , Role = Roles.Admin });

            await _controller.SignInAsync(new UserEntity());
        }

        [Test]
        public void SignUpAsyncGet_Pozitive()
        {
            _controller.SignUpAsync();
        }

        [Test]
        public async Task SignUpAsyncPost_ModelStateInvalid()
        {
            _controller.ModelState.AddModelError("IsValid", "False");
            await _controller.SignUpAsync(new UserEntity());
            _service.Verify(m => m.CreateUserAsync(It.IsAny<UserEntity>()), Times.Exactly(0));
        }

        [Test]
        public async Task SignUpAsyncPost_SuccessTrue()
        {
            _service.Setup(m => m.CreateUserAsync(It.IsAny<UserEntity>())).ReturnsAsync(() => new UserResponse { Success = true });
            _service.Setup(m => m.GetUserAsync(It.IsAny<UserEntity>())).ReturnsAsync(() => new UserEntity());
            await _controller.SignUpAsync(new UserEntity { Id = "", Role = Roles.Student });
            _service.Verify(m => m.CreateUserAsync(It.IsAny<UserEntity>()), Times.Exactly(1));
            _service.Verify(m => m.GetUserAsync(It.IsAny<UserEntity>()), Times.Exactly(1));
        }

        [Test]
        public async Task SignUpAsyncPost_SuccessFalse()
        {
            _service.Setup(m => m.CreateUserAsync(It.IsAny<UserEntity>())).ReturnsAsync(() => new UserResponse { Success = false });
            await _controller.SignUpAsync(new UserEntity { Id = "", Role = Roles.Student });
            _service.Verify(m => m.CreateUserAsync(It.IsAny<UserEntity>()), Times.Exactly(1));
            _service.Verify(m => m.GetUserAsync(It.IsAny<UserEntity>()), Times.Exactly(0));
        }
    }
}
