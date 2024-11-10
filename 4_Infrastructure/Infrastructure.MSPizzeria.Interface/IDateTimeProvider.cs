namespace Infrastructure.MSPizzeria.Interface;

public interface IDateTimeProvider
{
    DateTime UtcNow { get; }
}