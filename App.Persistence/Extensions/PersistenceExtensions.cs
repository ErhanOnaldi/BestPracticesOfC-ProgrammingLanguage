using App.Application.Interfaces.Persistence;
using App.Application.Interfaces.Persistence.Category;
using App.Application.Interfaces.Persistence.Product;
using App.Domain.Options;
using App.Persistence.Category;
using App.Persistence.Products;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace App.Persistence.Extensions;

public static class PersistenceExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            var connectionString = configuration.GetSection(ConnectionStringOption.Key).Get<ConnectionStringOption>();
            options.UseNpgsql(connectionString!.DefaultConnection, sqlOptions =>
            {
                //Migration'ın Repository kullanılması için
                sqlOptions.MigrationsAssembly(typeof(PersistenceAssembly).Assembly.FullName);
            });
        });
        
        services.AddScoped<IProductRepository, ProductRepository>();
        services.AddScoped<ICategoryRepository, CategoryRepository>();
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
        services.AddScoped<IUnitOfWork, UnitOfWork>();
            
        return services;
    }
}