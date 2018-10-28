using NUnit.Framework;
using Moq;
using Training.BAL;
using Training.WebApp.Controllers;
using System.Threading.Tasks;
using Training.BAL.Entities;
using System.Web;
using System.Web.Mvc;
using System.Collections.Generic;
using System.Web.Routing;

namespace Training.WebApp.Tests.ControllersTests
{
    public class AdminControllerTests
    {
        private Mock<IUserService> _service;
        private AdminController _controller;

        [SetUp]
        public void Init()
        {
            _service = new Mock<IUserService>();
            _controller = new AdminController(_service.Object);
        }

        [TestCase("123", "name")]
        public async Task EditAsyncPost_Pozitive(string id, string name)
        {
            _service.Setup(m => m.UpdateUserAsync(It.IsAny<string>(), It.IsAny<UserEntity>())).ReturnsAsync(new UserResponse());
            var user = new UserEntity { Id = id, Name = name };
            await _controller.EditAsync(user);

            _service.Verify(fw => fw.UpdateUserAsync(user.Id, user), Times.Exactly(1));
        }

        [TestCase("123", "name")]
        public async Task EditAsyncPost_ModelStateInvalid(string id, string name)
        {
            _controller.ModelState.AddModelError("IsValid", "False");
            var user = new UserEntity { Id = id, Name = name };
            await _controller.EditAsync(user);

            _service.Verify(fw => fw.UpdateUserAsync(user.Id, user), Times.Exactly(0));
        }


        [TestCase("123")]
        public async Task EditAsyncGet_Pozitive(string id)
        {
            _service.Setup(m => m.GetUserByIdAsync(It.IsAny<string>())).ReturnsAsync(new UserEntity());
            await _controller.EditAsync(id);

            _service.Verify(fw => fw.GetUserByIdAsync(id), Times.Exactly(1));
        }

        [TestCase("123")]
        public async Task EditAsyncGet_UserNull(string id)
        {
            _service.Setup(m => m.GetUserByIdAsync(It.IsAny<string>())).ReturnsAsync(() => null);
            await _controller.EditAsync(id);

            _service.Verify(fw => fw.GetUserByIdAsync(id), Times.Exactly(1));
        }

        [TestCase(null)]
        public async Task EditAsyncGet_BadRequest(string id)
        {
            _service.Setup(m => m.GetUserByIdAsync(It.IsAny<string>())).ReturnsAsync(new UserEntity());
            await _controller.EditAsync(id);

            _service.Verify(fw => fw.GetUserByIdAsync(id), Times.Exactly(0));
        }

        //TODO DeleteAsync_Pozitive
        //[TestCase("123")]
        //public async Task DeleteAsync_Pozitive(string id)
        //{
        //    Mock<HttpContextBase> moqContext = new Mock<HttpContextBase>();
        //    moqContext.Setup(m => m.Items[It.IsAny<string>()]).Returns(new Dictionary<string, object>() { { "a", "b" } });
        //    moqContext.Setup(m => m.User.Identity.Name).Returns("name");
        //    _controller.ControllerContext = new ControllerContext(moqContext.Object, new RouteData(), _controller);
        //    _service.Setup(m => m.GetUserByIdAsync(It.IsAny<string>())).ReturnsAsync(new UserEntity());
        //    _service.Setup(m => m.DeleteUserAsync(It.IsAny<string>())).Returns(() => Task.FromResult(10));
        //    await _controller.DeleteAsync(id);

        //    _service.Verify(fw => fw.DeleteUserAsync(id), Times.Exactly(1));
        //}

        [Test]
        public void CreateAsyncGet_Pozitive()
        {
            _controller.CreateAsync();
        }

        [TestCase("123", "name")]
        public async Task CreateAsyncPost_SuccessTrue(string id, string name)
        {
            var user = new UserEntity { Id = id, Name = name };
            _service.Setup(m => m.CreateUserAsync(It.IsAny<UserEntity>())).ReturnsAsync(new UserResponse { Success = true });
            await _controller.CreateAsync(user);

            _service.Verify(fw => fw.CreateUserAsync(user), Times.Exactly(1));
        }

        [TestCase("123", "name")]
        public async Task CreateAsyncPost_SuccessFalse(string id, string name)
        {
            var user = new UserEntity { Id = id, Name = name };
            _service.Setup(m => m.CreateUserAsync(It.IsAny<UserEntity>())).ReturnsAsync(new UserResponse { Success = false });
            await _controller.CreateAsync(user);

            _service.Verify(fw => fw.CreateUserAsync(user), Times.Exactly(1));
        }

        [TestCase("123", "name")]
        public async Task CreateAsyncPost_ModelStateInvalid(string id, string name)
        {
            _controller.ModelState.AddModelError("IsValid", "False");
            var user = new UserEntity { Id = id, Name = name };
            await _controller.CreateAsync(user);

            _service.Verify(fw => fw.CreateUserAsync(user), Times.Exactly(0));
        }



    }
}
