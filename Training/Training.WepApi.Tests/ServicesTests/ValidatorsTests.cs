using NUnit.Framework;
using System.Collections.Generic;
using Training.WebApi.Entities;
using Training.WebApi.Services;

namespace Training.WepApi.Tests.ServicesTests
{
    class ValidatorsTests
    {
        [Test]
        public void ValidateQuestion_QuestionIsNull()
        {
            QuestionEntity question = null;

            Assert.AreEqual(false, Validators.ValidateQuestion(question).Success);
        }
    }
}
