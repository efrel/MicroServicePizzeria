using Domain.MSPizzeria.Entity.Models.v1;
using Domain.MSPizzeria.Interface;
using Infrastructure.MSPizzeria.Interface;

namespace Domain.MSPizzeria.Core;

public class ProductDomain : IProductDomain
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
    private readonly IProductRepository _ResultRepository;
    #endregion

    #region CONSTRUCTOR DE CLASE
    public ProductDomain(IProductRepository ProductRepository)
    {
        _ResultRepository = ProductRepository;
    }
    #endregion
    
    public async Task<Product> GetByIdAsync(int id)
    {
        return await _ResultRepository.GetByIdAsync(id);
    }

    public async Task<IEnumerable<Product>> GetAllAsync()
    {
        return await _ResultRepository.GetAllAsync();
    }

    public async Task<IEnumerable<Product>> GetByFilterAsync(bool? isAvailable, string category)
    {
        return await _ResultRepository.GetByFilterAsync(isAvailable, category);
    }

    public async Task<Product> CreateAsync(Product product)
    {
        return await _ResultRepository.CreateAsync(product);
    }

    public async Task<Product> UpdateAsync(Product product)
    {
        return await _ResultRepository.UpdateAsync(product);
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