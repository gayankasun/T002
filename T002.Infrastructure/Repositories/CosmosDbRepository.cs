using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;
using T002.Core.Interfaces;
using T002.Core.Models;


namespace T002.Infrastructure.Repositories
{
	public abstract class CosmosDbRepository<T>: IRepository<T>
    {
        private readonly ICosmosDbClientFactory _cosmosDbClientFactory;
        protected CosmosDbRepository(ICosmosDbClientFactory cosmosDbClientFactory)
        {
            //DI 
            _cosmosDbClientFactory = cosmosDbClientFactory;
        }

        public async Task<T> GetByIdAsync(string documentId)
        {
            var cosmosDbClient = _cosmosDbClientFactory.GetClient(CollectionName);
            var document = await cosmosDbClient.ReadDocumentAsync(documentId, new RequestOptions
            {
                PartitionKey = ResolvePartitionKey(documentId)
            });

            return JsonConvert.DeserializeObject<T>(document.ToString());
        }

        public async Task<T> AddAsync(T entity)
        {
            entity.Id = GenerateNewId(entity);
            var cosmosDbClient = _cosmosDbClientFactory.GetClient(CollectionName);
            var document = await cosmosDbClient.CreateDocumentAsync(entity);

            return JsonConvert.DeserializeObject<T>(document.ToString());
        }

        public async Task<T> UpdateAsync(T entity)
        {
            var cosmosDbClient = _cosmosDbClientFactory.GetClient(CollectionName);
            await cosmosDbClient.ReplaceDocumentAsync(entity.Id, entity);

            var document = await cosmosDbClient.ReadDocumentAsync(entity.Id, new RequestOptions
            {
                PartitionKey = ResolvePartitionKey(entity.Id)
            });

            return JsonConvert.DeserializeObject<T>(document.ToString());
        }

        public async Task DeleteAsync(T entity)
        {
            var cosmosDbClient = _cosmosDbClientFactory.GetClient(CollectionName);
            await cosmosDbClient.DeleteDocumentAsync(entity.Id, new RequestOptions
            {
                PartitionKey = ResolvePartitionKey(entity.Id)
            });
        }
        public abstract string CollectionName { get; }
        public virtual PartitionKey ResolvePartitionKey(string entityId) => null;
        public virtual string GenerateNewId(T entity) => Guid.NewGuid().ToString();
    }
}
