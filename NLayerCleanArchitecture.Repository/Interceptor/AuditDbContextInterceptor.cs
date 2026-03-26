using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace NLayerCleanArchitecture.Repository.Interceptor;

public class AuditDbContextInterceptor : SaveChangesInterceptor
{
    private static readonly Dictionary<EntityState, Action<DbContext, IAuditEntity>> Behaviours = new()
    {
        { EntityState.Added, AddBehaviour },
        { EntityState.Modified, ModifiedBehaviour }
    };

    private static void ModifiedBehaviour(DbContext context, IAuditEntity auditEntity)
    {
        auditEntity.Created = DateTime.Now;
        context.Entry(auditEntity).Property(x =>x.Updated).IsModified = false;
    }

    private static void AddBehaviour(DbContext context, IAuditEntity auditEntity)
    {
        auditEntity.Created = DateTime.Now;
        context.Entry(auditEntity).Property(x =>x.Created).IsModified = false;
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result,
        CancellationToken cancellationToken = new CancellationToken())
    {
        foreach (var entityEntry in eventData.Context!.ChangeTracker.Entries().ToList())
        {
            if(entityEntry.Entity is not IAuditEntity entity) continue;
            Behaviours[entityEntry.State](entityEntry.Context, entity);
        }
        
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}