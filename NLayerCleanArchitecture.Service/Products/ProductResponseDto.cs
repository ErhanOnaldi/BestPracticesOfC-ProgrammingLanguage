namespace NLayerCleanArchitecture.Service.Products;

public class ProductResponseDto //mümjün olduğunca immutable olmalı
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int Stock { get; set; }
}