using Microsoft.Azure.Cosmos;
using System.Threading.Tasks;
using T002.Core.Interfaces;

namespace T002.Core.Data
{
    public class CosmosDB : IRepository
    {
        private CosmosClient _cosmosClient = null;
        private Database _database = null;
        private Container _container = null;

        public CosmosDB(CosmosClient cosmosClient ,Container cosmosContainer, Database database)
        {
            _cosmosClient = cosmosClient;
            _database = database;
            _container = cosmosContainer;
        }

        public async Task<T> AddAsync(T entity)
        {
            return await this.container.CreateItemAsync<Invoice>(entity, new PartitionKey(entity.Id));
        }

        public async Task DeleteAsync(Invoice entity)
        {
            await this.container.DeleteItemAsync<Invoice>(entity.Id, new PartitionKey(entity.Id));
        }
        public async Task<Invoice> GetByIdAsync(string id)
        {
            return await this.container.ReadItemAsync<Invoice>(id, new PartitionKey(id));
        }

        public async Task<Invoice> UpdateAsync(Invoice entity)
        {
            return await this.container.ReplaceItemAsync<Invoice>(entity, entity.Id, new PartitionKey(entity.Id));
        }



    }
}
