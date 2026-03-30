using App.Application.Features.Products.Create;
using App.Application.Features.Products.Dto;
using App.Application.Features.Products.Update;
using App.Application.Features.Products.UpdateStock;

namespace App.Application.Features.Products;

public interface IProductService
{
    Task<ServiceResult<List<ProductResponseDto>>> GetMostExpensiveProductsAsync(int count);
    Task<ServiceResult<List<ProductResponseDto>>> GetPagedProductsAsync(int pageNumber, int pageSize);
    Task<ServiceResult<List<ProductResponseDto>>> GetAllProductAsync();
    Task<ServiceResult<ProductResponseDto?>> GetProductByIdAsync(int id);
    Task<ServiceResult> UpdateStockAsync(ProductUpdateStockRequestDto productUpdateStockRequestDto);
    Task<ServiceResult<ProductCreateResponseDto>> CreateProductAsync(ProductCreateRequestDto productCreateRequestDto);
    Task<ServiceResult> UpdateProductAsync(int id, ProductUpdateRequestDto productUpdateRequestDto);
    Task<ServiceResult> DeleteProductAsync(int id);
}