using NLayerCleanArchitecture.Repository.Products;

namespace NLayerCleanArchitecture.Service.Products;

public class ProductService(IProductRepository productRepository) : IProductService
{
    public Task<List<Product>> GetMostExpensiveProductsAsync(int count) => productRepository.GetMostExpensiveProductsAsync(count);
}