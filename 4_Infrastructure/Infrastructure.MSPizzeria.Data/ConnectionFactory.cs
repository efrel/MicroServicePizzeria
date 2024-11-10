using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using System.Data;

using Transversal.MSPizzeria.Common;

namespace Infrastructure.MSPizzeria.Data;

public class ConnectionFactory : IConnectionFactory
{
    private readonly IConfiguration _configuration;

    public ConnectionFactory(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public IDbConnection GetConnection
    {
        get
        {
            var sqlConnection = new SqlConnection();
            if (sqlConnection == null) return null;
            
            sqlConnection.ConnectionString = _configuration.GetConnectionString("defaultConnection");
            
            sqlConnection.Open();
            return sqlConnection;
        }
    }
    
}