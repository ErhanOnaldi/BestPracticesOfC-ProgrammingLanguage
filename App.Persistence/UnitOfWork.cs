using App.Application.Interfaces.Persistence;

namespace App.Persistence;

public class UnitOfWork(AppDbContext dbContext) : IUnitOfWork
{
    public Task<int> SaveChangesAsync(CancellationToken cancellationToken= default) => dbContext.SaveChangesAsync(cancellationToken);
}