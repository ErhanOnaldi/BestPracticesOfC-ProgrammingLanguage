namespace App.Domain.Entities;
public class Product : IAuditEntity
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
    public Category Category { get; set; } = null!;
    public int CategoryId { get; set; }

    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
}