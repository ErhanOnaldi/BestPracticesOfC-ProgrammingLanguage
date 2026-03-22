using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;

namespace NLayerCleanArchitecture.Repository;

public class GenericRepository<T>(AppDbContext dbContext):IGenericRepository<T> where T : class
{
    protected readonly AppDbContext DbContext = dbContext;
    private readonly DbSet<T> _dbSet = dbContext.Set<T>();
    
    public IQueryable<T> GetAll() => _dbSet.AsQueryable().AsNoTracking();

    public IQueryable<T> Where(Expression<Func<T, bool>> predicate) => _dbSet.Where(predicate).AsQueryable().AsNoTracking();

    public ValueTask<T?> GetByIdAsync(int id) => _dbSet.FindAsync(id);

    public async ValueTask AddAsync(T entity) => await _dbSet.AddAsync(entity);

    public void Update(T entity) => _dbSet.Update(entity);

    public void Delete(T entity) => _dbSet.Remove(entity);
}