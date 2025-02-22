using Microsoft.Extensions.DependencyInjection;
using StructuredMarket.Application.Features.Users.Queries;
using System.Reflection;

public static class DependencyInjection
{
    public static IServiceCollection AddApplicationLayer(this IServiceCollection services)
    {
        var assembly = typeof(GetUserByIdQueryHandler).Assembly;
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));
        services.AddAutoMapper(assembly);

        return services;
    }
}
