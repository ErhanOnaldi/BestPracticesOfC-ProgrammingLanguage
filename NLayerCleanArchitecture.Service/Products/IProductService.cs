using NLayerCleanArchitecture.Repository.Products;

namespace NLayerCleanArchitecture.Service.Products;

public interface IProductService
{
    Task<List<Product>> GetMostExpensiveProductsAsync(int count);
}