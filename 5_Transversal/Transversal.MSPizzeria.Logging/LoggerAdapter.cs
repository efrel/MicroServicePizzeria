using Microsoft.Extensions.Logging;
using Transversal.MSPizzeria.Common;

namespace Transversal.MSPizzeria.Logging;

public class LoggerAdapter<T> : IAppLogger<T>
{
    #region PROPIEDADES
    private readonly ILogger<T> _logger;
    #endregion

    #region CONSTRUCTOR
    public LoggerAdapter(ILoggerFactory loggerFactory)
    {
        _logger = loggerFactory.CreateLogger<T>();
    }
    #endregion
    
    public void LogInformation(string message, params object[] args)
    {
        _logger.LogInformation(message, args);
    }

    public void LogWarning(string message, params object[] args)
    {
        _logger.LogWarning(message, args);
    }

    public void LogError(string message, params object[] args)
    {
        _logger.LogError(message, args);
    }
}