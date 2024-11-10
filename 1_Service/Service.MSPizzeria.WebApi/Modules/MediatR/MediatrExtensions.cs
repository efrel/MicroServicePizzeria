namespace Service.MSPizzeria.WebApi.Modules.MediatR;

public static class MediatrExtensions
{
    public static IServiceCollection AddMediatr(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            // cfg.RegisterServicesFromAssembly(typeof(GetByIdCategoryQuery).Assembly);
        });

        return services;
    }
}