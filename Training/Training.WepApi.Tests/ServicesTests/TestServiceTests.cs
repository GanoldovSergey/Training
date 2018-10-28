//using System;
//using System.Collections.Generic;
//using System.Threading.Tasks;
//using AutoFixture;
//using Moq;
//using NUnit.Framework;
//using Training.DAL.Entities;
//using Training.DAL.Exceptions;
//using Training.DAL.Services;
//using Training.DAL.Services.Interfaces;
//using Training.WebApi.Entities;
//using Training.WebApi.Interfaces;
//using Training.WebApi.Services;

//namespace Training.WepApi.Tests.ServicesTests
//{
//    [TestFixture]
//    public class TestServiceTests
//    {
//        private TestEntity _test;
//        private string _id;
//        private Mock<ITestRepository> _repository;
//        private Mock<IConverter> _converter;
//        private TestService _target;

//        [SetUp]
//        public void Init()
//        {
//            _test = new Fixture().Create<TestEntity>();
//            _id = new Fixture().Create<string>();
//            _repository = new Mock<ITestRepository>();
//            _converter = new Mock<IConverter>();
//            _target = new TestService(_repository.Object, _converter.Object);
//        }

//        [Test]
//        public async Task CreateTestAsync_Positive()
//        {
//            _repository.Setup(m => m.CreateTestAsync(It.IsAny<TestDto>())).Returns(() => Task.FromResult(10));

//            var result = await _target.CreateTestAsync(_test);

//            _repository.Verify(fw => fw.CreateTestAsync(It.IsAny<TestDto>()), Times.Exactly(1));
//            Assert.AreEqual(result.Success, true);
//        }

//        [Test]
//        public async Task CreateTestAsync_AnswerExistException()
//        {
//            _repository.Setup(m => m.CreateTestAsync(It.IsAny<TestDto>())).Throws(new TestExistException(""));

//            var result = await _target.CreateTestAsync(_test);

//            _repository.Verify(fw => fw.CreateTestAsync(It.IsAny<TestDto>()), Times.Exactly(1));
//            Assert.AreEqual(result.Success, false);
//        }

//        [Test]
//        public async Task DeleteTestAsync_Positive()
//        {
//            _repository.Setup(m => m.DeleteTestAsync(It.IsAny<string>())).Returns(() => Task.FromResult(10));

//            await _target.DeleteTestAsync(_id);

//            _repository.Verify(fw => fw.DeleteTestAsync(_id), Times.Exactly(1));
//        }

//        [Test]
//        public async Task UpdateTestAsync_Positive()
//        {
//            _repository.Setup(m => m.UpdateTestAsync(It.IsAny<string>(), It.IsAny<TestDto>())).Returns(Task.FromResult(10));

//            var result = await _target.UpdateTestAsync(_id, _test);

//            _repository.Verify(fw => fw.UpdateTestAsync(_id, It.IsAny<TestDto>()), Times.Exactly(1));
//            Assert.AreEqual(result.Success, true);
//        }

//        [Test]
//        public async Task UpdateTestAsync_AnswerExistException()
//        {
//            _repository.Setup(m => m.UpdateTestAsync(It.IsAny<string>(), It.IsAny<TestDto>())).Throws(new TestExistException(""));

//            var result = await _target.UpdateTestAsync(_id, _test);

//            _repository.Verify(fw => fw.UpdateTestAsync(_id, It.IsAny<TestDto>()), Times.Exactly(1));
//            Assert.AreEqual(result.Success, false);
//        }

//        [Test]
//        public async Task GetTestByIdAsync_Positive()
//        {
//            _repository.Setup(m => m.GetTestByIdAsync(It.IsAny<string>())).ReturnsAsync(new TestDto());

//            await _target.GetTestByIdAsync(_id);

//            _repository.Verify(fw => fw.GetTestByIdAsync(_id), Times.Exactly(1));
//        }

