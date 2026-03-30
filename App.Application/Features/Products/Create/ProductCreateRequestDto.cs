namespace App.Application.Features.Products.Create;

public record ProductCreateRequestDto(string Name, decimal Price,int Stock,string Description, int CategoryId);