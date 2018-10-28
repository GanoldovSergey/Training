using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading.Tasks;
using Training.DAL.Entities;
using System.Collections.Generic;

namespace CreateDB
{
    class Program
    {
        private const string Endpoint = "https://localhost:8081";
        private const string AuthKey = "C2y6yDjf5/R+ob0N8A7Cgv30VRDJIWEHLM+4QDU5DE2nQ9nDuVTqobD4b8mGGyPMbIZnqyMsEcaGQy67XIw/Jw==";
        private static readonly string DatabaseId = "UserStorage";
        private static readonly string UserCollectionId = "Users";
        private static readonly string TestCollectionId = "Tests";
        private static readonly string QuestionCollectionId = "Questions";
        private static readonly DocumentClient Client = new DocumentClient(new Uri(Endpoint), AuthKey, new ConnectionPolicy { EnableEndpointDiscovery = false });


        private static void Main()
        {
            CreateDatabaseIfNotExistsAsync().Wait();
            CreateCollectionIfNotExistsAsync(UserCollectionId).Wait();
            CreateCollectionIfNotExistsAsync(TestCollectionId).Wait();
            CreateCollectionIfNotExistsAsync(QuestionCollectionId).Wait();
            FillDb(UserCollectionId,"users.json");
        }

        private static async Task CreateDatabaseIfNotExistsAsync()
        {
            try
            {
                await Client.ReadDatabaseAsync(UriFactory.CreateDatabaseUri(DatabaseId));
            }
            catch (DocumentClientException e)
            {
                if (e.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    await Client.CreateDatabaseAsync(new Database { Id = DatabaseId });
                }
                else
                {
                    throw;
                }
            }
        }

        private static async Task CreateCollectionIfNotExistsAsync(string collectionId)
        {
            try
            {
                await Client.ReadDocumentCollectionAsync(UriFactory.CreateDocumentCollectionUri(DatabaseId, collectionId));
            }
            catch (DocumentClientException e)
            {
                if (e.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    await Client.CreateDocumentCollectionAsync(
                        UriFactory.CreateDatabaseUri(DatabaseId),
                        new DocumentCollection { Id = collectionId },
                        new RequestOptions { OfferThroughput = 1000 });
                }
                else
                {
                    throw;
                }
            }
        }
        private static void FillDb(string collectionId, string path)

        {
            var users = TakeFromFile(path);
            foreach (var user in users)
                Client.CreateDocumentAsync(UriFactory.CreateDocumentCollectionUri(DatabaseId, collectionId), user).Wait();
        }

        private static IEnumerable<UserDto> TakeFromFile(string path)
        {
            if (!File.Exists(path)) return new List<UserDto>();
            var file = File.OpenText(path);
            JsonSerializer serializer = new JsonSerializer();
            return (IEnumerable<UserDto>)serializer.Deserialize(file, typeof(IEnumerable<UserDto>));
        }
    }
}