using Microsoft.Azure.Documents;
using System;
using T002.Core.Interfaces;
using T002.Core.Models;
using T002.Infrastructure.Data;

namespace T002.Infrastructure.Repositories
{
    public class InvoiceHeaderRepository : CosmosDbRepository<InvoiceHeader>, IInvoiceHeaderRepository
    {
        public InvoiceHeaderRepository(ICosmosDbClientFactory cosmosDbClientFactory) : base(cosmosDbClientFactory)
        {
        }
        public override string CollectionName { get; } = "InvoiceHeader";
        public override string GenerateNewId(InvoiceHeader entity) => $"{entity.InvoiceNo}:{Guid.NewGuid()}";
        public override PartitionKey ResolvePartitionKey(string entityId) => new PartitionKey(entityId.Split(':')[0]);
    }
}
