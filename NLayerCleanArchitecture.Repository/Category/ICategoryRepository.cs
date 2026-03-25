namespace NLayerCleanArchitecture.Repository.Category;

public interface ICategoryRepository : IGenericRepository<Category>
{
    Task<Category?> GetCategoryWithProducts(int categoryId);
    IQueryable<Category> GetCategoryWithProducts();
}