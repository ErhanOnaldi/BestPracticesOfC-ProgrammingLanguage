using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace NLayerCleanArchitecture.Repository.Extensions;

public static class RepositoryExtensions
{
    public static IServiceCollection AddRepositories(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
        {
            var connectionString = configuration.GetSection(ConnectionStringOption.Key).Get<ConnectionStringOption>();
            options.UseNpgsql(connectionString!.DefaultConnection, sqlOptions =>
            {
                //Migration'ın Repository kullanılması için
                sqlOptions.MigrationsAssembly(typeof(RepositoryAssembly).Assembly.FullName);
            });
        });
        return services;
    }
}