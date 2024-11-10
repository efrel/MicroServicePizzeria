using Domain.MSPizzeria.Entity.Models.v1;

namespace Domain.MSPizzeria.Interface;

public interface IUserDomain
{
    Task<bool> CreateAsync(ApplicationUser user, string password);
    Task<bool> UpdateAsync(ApplicationUser user);
    Task<bool> DeleteAsync(string id);
    Task<ApplicationUser> GetByEmailAsync(string email);
    Task<ApplicationUser> GetByIdAsync(string id);
    Task<IEnumerable<ApplicationUser>> GetAllAsync();
    Task<bool> AddToRoleAsync(ApplicationUser user, string role);
    Task<IList<string>> GetRolesAsync(ApplicationUser user);
}