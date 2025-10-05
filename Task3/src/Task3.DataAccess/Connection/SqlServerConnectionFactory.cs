using System.Data;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace Task3.DataAccess.Connection;

public class SqlServerConnectionFactory : IConnectionFactory
{
    private readonly string _connectionString;

    public SqlServerConnectionFactory(IConfiguration configuration)
    {
        var conStr = configuration.GetConnectionString("DefaultConnection") ??
         throw new InvalidOperationException(
                $"Failed to find connection string in appsettings.json.");

        _connectionString = conStr;
    }

    public IDbConnection Create() => new SqlConnection(_connectionString);
}
