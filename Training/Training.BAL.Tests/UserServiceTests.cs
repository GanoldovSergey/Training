using NUnit.Framework;
using System.Threading.Tasks;
using System.Collections.Generic;
using Moq;
using Training.BAL.Entities;
using Training.BAL.Services;
using Training.DAL.Entities;
using Training.DAL.Services;
using AutoFixture;
using System;
using Training.DAL.Exceptions;
using Training.DAL.Services.Interfaces;

namespace Training.BAL.Tests
{
    [TestFixture]
    public class UserServiceTests
    {
        private UserEntity _user;
        private string _id;
        private Mock<IUserRepository> _mock;
        private Mock<ILogService> _mock2;
        private UserService _target;

        [SetUp]
        public void Init()
        {
            _user = new Fixture().Create<UserEntity>();
            _id = new Fixture().Create<string>();
            _mock = new Mock<IUserRepository>();
            _mock2 = new Mock<ILogService>();
            _target = new UserService(_mock.Object, _mock2.Object);
        }

        [Test]
        public async Task CreateUserAsync_Positive()
        {
            _mock.Setup(m => m.CreateUserAsync(It.IsAny<UserDto>())).Returns(() => Task.FromResult(10));

            var result = await _target.CreateUserAsync(_user);

            _mock.Verify(fw => fw.CreateUserAsync(It.IsAny<UserDto>()), Times.Exactly(1));
            _mock2.Verify(fw => fw.InfoWriteToLog(It.IsAny<DateTime>(), It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(2));
            Assert.AreEqual(result.Success, true);
        }

        [Test]
        public async Task CreateUserAsync_UserExistException()
        {
            _mock.Setup(m => m.CreateUserAsync(It.IsAny<UserDto>())).Throws(new UserExistException(""));

            var result = await _target.CreateUserAsync(_user);

            _mock.Verify(fw => fw.CreateUserAsync(It.IsAny<UserDto>()), Times.Exactly(1));
            _mock2.Verify(fw => fw.ErrorWriteToLog(It.IsAny<DateTime>(), It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(1));
            Assert.AreEqual(result.Success, false);
        }

        [Test]
        public async Task DeleteUserAsync_Positive()
        {
            _mock.Setup(m => m.DeleteUserAsync(It.IsAny<string>())).Returns(() => Task.FromResult(10));

            await _target.DeleteUserAsync(_id);

            _mock.Verify(fw => fw.DeleteUserAsync(_id), Times.Exactly(1));
            _mock2.Verify(fw => fw.InfoWriteToLog(It.IsAny<DateTime>(), It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(2));
        }

        [Test]
        public async Task UpdateUserAsync_Positive()
        {
            _mock.Setup(m => m.UpdateUserAsync(It.IsAny<string>(), It.IsAny<UserDto>())).Returns(Task.FromResult(10));

            var result = await _target.UpdateUserAsync(_id, _user);

            _mock.Verify(fw => fw.UpdateUserAsync(_id, It.IsAny<UserDto>()), Times.Exactly(1));
            _mock2.Verify(fw => fw.InfoWriteToLog(It.IsAny<DateTime>(), It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(2));
            Assert.AreEqual(result.Success, true);
        }

        [Test]
        public async Task UpdateUserAsync_UserExistException()
        {
            _mock.Setup(m => m.UpdateUserAsync(It.IsAny<string>(), It.IsAny<UserDto>())).Throws(new UserExistException(""));

            var result = await _target.UpdateUserAsync(_id, _user);

            _mock.Verify(fw => fw.UpdateUserAsync(_id, It.IsAny<UserDto>()), Times.Exactly(1));
            _mock2.Verify(fw => fw.ErrorWriteToLog(It.IsAny<DateTime>(), It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(1));
            Assert.AreEqual(result.Success, false);
        }

        [Test]
        public async Task GetUserByIdAsync_Positive()
        {
            _mock.Setup(m => m.GetUserByIdAsync(It.IsAny<string>())).ReturnsAsync(new UserDto());

            await _target.GetUserByIdAsync(_id);

            _mock.Verify(fw => fw.GetUserByIdAsync(_id), Times.Exactly(1));
            _mock2.Verify(fw => fw.InfoWriteToLog(It.IsAny<DateTime>(), It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(2));
        }

        [Test]
        public async Task GetUsersAsync_Positive()
        {
            _mock.Setup(m => m.GetUsersAsync()).ReturnsAsync(new List<UserDto> { null, new UserDto() });

            await _target.GetUsersAsync();

            _mock.Verify(fw => fw.GetUsersAsync(), Times.Exactly(1));
            _mock2.Verify(fw => fw.InfoWriteToLog(It.IsAny<DateTime>(), It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(2));
        }

        [Test]
        public async Task GetUsersAsync_Null()
        {
            _mock.Setup(m => m.GetUsersAsync()).ReturnsAsync(() => null);

            var result = await _target.GetUsersAsync();

            _mock.Verify(fw => fw.GetUsersAsync(), Times.Exactly(1));
            _mock2.Verify(fw => fw.InfoWriteToLog(It.IsAny<DateTime>(), It.IsAny<string>(), It.IsAny<string>()), Times.Exactly(2));
            Assert.AreEqual(result, null);
        }

        [Test]
        public async Task IsUserExistAsync_Positive()
        {
            _mock.Setup(m => m.IsUserExistAsync(It.IsAny<UserDto>())).ReturnsAsync(new bool());

            await _target.IsUserExistAsync(_user);

            _mock.Verify(fw => fw.IsUserExistAsync(It.IsAny<UserDto>()), Times.Exactly(1));
        }

        [Test]
        public async Task GetUseByIdAsync_Positive()
        {
            _mock.Setup(m => m.GetUserByIdAsync(It.IsAny<string>())).ReturnsAsync(new UserDto());

            await _target.GetUserAsync(_user);

            _mock.Verify(fw => fw.GetUserAsync(It.IsAny<UserDto>()), Times.Exactly(1));
        }
    }
}
