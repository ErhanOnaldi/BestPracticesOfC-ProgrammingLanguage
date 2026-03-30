using App.Application.Features.Products.Dto;

namespace App.Application.Features.Categories.Dto;

public record CategoryWithProductResponseDto(int Id, string Name, List<ProductResponseDto> Products);