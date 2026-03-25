namespace NLayerCleanArchitecture.Service.Products.Update;

public record ProductUpdateRequestDto(string Name, string? Description, decimal Price, int Stock);