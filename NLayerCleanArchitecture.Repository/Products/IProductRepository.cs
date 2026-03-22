namespace NLayerCleanArchitecture.Repository.Products;

public interface IProductRepository : IGenericRepository<Product>
{
    public Task<List<Product>> GetMostExpensiveProductsAsync(int count);
    
    
}