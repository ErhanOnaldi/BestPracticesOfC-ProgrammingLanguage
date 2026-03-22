using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace NLayerCleanArchitecture.Repository.Products;

public class ProductRepository(AppDbContext dbContext) : GenericRepository<Product>(dbContext), IProductRepository
{
    public Task<List<Product>> GetMostExpensiveProductsAsync(int count)
    {
        return DbContext.Products.OrderByDescending(x => x.Price).Take(count).ToListAsync();
    }
}