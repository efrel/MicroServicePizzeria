using AutoMapper;

// MIS REFERENCIAS
using Transversal.MSPizzeria.Mapper;

namespace Service.MSPizzeria.WebApi.Modules.Mapper;

public static class MapperExtensions
{
    public static IServiceCollection AddMapper(this IServiceCollection services)
    {
        var mappinConfig = new MapperConfiguration(mc =>
        {
            mc.AddProfile(new MappingProfile());
        });
        
        IMapper mapper = mappinConfig.CreateMapper();
        
        services.AddSingleton(mapper);

        return services;
    }
}