//        [Test]
//        public async Task GetTestAsync_Positive()
//        {
//            _repository.Setup(m => m.GetTestsAsync()).ReturnsAsync(new List<TestDto> { null, new TestDto() });

//            await _target.GetTestsAsync();

//            _repository.Verify(fw => fw.GetTestsAsync(), Times.Exactly(1));
//        }

//        [Test]
//        public async Task GetTestAsync_Null()
//        {
//            _repository.Setup(m => m.GetTestsAsync()).ReturnsAsync(() => null);

//            var result = await _target.GetTestsAsync();

//            _repository.Verify(fw => fw.GetTestsAsync(), Times.Exactly(1));
//            Assert.AreEqual(result, null);
//        }

//        [Test]
//        public async Task IsTestExistAsync_Positive()
//        {
//            _repository.Setup(m => m.IsTestExistAsync(It.IsAny<TestDto>())).ReturnsAsync(new bool());

//            await _target.IsTestExistAsync(_test);

//            _repository.Verify(fw => fw.IsTestExistAsync(It.IsAny<TestDto>()), Times.Exactly(1));
//        }

//        [Test]
//        public async Task AddQuestionToTest_Pozitive()
//        {
//            _repository.Setup(m => m.GetTestByIdAsync(It.IsAny<string>())).ReturnsAsync(new TestDto {Questions = new List<QuestionDto>() });
//            _repository.Setup(m => m.UpdateTestAsync(It.IsAny<string>(), It.IsAny<TestDto>())).Returns(Task.FromResult(10));

//            await _target.AddQuestionToTest(_id, new QuestionEntity());

//            _repository.Verify(fw => fw.GetTestByIdAsync(_id), Times.Exactly(1));
//            _repository.Verify(fw => fw.UpdateTestAsync(_id, It.IsAny<TestDto>()), Times.Exactly(1));

//        }

//        [Test]
//        public void AddQuestionToTest_NullReferenceExeption()
//        {
//            _repository.Setup(m => m.GetTestByIdAsync(It.IsAny<string>())).ReturnsAsync(() => null);

//            Assert.ThrowsAsync<NullReferenceException>(() => _target.AddQuestionToTest(_id, new QuestionEntity()));

//            _repository.Verify(fw => fw.GetTestByIdAsync(_id), Times.Exactly(1));
//            _repository.Verify(fw => fw.UpdateTestAsync(_id, It.IsAny<TestDto>()), Times.Exactly(0));

//        }

//        [Test]
//        public async Task DelQuestionFromTest_Pozitive()
//        {
//            _repository.Setup(m => m.GetTestByIdAsync(It.IsAny<string>())).ReturnsAsync(new TestDto { Questions = new List<QuestionDto>() });
//            _repository.Setup(m => m.UpdateTestAsync(It.IsAny<string>(), It.IsAny<TestDto>())).Returns(Task.FromResult(10));

//            await _target.DelQuestionFromTest(_id, new QuestionEntity());

//            _repository.Verify(fw => fw.GetTestByIdAsync(_id), Times.Exactly(1));
//            _repository.Verify(fw => fw.UpdateTestAsync(_id, It.IsAny<TestDto>()), Times.Exactly(1));

//        }


//        [Test]
//        public void DelQuestionFromTest_NullReferenceException()
//        {
//            _repository.Setup(m => m.GetTestByIdAsync(It.IsAny<string>())).ReturnsAsync(() => null);
//            _repository.Setup(m => m.UpdateTestAsync(It.IsAny<string>(), It.IsAny<TestDto>())).Returns(Task.FromResult(10));

//            Assert.ThrowsAsync<NullReferenceException>(() => _target.DelQuestionFromTest(_id, new QuestionEntity()));

//            _repository.Verify(fw => fw.GetTestByIdAsync(_id), Times.Exactly(1));
//            _repository.Verify(fw => fw.UpdateTestAsync(_id, It.IsAny<TestDto>()), Times.Exactly(0));

//        }

//    }
//}
