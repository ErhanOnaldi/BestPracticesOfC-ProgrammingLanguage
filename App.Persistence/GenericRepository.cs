using System.Linq.Expressions;
using App.Application.Interfaces.Persistence;
using Microsoft.EntityFrameworkCore;

namespace App.Persistence;

public class GenericRepository<T>(AppDbContext dbContext):IGenericRepository<T> where T : class
{
    protected readonly AppDbContext DbContext = dbContext;
    private readonly DbSet<T> _dbSet = dbContext.Set<T>();
    
    public Task<bool> AnyAsync(Expression<Func<T, bool>> predicate) => _dbSet.AnyAsync(predicate);
    
    public Task<List<T>> GetAllAsync()
    {
        return _dbSet.ToListAsync();
    }

    public Task<List<T>> GetAllPagedAsync(int pageNumber, int pageSize)
    {
        return _dbSet.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();
    }

    public IQueryable<T> Where(Expression<Func<T, bool>> predicate) => _dbSet.Where(predicate).AsQueryable().AsNoTracking();

    public ValueTask<T?> GetByIdAsync(int id) => _dbSet.FindAsync(id);

    public async ValueTask AddAsync(T entity) => await _dbSet.AddAsync(entity);

    public void Update(T entity) => _dbSet.Update(entity);

    public void Delete(T entity) => _dbSet.Remove(entity);
}