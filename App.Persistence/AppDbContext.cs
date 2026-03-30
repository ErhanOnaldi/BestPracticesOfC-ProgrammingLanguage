using System.Reflection;
using App.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace App.Persistence;

public class AppDbContext(DbContextOptions<AppDbContext> options) : DbContext(options)
{
    public DbSet<Product> Products { get; set; } = default!;
    public DbSet<Domain.Entities.Category> Categories { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //Bu kod ile tek tek configleri geçmek yerine, EntityConfiguration patternine uyan tüm sınıflar uygulanır.
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        base.OnModelCreating(modelBuilder);
    }
}