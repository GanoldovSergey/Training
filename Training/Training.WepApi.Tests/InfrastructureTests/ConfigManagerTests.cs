using NUnit.Framework;
using Training.WebApi.Infrastructure;

namespace Training.WepApi.Tests.InfrastructureTests
{
    public class ConfigManagerTests
    {
        [Test]
        public void DatabaseId_Pozitive()
        {
            ConfigManager config = new ConfigManager();
            Assert.AreEqual(config.DatabaseId, "UserStorage");
        }

        [Test]
        public void UserCollectionId_Pozitive()
        {
            ConfigManager config = new ConfigManager();
            Assert.AreEqual(config.UserCollectionId, "Users");
        }

        [Test]
        public void TestCollectionId_Pozitive()
        {
            ConfigManager config = new ConfigManager();
            Assert.AreEqual(config.TestCollectionId, "Tests");
        }

        [Test]
        public void QuestionCollectionId_Pozitive()
        {
            ConfigManager config = new ConfigManager();
            Assert.AreEqual(config.QuestionCollectionId, "Questions");
        }

        [Test]
        public void Endpoint_Pozitive()
        {
            ConfigManager config = new ConfigManager();
            Assert.AreEqual(config.Endpoint, "https://localhost:8081");
        }

        [Test]
        public void AuthKey_Pozitive()
        {
            ConfigManager config = new ConfigManager();
            Assert.AreEqual(config.AuthKey, "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==");
        }

    }
}
