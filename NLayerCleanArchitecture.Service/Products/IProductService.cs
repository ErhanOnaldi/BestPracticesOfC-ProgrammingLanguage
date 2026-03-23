using NLayerCleanArchitecture.Repository.Products;

namespace NLayerCleanArchitecture.Service.Products;

public interface IProductService
{
    Task<ServiceResult<List<ProductResponseDto>>> GetMostExpensiveProductsAsync(int count);
}