namespace NLayerCleanArchitecture.Service.Products.Create;

public record ProductCreateRequestDto(string Name, decimal Price,int Stock,string Description);