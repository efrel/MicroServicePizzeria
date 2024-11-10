using Domain.MSPizzeria.Core;
using Domain.MSPizzeria.Interface;
using Infrastructure.MSPizzeria.Data;
using Infrastructure.MSPizzeria.Interface;
using Infrastructure.MSPizzeria.Repository;
using Infrastructure.MSPizzeria.Service;
using Transversal.MSPizzeria.Common;
using Transversal.MSPizzeria.Logging;

namespace Service.MSPizzeria.WebApi.Modules.Injection;

public static class InjectionExtensions
{
    public static IServiceCollection addInjection(
        this IServiceCollection services,
        IConfiguration Configuration
    )
    {
        #region CARGAR ARCHIVO DE CONFIGURACIONES
        //Singleton para cargar 1 vez la configuracion y reutilizarla posteriormente
        services.AddSingleton<IConfiguration>(Configuration);
        #endregion

        #region INYECCION DEL SERVICIO PARA LA CONEXION DB
        services.AddSingleton<IConnectionFactory, ConnectionFactory>();
        #endregion

        #region INYECCION DOMINO
        //AddScoped permite que se instancie 1 vez por cada solicitud
        services.AddScoped<IUserDomain, UserDomain>();
        services.AddScoped<IProductDomain, ProductDomain>();
        services.AddScoped<IOrderDomain, OrderDomain>();
        #endregion
        
        #region INYECCION INFRASTRUTURE
        
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        
        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        
        #endregion


        #region INYECCION TRANSVERSAL

        services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>)); //Se usa typeof porque es una clase generica <T>

        #endregion

        
        return services;
    }
}