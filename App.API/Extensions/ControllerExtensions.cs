using System.Reflection;
using App.API.Filters;
using Microsoft.AspNetCore.Mvc;

namespace App.API.Extensions;

public static class ControllerExtensions
{
    public static IServiceCollection AddControllersWithFiltersExtension(this IServiceCollection services)
    {
        services.AddScoped(typeof(NotFoundFilter<>));
        services.AddControllers(options =>
        {
            options.Filters.Add<FluentValidationFilter>();
            options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes = true; // kendi kendine nullable olanlara hata mesajı eklemesin
        });
        return services;
    }
    
}