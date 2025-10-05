using System.Data;

namespace Task3.DataAccess.Connection;

public interface IConnectionFactory
{
    IDbConnection Create();
}
