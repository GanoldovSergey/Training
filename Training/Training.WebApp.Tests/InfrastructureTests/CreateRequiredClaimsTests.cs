using NUnit.Framework;
using System;
using Training.BAL.Entities;
using Training.WebApp.Infrastructure;

namespace Training.WebApp.Tests.InfrastructureTests
{
    public class CreateRequiredClaimsTests
    {
        [Test]
        public void Create_Pozitive()
        {
            CreateRequiredClaims.Create(new UserEntity { Id = "", Name = "", Role = Roles.Student, Password = "" });
        }

        [Test]
        public void Create_NullReferenceException()
        {
            Assert.Throws<NullReferenceException>(() => CreateRequiredClaims.Create(null));
        }

        [Test]
        public void Create_ArgumentNullException()
        {
            Assert.Throws<ArgumentNullException>(() => CreateRequiredClaims.Create(new UserEntity()));
        }

    }
}
