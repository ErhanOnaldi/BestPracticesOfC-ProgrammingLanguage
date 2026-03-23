namespace NLayerCleanArchitecture.Service.Products;

public record ProductUpdateRequestDto(int Id, string Name, string? Description, decimal Price, int Stock);