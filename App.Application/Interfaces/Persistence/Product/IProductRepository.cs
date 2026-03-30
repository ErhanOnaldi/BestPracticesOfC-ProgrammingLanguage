namespace App.Application.Interfaces.Persistence.Product;

public interface IProductRepository : IGenericRepository<Domain.Entities.Product>
{
    public Task<List<Domain.Entities.Product>> GetMostExpensiveProductsAsync(int count);
    
    
}