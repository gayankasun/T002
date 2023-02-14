using Microsoft.Azure.Documents;
using T002.Core.Models;

namespace T002.Infrastructure.Data
{
    public interface IDocumentBaseContext<in T> where T: DbEntity
    {
        string CollectionName { get; }
        string GenerateNewId(T entity);
        PartitionKey ResolvePartitionKey(string entityId);
    }
}
