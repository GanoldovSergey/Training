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
    public class QuestionRepository : IQuestionRepository
    {
        private readonly IDocumentClient _client;
        private readonly IConfigManager _config;
        private Uri CreateDocumentCollectionUri => UriFactory.CreateDocumentCollectionUri(_config.DatabaseId, _config.QuestionCollectionId);

        public QuestionRepository(IDocumentClient client, IConfigManager config)
        {
            _client = client;
            _config = config;
        }

        public async Task<QuestionDto> GetQuestionByIdAsync(string id)
        {
            try
            {
                Document document = await _client.ReadDocumentAsync(UriFactory.CreateDocumentUri(_config.DatabaseId, _config.QuestionCollectionId, id));
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

        public async Task<IEnumerable<QuestionDto>> GetQuestionsAsync()
        {
            return await GetQuestionsAsync(t => true);
        }

        public async Task<bool> IsQuestionExistAsync(QuestionDto question)
        {
            var result = await GetQuestionsAsync(t => t.Text == question.Text);
            return result.Any();
        }

        public async Task CreateQuestionAsync(QuestionDto question)
        {
            if (await IsQuestionExistAsync(question))
            {
                throw new QuestionExistException($"{ nameof(question) } is invalid! Question is already exist.");
            }

            await _client.CreateDocumentAsync(CreateDocumentCollectionUri, question);
        }

        public async Task DeleteQuestionAsync(string id)
        {
            await _client.DeleteDocumentAsync(UriFactory.CreateDocumentUri(_config.DatabaseId, _config.QuestionCollectionId, id));
        }

        public async Task UpdateQuestionAsync(string id, QuestionDto question)
        {
            if (await IsQuestionExistAsync(question))
            {
                throw new QuestionExistException($"{ nameof(question) } is invalid! Question is already exist.");
            }

            await _client.ReplaceDocumentAsync(UriFactory.CreateDocumentUri(_config.DatabaseId, _config.QuestionCollectionId, id), question);
        }

        private async Task<IEnumerable<QuestionDto>> GetQuestionsAsync(Expression<Func<QuestionDto, bool>> predicate)
        {
            var x = _client.CreateDocumentQuery<QuestionDto>(
                CreateDocumentCollectionUri,
                new FeedOptions { MaxItemCount = -1 });
            var y = x.Where(predicate);
            IDocumentQuery<QuestionDto> query = y.AsDocumentQuery();

            List<QuestionDto> results = new List<QuestionDto>();
            while (query.HasMoreResults)
            {
                results.AddRange(await query.ExecuteNextAsync<QuestionDto>());
            }

            return results;
        }
    }
}

