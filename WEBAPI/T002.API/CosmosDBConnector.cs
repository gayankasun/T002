using Microsoft.Azure.Cosmos;
using System.Threading.Tasks;
using T002.Core.Models;

namespace T002.API.T002.API
{
    public class CosmosDBConnector
    {
        public IT002Configuration Configuration { get; private set; }
        private static CosmosClient cosmosClient = null;
        private Database database = null;
        private Container container = null;

        public CosmosDBConnector()
        {
            if (cosmosClient == null)
            {
                cosmosClient = new CosmosClient(this.Configuration.DatabaseSettings.EndpointUri,
                    this.Configuration.DatabaseSettings.PrimaryKey, new CosmosClientOptions() { ApplicationName = "SGWebhook" });
            }

            this.database = cosmosClient.GetDatabase(this.Configuration.DatabaseSettings.DatabaseId);
            this.container = this.database.GetContainer(this.Configuration.DatabaseSettings.ContainerId);

        }

        public async Task<Invoice> Add(Invoice request)
        {
            return await this.container.CreateItemAsync<Invoice>(request, new PartitionKey(request.Id));
        }

        public async Task<Invoice> Update(Invoice request)
        {
            return await this.container.ReplaceItemAsync<Invoice>(request, request.Id, new PartitionKey(request.Id));
        }

        public async Task<Invoice> FindByid(Invoice request)
        {
            return await this.container.ReadItemAsync<Invoice>(request.Id, new PartitionKey(request.Id));
        }
    }
}
