# AWS MSSQL Connection Example

This example demonstrates how to connect to a Microsoft SQL Server instance hosted on AWS using `.NET`.

```csharp
var connection = new AwsMssqlConnection("Server=<endpoint>;Database=<db>;User Id=<user>;Password=<pwd>;");
await connection.TestConnectionAsync();
```

Replace the connection string values with your AWS RDS SQL Server endpoint and credentials.
