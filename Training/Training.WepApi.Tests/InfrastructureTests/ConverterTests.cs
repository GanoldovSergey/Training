//using AutoFixture;
//using NUnit.Framework;
//using Training.DAL.Entities;
//using Training.WebApi.Entities;
//using Training.WebApi.Infrastructure;

//namespace Training.WepApi.Tests.InfrastructureTests
//{
//    [TestFixture]
//    public class ConverterTests
//    {
//        private readonly Converter _converter = new Converter();

//        [Test]
//        public void ToBalTest_Positve()
//        {
//            TestDto test = new Fixture().Create<TestDto>();

//            var result = _converter.ToBalTest(test);

//            Assert.AreEqual(result.Id, test.Id);
//            Assert.AreEqual(result.Name, test.Name);
//        }

//        [Test]
//        public void ToBalTest_Null()
//        {
//            var result = _converter.ToBalTest(null);

//            Assert.NotNull(result);
//            Assert.IsNull(result.Id);
//            Assert.IsNull(result.Name);
//            Assert.IsNull(result.Questions);
//        }



//        [Test]
//        public void ToDalTest_Positve()
//        {
//            TestEntity test = new Fixture().Create<TestEntity>();

//            var result = _converter.ToDalTest(test);

//            Assert.AreEqual(result.Id, test.Id);
//            Assert.AreEqual(result.Name, test.Name);
//        }

//        [Test]
//        public void ToDalTest_Null()
//        {
//            var result = _converter.ToDalTest(null);

//            Assert.NotNull(result);
//            Assert.IsNull(result.Id);
//            Assert.IsNull(result.Name);
//            Assert.IsNull(result.Questions);
//        }



//        [Test]
//        public void ToBalQuestion_Positve()
//        {
//            QuestionDto question = new Fixture().Create<QuestionDto>();

//            var result = _converter.ToBalQuestion(question);

//            Assert.AreEqual(result.Id, question.Id);
//            Assert.AreEqual(result.IsMultiple, question.IsMultiple);
//            Assert.AreEqual(result.Text, question.Text);
//        }

//        [Test]
//        public void ToBalQuestion_Null()
//        {
//            var result = _converter.ToBalQuestion(null);

//            Assert.NotNull(result);
//            Assert.IsNull(result.Id);
//            Assert.IsNull(result.Text);
//            Assert.IsNull(result.Answers);
//            Assert.AreEqual(result.IsMultiple, false);
//        }



//        [Test]
//        public void ToDalQuestion_Positve()
//        {
//            QuestionEntity question = new Fixture().Create<QuestionEntity>();

//            var result = _converter.ToDalQuestion(question);

//            Assert.AreEqual(result.Id, question.Id);
//            Assert.AreEqual(result.Text, question.Text);
//        }

//        [Test]
//        public void ToDalQuestion_Null()
//        {
//            var result = _converter.ToDalQuestion(null);

//            Assert.NotNull(result);
//            Assert.IsNull(result.Id);
//            Assert.IsNull(result.Text);
//            Assert.IsNull(result.Answers);
//        }



//        [Test]
//        public void ToBalAnswer_Positve()
//        {
//            AnswerDto answer = new Fixture().Create<AnswerDto>();

//            var result = _converter.ToBalAnswer(answer);

//            Assert.AreEqual(result.Text, answer.Text);
//            Assert.AreEqual(result.IsRight, answer.IsRight);
//        }

//        [Test]
//        public void ToBalAnswer_Null()
//        {
//            var result = _converter.ToBalAnswer(null);

//            Assert.NotNull(result);
//            Assert.IsNull(result.Text);
//            Assert.AreEqual(result.IsRight, false);
//        }



//        [Test]
//        public void ToDalAnswer_Positve()
//        {
//            AnswerEntity answer = new Fixture().Create<AnswerEntity>();

//            var result = _converter.ToDalAnswer(answer);

//            Assert.AreEqual(result.Text, answer.Text);
//            Assert.AreEqual(result.IsRight, answer.IsRight);
//        }

//        [Test]
//        public void ToDalAnswer_Null()
//        {
//            var result = _converter.ToDalAnswer(null);

//            Assert.NotNull(result);
//            Assert.IsNull(result.Text);
//            Assert.AreEqual(result.IsRight, false);
//        }
//    }
//}
