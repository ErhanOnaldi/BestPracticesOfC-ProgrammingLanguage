using System.Net;
using NLayerCleanArchitecture.Repository.Products;

namespace NLayerCleanArchitecture.Service.Products;

public class ProductService(IProductRepository productRepository) : IProductService
{
    public async Task<ServiceResult<List<Product>>> GetMostExpensiveProductsAsync(int count)
    {
        var products = await productRepository.GetMostExpensiveProductsAsync(count);
        return ServiceResult<List<Product>>.Success(products);
    }

    public async Task<ServiceResult<Product>> GetProductById(int id)
    {
        var product = await productRepository.GetByIdAsync(id);
        if (product == null)
        {
            return ServiceResult<Product>.Fail("Product not found",HttpStatusCode.NotFound);
        }
        return ServiceResult<Product>.Success(product);
    }
}