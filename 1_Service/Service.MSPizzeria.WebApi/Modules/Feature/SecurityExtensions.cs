using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption;
using Microsoft.AspNetCore.DataProtection.AuthenticatedEncryption.ConfigurationModel;

// MIS REFERENCIAS
using Infrastructure.MSPizzeria.Service;

namespace Service.MSPizzeria.WebApi.Modules.Feature;

public static class SecurityExtensions
{
    public static IServiceCollection addHashSecurity(this IServiceCollection services)
    {
        services.AddScoped<HashService>();
        
        //Documentación: https://docs.microsoft.com/es-es/aspnet/core/security/data-protection/configuration/overview?view=aspnetcore-3.1#changing-algorithms-with-usecryptographicalgorithms
        services.AddDataProtection().UseCryptographicAlgorithms(new AuthenticatedEncryptorConfiguration()
        {
            EncryptionAlgorithm = EncryptionAlgorithm.AES_256_CBC,
            ValidationAlgorithm = ValidationAlgorithm.HMACSHA256
        });

        return services;
    }
}