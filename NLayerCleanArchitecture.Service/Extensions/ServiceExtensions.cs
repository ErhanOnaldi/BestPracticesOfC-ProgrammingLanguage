using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NLayerCleanArchitecture.Service.Products;

namespace NLayerCleanArchitecture.Service.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IProductService, ProductService>();
        return services;
    }
    
}