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
    public class UserRepository : IUserRepository
    {
        private readonly IDocumentClient _client;
        private readonly IConfigManager _config;
        private Uri CreateDocumentCollectionUri => UriFactory.CreateDocumentCollectionUri(_config.DatabaseId, _config.UserCollectionId);

        public UserRepository(IDocumentClient client, IConfigManager config)
        {
            _client = client;
            _config = config;
        }

        public async Task<UserDto> GetUserByIdAsync(string id)
        {
            try
            {
                Document document = await _client.ReadDocumentAsync(UriFactory.CreateDocumentUri(_config.DatabaseId, _config.UserCollectionId, id));
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

        public async Task<IEnumerable<UserDto>> GetUsersAsync()
        {
            return await GetUsersAsync(t => true);
        }

        public async Task<bool> IsUserExistAsync(UserDto user)
        {
            var result = await GetUsersAsync(t => t.Name == user.Name);
            return result.Any();
        }

        public async Task<UserDto> GetUserAsync(UserDto user)
        {
            var result = await GetUsersAsync(t => ((t.Name == user.Name) && (t.Password == user.Password)));
            return result.FirstOrDefault();
        }

        public async Task CreateUserAsync(UserDto user)
        {
            if (await IsUserExistAsync(user))
            {
                throw new UserExistException($"{ nameof(user) } is invalid! User is already exist.");
            }

            await _client.CreateDocumentAsync(CreateDocumentCollectionUri, user);
        }

        public async Task DeleteUserAsync(string id)
        {
            await _client.DeleteDocumentAsync(UriFactory.CreateDocumentUri(_config.DatabaseId, _config.UserCollectionId, id));
        }

        public async Task UpdateUserAsync(string id, UserDto user)
        {
            if (await IsUserExistAsync(user))
            {
                throw new UserExistException($"{ nameof(user) } is invalid! User is already exist.");
            }

            await _client.ReplaceDocumentAsync(UriFactory.CreateDocumentUri(_config.DatabaseId, _config.UserCollectionId, id), user);
        }

        private async Task<IEnumerable<UserDto>> GetUsersAsync(Expression<Func<UserDto, bool>> predicate)
        {
            IDocumentQuery<UserDto> query = _client.CreateDocumentQuery<UserDto>(
                CreateDocumentCollectionUri,
                new FeedOptions { MaxItemCount = -1 })
            .Where(predicate)
            .AsDocumentQuery();

            List<UserDto> results = new List<UserDto>();
            while (query.HasMoreResults)
            {
                results.AddRange(await query.ExecuteNextAsync<UserDto>());
            }

            return results;
        }
    }
}