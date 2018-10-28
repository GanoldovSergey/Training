using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;
using Training.DAL.Entities;
using Training.DAL.Exceptions;
using Training.DAL.Services.Interfaces;

namespace Training.DAL.Services.Repositories
{
    public class TestRepository : ITestRepository
    {
        private readonly IDocumentClient _client;
        private readonly IConfigManager _config;
        private Uri CreateDocumentCollectionUri => UriFactory.CreateDocumentCollectionUri(_config.DatabaseId, _config.TestCollectionId);

        public TestRepository(IDocumentClient client, IConfigManager config)
        {
            _client = client;
            _config = config;
        }

        public async Task<TestDto> GetTestByIdAsync(string id)
        {
            try
            {
                Document document = await _client.ReadDocumentAsync(UriFactory.CreateDocumentUri(_config.DatabaseId, _config.TestCollectionId, id));
                return (dynamic)document;
            }
            catch (DocumentClientException e)
            {
                if (e.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return null;
                }
                else
                {
                    throw;
                }
            }
        }

        public async Task<IEnumerable<TestDto>> GetTestsAsync()
        {
            return await GetTestsAsync(t => true);
        }

        public async Task<bool> IsTestExistAsync(TestDto test)
        {
            var result = await GetTestsAsync(t => t.Name == test.Name);
            return result.Any();
        }

        public async Task CreateTestAsync(TestDto test)
        {
            if (await IsTestExistAsync(test))
            {
                throw new TestExistException($"{ nameof(test) } is invalid! Test is already exist.");
            }

            await _client.CreateDocumentAsync(CreateDocumentCollectionUri, test);
        }

        public async Task DeleteTestAsync(string id)
        {
            await _client.DeleteDocumentAsync(UriFactory.CreateDocumentUri(_config.DatabaseId, _config.TestCollectionId, id));
        }

        public async Task UpdateTestAsync(string id, TestDto test)
        {
            if (await IsTestExistAsync(test))
            {
                throw new TestExistException($"{ nameof(test) } is invalid! Test is already exist.");
            }

            await _client.ReplaceDocumentAsync(UriFactory.CreateDocumentUri(_config.DatabaseId, _config.TestCollectionId, id), test);
        }

        private async Task<IEnumerable<TestDto>> GetTestsAsync(Expression<Func<TestDto, bool>> predicate)
        {
            var x = _client.CreateDocumentQuery<TestDto>(
                CreateDocumentCollectionUri,
                new FeedOptions { MaxItemCount = -1 });
            var y = x.Where(predicate);
            IDocumentQuery<TestDto> query = y.AsDocumentQuery();

            List<TestDto> results = new List<TestDto>();
            while (query.HasMoreResults)
            {
                results.AddRange(await query.ExecuteNextAsync<TestDto>());
            }

            return results;
        }
    }
}
