using System.Reflection;
using Microsoft.EntityFrameworkCore;
using NLayerCleanArchitecture.Repository.Products;
using NLayerCleanArchitecture.Repository.Category;


namespace NLayerCleanArchitecture.Repository;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Product> Products { get; set; } = default!;
    public DbSet<Category.Category> Categories { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //Bu kod ile tek tek configleri geçmek yerine, EntityConfiguration patternine uyan tüm sınıflar uygulanır.
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}