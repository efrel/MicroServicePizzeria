using Application.MSPizzeria.Validator;

namespace Service.MSPizzeria.WebApi.Modules.Validator;

public static class ValidatorExtensions
{
    public static IServiceCollection AddValidator(this IServiceCollection services)
    {
        //AddTransient permite que se cree una nueva instancia por cada petición
        services.AddTransient<UserInfoDTO_Validator>();
        services.AddTransient<RegisterRequestDTO_Validator>();
        services.AddTransient<CreateProductDTO_Validator>();
        services.AddTransient<UpdateProductDTO_Validator>();
        
        return services;
    }
}