using Application.MSPizzeria.Commands.Order.Create;
using Application.MSPizzeria.Commands.Order.Update;
using Application.MSPizzeria.Commands.Product.Create;
using Application.MSPizzeria.Commands.Product.Update;
using Application.MSPizzeria.Commands.User.Register;
using Application.MSPizzeria.Queries.Order.GetAll;
using Application.MSPizzeria.Queries.Order.GetById;
using Application.MSPizzeria.Queries.Product.GetAll;
using Application.MSPizzeria.Queries.Product.GetById;
using Application.MSPizzeria.Queries.User.Login;

namespace Service.MSPizzeria.WebApi.Modules.MediatR;

public static class MediatrExtensions
{
    public static IServiceCollection AddMediatr(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
        {
            cfg.RegisterServicesFromAssembly(typeof(LoginUserQuery).Assembly);
            cfg.RegisterServicesFromAssembly(typeof(RegisterUserCommand).Assembly);
            
            cfg.RegisterServicesFromAssembly(typeof(CreateProductCommand).Assembly);
            cfg.RegisterServicesFromAssembly(typeof(UpdateProductCommand).Assembly);
            cfg.RegisterServicesFromAssembly(typeof(GetAllProductsQuery).Assembly);
            cfg.RegisterServicesFromAssembly(typeof(GetProductByIdQuery).Assembly);
            
            cfg.RegisterServicesFromAssembly(typeof(CreateOrderCommand).Assembly);
            cfg.RegisterServicesFromAssembly(typeof(UpdateOrderCommand).Assembly);
            cfg.RegisterServicesFromAssembly(typeof(GetAllOrdersQuery).Assembly);
            cfg.RegisterServicesFromAssembly(typeof(GetOrderByIdQuery).Assembly);
        });

        return services;
    }
}