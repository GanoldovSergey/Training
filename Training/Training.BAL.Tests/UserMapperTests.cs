using NUnit.Framework;
using Training.BAL.Entities;
using Training.BAL.Services;
using AutoFixture;
using Training.DAL.Entities;

namespace Training.BAL.Tests
{
    [TestFixture]
    public class UserMapperTests
    {
        private UserEntity _userEntity;
        private UserDto _userDto;

        [SetUp]
        public void Init()
        {
            _userEntity = new Fixture().Create<UserEntity>();
            _userDto = new Fixture().Create<UserDto>();
        }

        [Test]
        public void ToDalUser_Positve()
        {
            var result = _userEntity.ToDalUser();
            Assert.AreEqual(result.Id, _userEntity.Id);
            Assert.AreEqual(result.Name , _userEntity.Name);
        }

        [Test]
        public void ToBalUser_Positve()
        {
            var result = _userDto.ToBalUser();
            Assert.AreEqual(result.Id, _userDto.Id);
            Assert.AreEqual(result.Name, _userDto.Name);
        }
        
        [Test]
        public void ToBalUserNegative()
        {
            UserDto user = null;
            var result = user.ToBalUser();

            Assert.NotNull(result);
            Assert.IsNull(result.Id);
            Assert.IsNull(result.Name);
            Assert.IsNull(result.Password);
            Assert.AreEqual((int)result.Role,0);
        }

        [Test]
        public void ToDalUserNegative()
        {
            UserEntity user = null;
            var result = user.ToDalUser();

            Assert.NotNull(result);
            Assert.IsNull(result.Id);
            Assert.IsNull(result.Name);
            Assert.IsNull(result.Password);
            Assert.AreEqual(result.Role,0);
        }

    }
}
