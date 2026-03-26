using System.Reflection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using FluentValidation;
using NLayerCleanArchitecture.Repository.Category;
using NLayerCleanArchitecture.Service.Category;
using NLayerCleanArchitecture.Service.ExceptionHandlers;
using NLayerCleanArchitecture.Service.Products;

namespace NLayerCleanArchitecture.Service.Extensions;

public static class ServiceExtensions
{
    public static IServiceCollection AddServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddScoped<IProductService, ProductService>();
        services.AddScoped<ICategoryService, CategoryService>();
        
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddAutoMapper(cfg => { }, Assembly.GetExecutingAssembly());
        services.AddExceptionHandler<CriticalExceptionHandler>();
        services.AddExceptionHandler<GlobalExceptionHandler>();
        return services;
    }
    
}