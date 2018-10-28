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
//    public class QuestionServiceTests
//    {
//        private QuestionEntity _question;
//        private string _id;
//        private Mock<IQuestionRepository> _repository;
//        private Mock<IConverter> _converter;
//        private QuestionService _target;

//        [SetUp]
//        public void Init()
//        {
//            _question = new Fixture().Create<QuestionEntity>();
//            _id = new Fixture().Create<string>();
//            _repository = new Mock<IQuestionRepository>();
//            _converter = new Mock<IConverter>();
//            _target = new QuestionService(_repository.Object, _converter.Object);
//        }

//        [Test]
//        public async Task CreateQuestionAsync_Positive()
//        {
//            _repository.Setup(m => m.CreateQuestionAsync(It.IsAny<QuestionDto>())).Returns(() => Task.FromResult(10));

//            var result = await _target.CreateQuestionAsync(_question);

//            _repository.Verify(fw => fw.CreateQuestionAsync(It.IsAny<QuestionDto>()), Times.Exactly(1));
//            Assert.AreEqual(result.Success, true);
//        }

//        [Test]
//        public async Task CreateQuestionAsync_AnswerExistException()
//        {
//            _repository.Setup(m => m.CreateQuestionAsync(It.IsAny<QuestionDto>())).Throws(new QuestionExistException(""));

//            var result = await _target.CreateQuestionAsync(_question);

//            _repository.Verify(fw => fw.CreateQuestionAsync(It.IsAny<QuestionDto>()), Times.Exactly(1));
//            Assert.AreEqual(result.Success, false);
//        }

//        [Test]
//        public async Task DeleteQuestionAsync_Positive()
//        {
//            _repository.Setup(m => m.DeleteQuestionAsync(It.IsAny<string>())).Returns(() => Task.FromResult(10));

//            await _target.DeleteQuestionAsync(_id);

//            _repository.Verify(fw => fw.DeleteQuestionAsync(_id), Times.Exactly(1));
//        }

//        [Test]
//        public async Task UpdateQuestionAsync_Positive()
//        {
//            _repository.Setup(m => m.UpdateQuestionAsync(It.IsAny<string>(), It.IsAny<QuestionDto>())).Returns(Task.FromResult(10));

//            var result = await _target.UpdateQuestionAsync(_id, _question);

//            _repository.Verify(fw => fw.UpdateQuestionAsync(_id, It.IsAny<QuestionDto>()), Times.Exactly(1));
//            Assert.AreEqual(result.Success, true);
//        }

//        [Test]
//        public async Task UpdateQuestionAsync_QuestionExistException()
//        {
//            _repository.Setup(m => m.UpdateQuestionAsync(It.IsAny<string>(), It.IsAny<QuestionDto>())).Throws(new QuestionExistException(""));

//            var result = await _target.UpdateQuestionAsync(_id, _question);

//            _repository.Verify(fw => fw.UpdateQuestionAsync(_id, It.IsAny<QuestionDto>()), Times.Exactly(1));
//            Assert.AreEqual(result.Success, false);
//        }

//        [Test]
//        public async Task GetQuestionByIdAsync_Positive()
//        {
//            _repository.Setup(m => m.GetQuestionByIdAsync(It.IsAny<string>())).ReturnsAsync(new QuestionDto());

//            await _target.GetQuestionByIdAsync(_id);

//            _repository.Verify(fw => fw.GetQuestionByIdAsync(_id), Times.Exactly(1));
//        }

//        [Test]
//        public async Task GetQuestionAsync_Positive()
//        {
//            _repository.Setup(m => m.GetQuestionsAsync()).ReturnsAsync(new List<QuestionDto> { null, new QuestionDto() });

//            await _target.GetQuestionsAsync();

//            _repository.Verify(fw => fw.GetQuestionsAsync(), Times.Exactly(1));
//        }

