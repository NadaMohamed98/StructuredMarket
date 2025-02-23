using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using StructuredMarket.Application.Interfaces.Repositories;
using StructuredMarket.Application.Interfaces.Services;
using StructuredMarket.Domain.Entities;
using StructuredMarket.Infrastructure.Authentication;
using StructuredMarket.Infrastructure.Common;
using StructuredMarket.Infrastructure.Data;
using StructuredMarket.Infrastructure.Repositories;
using StructuredMarket.Infrastructure.Repositories.OrderItemRepo;
using StructuredMarket.Infrastructure.Repositories.OrderRepo;
using StructuredMarket.Infrastructure.Repositories.PermissionRepo;
using StructuredMarket.Infrastructure.Repositories.ProductRepo;
using StructuredMarket.Infrastructure.Repositories.RoleRepo;
using StructuredMarket.Infrastructure.Repositories.UserRepo;
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, ConnectionStrings connectionString)
    {
        services.AddDbContext<StructuredMarketDbContext>(options =>
            options.UseSqlServer(connectionString.DefaultConnection));

        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

        // repositories
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<IOrderRepository, OrderRepository>();
        services.AddScoped<IOrderItemRepository, OrderItemRepository>();

        services.AddScoped<IPermissionRepository, PermissionRepository>();
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<IUserRoleRepository, UserRoleRepository>();

        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

        return services;
    }

}
