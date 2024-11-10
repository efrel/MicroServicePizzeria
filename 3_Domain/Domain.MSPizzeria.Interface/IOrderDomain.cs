using Domain.MSPizzeria.Entity.Models.v1;

namespace Domain.MSPizzeria.Interface;

public interface IOrderDomain
{
    Task<Order> GetByIdAsync(int id, bool includeDetails = true);
    Task<IEnumerable<Order>> GetAllAsync(bool includeDetails = true);
    Task<IEnumerable<Order>> GetByCustomerIdAsync(int customerId);
    Task<IEnumerable<Order>> GetByStatusAsync(string status);
    Task<Order> CreateAsync(Order order);
    Task<Order> UpdateAsync(Order order);
    Task<bool> DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
}