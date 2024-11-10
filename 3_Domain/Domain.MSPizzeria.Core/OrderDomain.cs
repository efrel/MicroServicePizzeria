using Domain.MSPizzeria.Entity.Models.v1;
using Domain.MSPizzeria.Interface;
using Infrastructure.MSPizzeria.Interface;

namespace Domain.MSPizzeria.Core;

public class OrderDomain : IOrderDomain
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
    private readonly IOrderRepository _ResultRepository;
    #endregion

    #region CONSTRUCTOR DE CLASE
    public OrderDomain(IOrderRepository OrderRepository)
    {
        _ResultRepository = OrderRepository;
    }
    #endregion
    
    public async Task<Order> GetByIdAsync(int id, bool includeDetails = true)
    {
        return await _ResultRepository.GetByIdAsync(id, includeDetails);
    }

    public async Task<IEnumerable<Order>> GetAllAsync(bool includeDetails = true)
    {
        return await _ResultRepository.GetAllAsync(includeDetails);
    }

    public async Task<IEnumerable<Order>> GetByCustomerIdAsync(int customerId)
    {
        return await _ResultRepository.GetByCustomerIdAsync(customerId);
    }

    public async Task<IEnumerable<Order>> GetByStatusAsync(string status)
    {
        return await _ResultRepository.GetByStatusAsync(status);
    }

    public async Task<Order> CreateAsync(Order order)
    {
        return await _ResultRepository.CreateAsync(order);
    }

    public async Task<Order> UpdateAsync(Order order)
    {
        return await _ResultRepository.UpdateAsync(order);
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _ResultRepository.DeleteAsync(id);
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _ResultRepository.ExistsAsync(id);
    }
}