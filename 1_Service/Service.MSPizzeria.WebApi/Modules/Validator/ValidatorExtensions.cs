namespace Service.MSPizzeria.WebApi.Modules.Validator;

public static class ValidatorExtensions
{
    public static IServiceCollection AddValidator(this IServiceCollection services)
    {
        //AddTransient permite que se cree una nueva instancia por cada petición
        // services.AddTransient<CategoryDTO_Validator>();
        
        return services;
    }
}