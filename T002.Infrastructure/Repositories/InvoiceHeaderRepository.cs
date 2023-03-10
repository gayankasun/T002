using Microsoft.Azure.Cosmos;
using System.Threading.Tasks;
using T002.Core.Data;
using T002.Core.Interfaces;
using T002.Core.Models;
using Database = Microsoft.Azure.Cosmos.Database;

namespace T002.Infrastructure.Repositories
{
    public class InvoiceHeaderRepository : IInvoiceHeaderRepository
    {


        private CosmosDB cosmosDB = null;

        public InvoiceHeaderRepository(CosmosDB cosmosDB)
        {
         
        }

        public async Task<Invoice> AddAsync(Invoice entity)
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
