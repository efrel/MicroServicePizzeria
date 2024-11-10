using Domain.MSPizzeria.Entity.Models.v1;

namespace Domain.MSPizzeria.Interface;

public interface IProductDomain
{
    Task<Product> GetByIdAsync(int id);
    Task<IEnumerable<Product>> GetAllAsync();
    Task<IEnumerable<Product>> GetByFilterAsync(bool? isAvailable, string category);
    Task<Product> CreateAsync(Product product);
    Task<Product> UpdateAsync(Product product);
    Task<bool> DeleteAsync(int id);
    Task<bool> ExistsAsync(int id);
}