//        [Test]
//        public async Task GetQuestionAsync_Null()
//        {
//            _repository.Setup(m => m.GetQuestionsAsync()).ReturnsAsync(() => null);

//            var result = await _target.GetQuestionsAsync();

//            _repository.Verify(fw => fw.GetQuestionsAsync(), Times.Exactly(1));
//            Assert.AreEqual(result, null);
//        }

//        [Test]
//        public async Task IsQuestionExistAsync_Positive()
//        {
//            _repository.Setup(m => m.IsQuestionExistAsync(It.IsAny<QuestionDto>())).ReturnsAsync(new bool());

//            await _target.IsQuestionExistAsync(_question);

//            _repository.Verify(fw => fw.IsQuestionExistAsync(It.IsAny<QuestionDto>()), Times.Exactly(1));
//        }

//        //[Test]
//        //public async Task AddAnswerToQuestion_Pozitive()
//        //{
//        //    _repository.Setup(m => m.GetQuestionByIdAsync(It.IsAny<string>())).ReturnsAsync(new QuestionDto { Answers = new List<AnswerDto>() });
//        //    _repository.Setup(m => m.UpdateQuestionAsync(It.IsAny<string>(), It.IsAny<QuestionDto>())).Returns(Task.FromResult(10));

//        //    await _target.AddAnswerToQuestion(_id, new AnswerEntity());

//        //    _repository.Verify(fw => fw.GetQuestionByIdAsync(_id), Times.Exactly(1));
//        //    _repository.Verify(fw => fw.UpdateQuestionAsync(_id, It.IsAny<QuestionDto>()), Times.Exactly(1));

//        //}

//        //[Test]
//        //public void AddAnswerToQuestion_NullReferenceExeption()
//        //{
//        //    _repository.Setup(m => m.GetQuestionByIdAsync(It.IsAny<string>())).ReturnsAsync(() => null);

//        //    Assert.ThrowsAsync<NullReferenceException>(() => _target.AddAnswerToQuestion(_id, new AnswerEntity()));

//        //    _repository.Verify(fw => fw.GetQuestionByIdAsync(_id), Times.Exactly(1));
//        //    _repository.Verify(fw => fw.UpdateQuestionAsync(_id, It.IsAny<QuestionDto>()), Times.Exactly(0));

//        //}

//        //[Test]
//        //public async Task DelAnswerFromQuestion_Pozitive()
//        //{
//        //    _repository.Setup(m => m.GetQuestionByIdAsync(It.IsAny<string>())).ReturnsAsync(new QuestionDto { Answers = new List<AnswerDto>() });
//        //    _repository.Setup(m => m.UpdateQuestionAsync(It.IsAny<string>(), It.IsAny<QuestionDto>())).Returns(Task.FromResult(10));

//        //    await _target.DelAnswerFromQuestion(_id, new AnswerEntity());

//        //    _repository.Verify(fw => fw.GetQuestionByIdAsync(_id), Times.Exactly(1));
//        //    _repository.Verify(fw => fw.UpdateQuestionAsync(_id, It.IsAny<QuestionDto>()), Times.Exactly(1));

//        //}


//        //[Test]
//        //public void DelAnswerFromQuestion_NullReferenceException()
//        //{
//        //    _repository.Setup(m => m.GetQuestionByIdAsync(It.IsAny<string>())).ReturnsAsync(() => null);
//        //    _repository.Setup(m => m.UpdateQuestionAsync(It.IsAny<string>(), It.IsAny<QuestionDto>())).Returns(Task.FromResult(10));

//        //    Assert.ThrowsAsync<NullReferenceException>(() => _target.DelAnswerFromQuestion(_id, new AnswerEntity()));

//        //    _repository.Verify(fw => fw.GetQuestionByIdAsync(_id), Times.Exactly(1));
//        //    _repository.Verify(fw => fw.UpdateQuestionAsync(_id, It.IsAny<QuestionDto>()), Times.Exactly(0));

//        //}

//    }
//}
