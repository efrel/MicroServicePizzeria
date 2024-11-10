using Domain.MSPizzeria.Entity.Models.v1;
using Domain.MSPizzeria.Interface;
using Infrastructure.MSPizzeria.Interface;

namespace Domain.MSPizzeria.Core;

public class UserDomain : IUserDomain
{
    #region INFORMACION
    /*
     * Desde la capa de dominio accedemos a la capa de infraestructura a traves de las interfaces
     * de esta forma los componentes estan desacoplados.
     * Es importante mencionar que este acceso se hace a traves la inyeccion de dependencias del repositorio
     * en el constructor de la clase.
     * Para este ejemplo no tenemos logica de negocio, pero es aquí donde se debe incluir todas las reglas
     * del negocio.
     */
    #endregion

    #region PROPIEDADES DE CLASES
    private readonly IUserRepository _ResultRepository;
    #endregion

    #region CONSTRUCTOR DE CLASE
    public UserDomain(IUserRepository userRepository)
    {
        _ResultRepository = userRepository;
    }
    #endregion
    
    public async Task<bool> CreateAsync(ApplicationUser user, string password)
    {
        return await _ResultRepository.CreateAsync(user, password);
    }

    public async Task<bool> UpdateAsync(ApplicationUser user)
    {
        return await _ResultRepository.UpdateAsync(user);
    }

    public async Task<bool> DeleteAsync(string id)
    {
        return await _ResultRepository.DeleteAsync(id);
    }

    public async Task<ApplicationUser> GetByEmailAsync(string email)
    {
        return await _ResultRepository.GetByEmailAsync(email);
    }

    public async Task<ApplicationUser> GetByIdAsync(string id)
    {
        return await _ResultRepository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<ApplicationUser>> GetAllAsync()
    {
        return await _ResultRepository.GetAllAsync();
    }

    public async Task<bool> AddToRoleAsync(ApplicationUser user, string role)
    {
        return await _ResultRepository.AddToRoleAsync(user, role);
    }

    public async Task<IList<string>> GetRolesAsync(ApplicationUser user)
    {
        return await _ResultRepository.GetRolesAsync(user);
    }
}