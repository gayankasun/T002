using System;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace Examples
{
    /// <summary>
    /// Demonstrates how to connect to a SQL Server instance hosted on AWS.
    /// </summary>
    public class AwsMssqlConnection
    {
        private readonly string _connectionString;

        public AwsMssqlConnection(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        /// <summary>
        /// Opens a connection and executes a simple query to verify connectivity.
        /// </summary>
        public async Task TestConnectionAsync()
        {
            using var connection = new SqlConnection(_connectionString);
            await connection.OpenAsync();

            using var command = new SqlCommand("SELECT @@VERSION", connection);
            var version = (string)await command.ExecuteScalarAsync();
            Console.WriteLine($"Connected to: {version}");
        }
    }
}
