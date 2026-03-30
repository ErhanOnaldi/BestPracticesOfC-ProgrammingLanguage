using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using App.Domain.Entities;

namespace App.Persistence.Category;

public class CategoryConfiguration : IEntityTypeConfiguration<Domain.Entities.Category>
{
    public void Configure(EntityTypeBuilder<Domain.Entities.Category> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Name).IsRequired().HasMaxLength(50);
    }
}