using Infrastructure.MSPizzeria.Interface;

namespace Infrastructure.MSPizzeria.Service;

public class DateTimeProvider : IDateTimeProvider
{
    public DateTime UtcNow => DateTime.UtcNow;
}