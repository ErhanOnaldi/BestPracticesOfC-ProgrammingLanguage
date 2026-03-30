using App.Application.Interfaces.Persistence.Product;
using App.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace App.Persistence.Products;

public class ProductRepository(AppDbContext dbContext) : GenericRepository<Product>(dbContext), IProductRepository
{
    public Task<List<Product>> GetMostExpensiveProductsAsync(int count)
    {
        return DbContext.Products.OrderByDescending(x => x.Price).Take(count).ToListAsync();
    }
}