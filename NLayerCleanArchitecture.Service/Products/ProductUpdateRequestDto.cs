namespace NLayerCleanArchitecture.Service.Products;

public record ProductUpdateRequestDto(string Name, string? Description, decimal Price, int Stock);