using System;
using NUnit.Framework;
using Moq;
using Microsoft.Azure.Documents;
using System.Threading.Tasks;
using Microsoft.Azure.Documents.Client;
using Training.DAL.Services;
using Training.DAL.Entities;
using AutoFixture;
using Training.DAL.Services.Repositories;
using Training.DAL.Exceptions;
using Training.DAL.Services.Interfaces;

namespace Training.DAL.Tests
{
    public class UserRepositoryTests
    {
        private Mock<IDocumentClient> _client;
        private Mock<IConfigManager> _config;
        private string _id;
        private UserRepository _repository;

        [SetUp]
        public void Init()
        {
            _client = new Mock<IDocumentClient>();
            _config = new Mock<IConfigManager>();
            _id = new Fixture().Create<string>();
            _repository = new UserRepository(_client.Object, _config.Object);
            _config.Setup(p => p.DatabaseId).Returns("UserStorage");
            _config.Setup(p => p.UserCollectionId).Returns("Users");

        }

        [Test]
        public void GetUserByIdAsync_ArgumentNullException()
        {
            _client.Setup(m => m.ReadDocumentAsync(It.IsAny<Uri>(), It.IsAny<RequestOptions>()));
            Assert.ThrowsAsync<ArgumentNullException>(() => _repository.GetUserByIdAsync(null));
        }

        [Test]
        public async Task GetUserByIdAsync_Null()
        {
            var clientExeption = new DocumentClient(new Uri("https://cosmosdbview.documents.azure.com:443"), "cv1MhKGIQL71Hx8CrmPw5GiPTPshhdXRNzlm5jOqRmpKV3kuVm4f8Pq3j9arnUKq2ygBD0PMLLnNYZml1R3NJw==", new ConnectionPolicy { EnableEndpointDiscovery = false });
            _client.Setup(m => m.ReadDocumentAsync(It.IsAny<Uri>(), It.IsAny<RequestOptions>())).Returns(clientExeption.ReadDocumentAsync(UriFactory.CreateDocumentUri("UserStorage", "Users", _id)));
             var result = await _repository.GetUserByIdAsync(_id);
            Assert.AreEqual(result, null);
        }

        [Test]
        public void GetUserByIdAsync_DocumentClientException()
        {
            var clientExeption = new DocumentClient(new Uri("https://cosmosdbview.documents.azure.com:443"), "Bv1MhKGIQL71Hx8CrmPw5GiPTPshhdXRNzlm5jOqRmpKV3kuVm4f8Pq3j9arnUKq2ygBD0PMLLnNYZml1R3NJw==", new ConnectionPolicy { EnableEndpointDiscovery = false });
            _client.Setup(m => m.ReadDocumentAsync(It.IsAny<Uri>(), It.IsAny<RequestOptions>())).Returns(clientExeption.ReadDocumentAsync(UriFactory.CreateDocumentUri("UserStorage", "Users", _id)));
            Assert.ThrowsAsync<DocumentClientException>(() => _repository.GetUserByIdAsync(_id));
        }

        [Test]
        public void IsUserExistAsync_ArgumentNullException()
        {
            Assert.ThrowsAsync<ArgumentNullException>(() => _repository.IsUserExistAsync(new UserDto() { Id = null, Name = "" }));
        }

        [Test]
        public void UpdateUserAsync_ArgumentNullException()
        {
            _client.Setup(m => m.ReplaceDocumentAsync(It.IsAny<Uri>(), It.IsAny<object>(), It.IsAny<RequestOptions>()));
            Assert.ThrowsAsync<ArgumentNullException>(() => _repository.UpdateUserAsync(null, new UserDto()));
        }

        [Test]
        public void CreateUserAsync_UserIdExistException()
        {
            _client.Setup(m => m.CreateDocumentQuery<UserDto>(It.IsAny<Uri>(), It.IsAny<FeedOptions>())).Throws(new UserExistException(""));
            Assert.ThrowsAsync<UserExistException>(() => _repository.CreateUserAsync(null));
        }

        [Test]
        public void DeleteUserAsync_ArgumentNullException()
        {
            _client.Setup(m => m.DeleteDocumentAsync(It.IsAny<Uri>(), It.IsAny<RequestOptions>()));
            Assert.ThrowsAsync<ArgumentNullException>(() => _repository.DeleteUserAsync(null));
        }
    }
}
