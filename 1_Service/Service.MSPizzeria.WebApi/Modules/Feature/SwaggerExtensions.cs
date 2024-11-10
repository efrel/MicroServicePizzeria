using Microsoft.OpenApi.Models;

namespace Service.MSPizzeria.WebApi.Modules.Feature;

public static class SwaggerExtensions
{
    public static IServiceCollection AddSwaggerE(this IServiceCollection services)
    {

        services.AddSwaggerGen(options =>
        {
            var groupName = "v1";

            options.SwaggerDoc(groupName, new OpenApiInfo
            {
                Title = $"Api Pizzeria {groupName}",
                Version = groupName,
                Description = "Micro service Pizzeria",
                Contact = new OpenApiContact
                {
                    Name = "Efrel López",
                    Email = "efli95.ealc@gmail.com",
                    Url = new Uri("https://github.com/efrel/MicroServicePizzeria"),
                }
            });

            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "Encabezado de autorización JWT utilizando el esquema Bearer. \r\n\r\n Ingrese 'Bearer' [space] y luego su token en la entrada de texto a continuación.\r\n\r\nEjemplo: \"Bearer 1safsfsdfdfd\"",
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement {
                {
                    new OpenApiSecurityScheme {
                        Reference = new OpenApiReference {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] {}
                }
            });
        });

        return services;
    }
}