using NLayerCleanArchitecture.Service.Products;

namespace NLayerCleanArchitecture.Service.Category;

public record CategoryWithProductResponseDto(int Id, string Name, List<ProductResponseDto> Products);