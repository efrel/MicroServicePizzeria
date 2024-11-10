using System.Data;

namespace Transversal.MSPizzeria.Common;

public interface IConnectionFactory
{
    IDbConnection GetConnection { get; }
}