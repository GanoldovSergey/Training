using NUnit.Framework;
using Moq;
using Training.WebApp.ErrorHandler;
using System;
using System.Web.Mvc;

namespace Training.WebApp.Tests.ErrorHandlerTests
{
    public class AiHandleErrorAttributeTests
    {
        private AiHandleErrorAttribute _handler;
        private Mock<ExceptionContext> _context;

        [SetUp]
        public void Init()
        {
            _handler = new AiHandleErrorAttribute();
            _context = new Mock<ExceptionContext>();
        }

        [Test]
        public void OnException_FilterContextIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => _handler.OnException(null));
        }

        [Test]
        public void OnException_HttpContextIsNull()
        {
            Assert.Throws<NotImplementedException>(() => _handler.OnException(new ExceptionContext { HttpContext = null }));
        }

        [Test]
        public void OnException_ExceptionIsNull()
        {
            Assert.Throws<NotImplementedException>(() => _handler.OnException(new ExceptionContext { Exception = null }));
        }

        [Test]
        public void OnException_CustomErrorDisabled()
        {
            _context.Setup(p => p.HttpContext.IsCustomErrorEnabled).Returns(false);
            _context.Setup(p => p.Exception).Returns(new Exception());
            _handler.OnException(_context.Object);
        }

        [Test]
        public void OnException_CustomErrorEnabled()
        {
            _context.Setup(p => p.HttpContext.IsCustomErrorEnabled).Returns(true);
            _context.Setup(p => p.Exception).Returns(new Exception());
            Assert.Throws<NullReferenceException>(() => _handler.OnException(_context.Object));
        }
    }
}
