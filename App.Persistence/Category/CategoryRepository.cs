using App.Application.Interfaces.Persistence.Category;
using Microsoft.EntityFrameworkCore;

namespace App.Persistence.Category;

public class CategoryRepository(AppDbContext dbContext) : GenericRepository<Domain.Entities.Category>(dbContext), ICategoryRepository
{
    public Task<Domain.Entities.Category?> GetCategoryWithProducts(int categoryId)
    {
        return DbContext.Categories.Include(x => x.Products).FirstOrDefaultAsync(x => x.Id == categoryId);
    }

    public Task<List<Domain.Entities.Category>> GetAllCategoriesWithTheirProductsAsync()
    {
        throw new NotImplementedException();
    }

    public IQueryable<Domain.Entities.Category> GetCategoryWithProducts()
    {
        return DbContext.Categories.Include(x => x.Products).AsQueryable();
    }
}