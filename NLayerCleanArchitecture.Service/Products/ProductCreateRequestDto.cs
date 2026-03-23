namespace NLayerCleanArchitecture.Service.Products;

public record ProductCreateRequestDto(string Name, decimal Price,int Stock,string Description);