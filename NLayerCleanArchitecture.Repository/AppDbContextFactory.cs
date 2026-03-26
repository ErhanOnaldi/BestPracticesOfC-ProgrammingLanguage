using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace NLayerCleanArchitecture.Repository;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        // appsettings.json'ı API projesinden oku
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "..", "NLayerCleanArchitecture.API"))
            .AddJsonFile("appsettings.json", optional: false)
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

        var connectionString = configuration.GetSection(ConnectionStringOption.Key)
            .Get<ConnectionStringOption>();

        optionsBuilder.UseNpgsql(connectionString!.DefaultConnection, sqlOptions =>
        {
            sqlOptions.MigrationsAssembly(typeof(RepositoryAssembly).Assembly.FullName);
        });

        return new AppDbContext(optionsBuilder.Options);
    }
}
