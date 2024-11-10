using Infrastructure.MSPizzeria.Data;
using Infrastructure.MSPizzeria.Interface;
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
        // services.AddScoped<ICategoryDomain, CategoryDomain>();
        #endregion
        
        #region INYECCION INFRASTRUTURE

        services.AddSingleton<IDateTimeProvider, DateTimeProvider>();
        
        #endregion


        #region INYECCION TRANSVERSAL

        services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>)); //Se usa typeof porque es una clase generica <T>

        #endregion

        
        return services;
    }
}