using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;

namespace T002.Infrastructure.Data
{
    public class CosmosDbClientFactory : ICosmosDbClientFactory
    {
        private readonly string _database;
        private readonly List<string> _collection;
        private readonly IDocumentClient _documentClient;

        public CosmosDbClientFactory(string databaseName, List<string> collectionNames, IDocumentClient documentClient)
        {
            _database = databaseName ;
            _collection = collectionNames ;
            _documentClient = documentClient ;
        }
        public ICosmosDbClient GetClient(string collectionName)
        {
            if (!_collection.Contains(collectionName))
            {
                throw new ArgumentException($"collection - {collectionName} not found ");
            }

            return new CosmosDbClient(_database, collectionName, _documentClient);
        }

        public async Task EnsureDbSetupAsync()
        {
            await _documentClient.ReadDatabaseAsync(UriFactory.CreateDatabaseUri(_database));

            foreach (var collectionName in _collection)
            {
                await _documentClient.ReadDocumentCollectionAsync(
                    UriFactory.CreateDocumentCollectionUri(_database, collectionName));
            }
        }
    }
}
