using NLayerCleanArchitecture.Repository.Products;

namespace NLayerCleanArchitecture.Service.Products;

public interface IProductService
{
    Task<ServiceResult<List<Product>>> GetMostExpensiveProductsAsync(int count);
}