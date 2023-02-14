using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using RequestOptions = Microsoft.Azure.Documents.Client.RequestOptions;

namespace T002.Infrastructure.Data
{
	public class CosmosDbClient : ICosmosDbClient
	{
		private readonly string _dbName;
		private readonly string _collectionName;
        private readonly IDocumentClient _documentClient;

		public CosmosDbClient(string databaseName, string collectionName, IDocumentClient documentClient)
		{
			_dbName = databaseName;
			_collectionName = collectionName;
			_documentClient = documentClient;
		}

        public async Task<Document> ReadDocumentAsync(string documentId, RequestOptions options = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return await _documentClient.ReadDocumentAsync(
                UriFactory.CreateDocumentUri(_dbName, _collectionName, documentId), options, cancellationToken);
        }

        public async Task<Document> CreateDocumentAsync(object document, RequestOptions options = null,
            bool disableAutomaticIdGeneration = false, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await _documentClient.CreateDocumentAsync(
                UriFactory.CreateDocumentCollectionUri(_dbName, _collectionName), document, options,
                disableAutomaticIdGeneration, cancellationToken);
        }

        public async Task<Document> ReplaceDocumentAsync(string documentId, object document,
            RequestOptions options = null, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await _documentClient.ReplaceDocumentAsync(
                UriFactory.CreateDocumentUri(_dbName, _collectionName, documentId), document, options,
                cancellationToken);
        }

        public async Task<Document> DeleteDocumentAsync(string documentId, RequestOptions options = null,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            return await _documentClient.DeleteDocumentAsync(
                UriFactory.CreateDocumentUri(_dbName, _collectionName, documentId), options, cancellationToken);
        }


    }
}
