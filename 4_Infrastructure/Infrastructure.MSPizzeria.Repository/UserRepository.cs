using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

// MIS REFERENCIAS
using Domain.MSPizzeria.Entity.Models.v1;
using Infrastructure.MSPizzeria.Data;
using Infrastructure.MSPizzeria.Interface;

namespace Infrastructure.MSPizzeria.Repository;

public class UserRepository  : IUserRepository
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ApplicationDBContext _context;

    public UserRepository(
        UserManager<ApplicationUser> userManager,
        ApplicationDBContext context)
    {
        _userManager = userManager;
        _context = context;
    }

    public async Task<bool> CreateAsync(ApplicationUser user, string password)
    {
        var result = await _userManager.CreateAsync(user, password);
        return result.Succeeded;
    }

    public async Task<bool> UpdateAsync(ApplicationUser user)
    {
        var result = await _userManager.UpdateAsync(user);
        return result.Succeeded;
    }

    public async Task<bool> DeleteAsync(string id)
    {
        var user = await _userManager.FindByIdAsync(id);
        if (user == null)
            return false;

        // En lugar de soft delete, usamos el mecanismo de Identity para deshabilitar el usuario
        user.LockoutEnabled = true;
        user.LockoutEnd = DateTimeOffset.MaxValue; // Bloqueo permanente
            
        var result = await _userManager.UpdateAsync(user);
        return result.Succeeded;
    }

    public async Task<ApplicationUser> GetByEmailAsync(string email)
    {
        return await _userManager.FindByEmailAsync(email);
    }

    public async Task<ApplicationUser> GetByIdAsync(string id)
    {
        return await _userManager.FindByIdAsync(id);
    }

    public async Task<IEnumerable<ApplicationUser>> GetAllAsync()
    {
        // Excluimos usuarios bloqueados permanentemente
        return await _context.Users
            .Where(u => !u.LockoutEnabled || (u.LockoutEnabled && u.LockoutEnd < DateTimeOffset.UtcNow))
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<bool> AddToRoleAsync(ApplicationUser user, string role)
    {
        var result = await _userManager.AddToRoleAsync(user, role);
        return result.Succeeded;
    }

    public async Task<IList<string>> GetRolesAsync(ApplicationUser user)
    {
        return await _userManager.GetRolesAsync(user);
    }
}