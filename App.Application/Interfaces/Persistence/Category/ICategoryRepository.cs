namespace App.Application.Interfaces.Persistence.Category;

using App.Domain.Entities;

public interface ICategoryRepository : IGenericRepository<Category>
{
    Task<Category?> GetCategoryWithProducts(int categoryId);
    Task<List<Category>> GetAllCategoriesWithTheirProductsAsync();
}