using Microsoft.Azure.Documents;
using T002.Core.Models;

namespace T002.Infrastructure.Interfaces
{
    public interface IDocumentBaseContext<in T> where T: Db
    {
        string CollectionName { get; }
        string GenerateNewId(T entity);
        PartitionKey ResolvePartitionKey(string entityId);
    }
}
