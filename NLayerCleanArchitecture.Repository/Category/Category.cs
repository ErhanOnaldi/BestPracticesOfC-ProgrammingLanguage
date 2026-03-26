using NLayerCleanArchitecture.Repository.Products;

namespace NLayerCleanArchitecture.Repository.Category;

public class Category : IAuditEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public List<Product>? Products { get; set; }

    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
}