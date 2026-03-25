using Microsoft.EntityFrameworkCore;

namespace NLayerCleanArchitecture.Repository.Category;

public class CategoryRepository(AppDbContext dbContext) : GenericRepository<Category>(dbContext), ICategoryRepository
{
    public Task<Category?> GetCategoryWithProducts(int categoryId)
    {
        return DbContext.Categories.Include(x => x.Products).FirstOrDefaultAsync(x => x.Id == categoryId);
    }

    public IQueryable<Category> GetCategoryWithProducts()
    {
        return DbContext.Categories.Include(x => x.Products).AsQueryable();
    }
}