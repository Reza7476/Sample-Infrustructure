using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Sample.Commons.Interfaces;
using Sample.Core.Entities.Generals;

namespace Sample.Persistence.EF.Interceptors;

internal class AuditingInterceptor: SaveChangesInterceptor
{

    private readonly ICurrentUserService _currentUserService;

    public AuditingInterceptor(ICurrentUserService currentUserService)
    {
        _currentUserService = currentUserService;
    }

    public override InterceptionResult<int> SavingChanges(DbContextEventData eventData, InterceptionResult<int> result)
    {
        ApplyAuditing(eventData.Context);
        return base.SavingChanges(eventData, result);
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData, InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        ApplyAuditing(eventData.Context);
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }

    private void ApplyAuditing(DbContext? context)
    {
        if (context == null) return;

        var now = DateTime.UtcNow;

        foreach (var entry in context.ChangeTracker.Entries())
        {
            if (entry.State == EntityState.Added)
            {
                if (entry.Entity is BaseEntity baseEntity)
                {
                    baseEntity.CreatedBy = _currentUserService.UserId;
                    baseEntity.CreatedAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
                }
            }
            else if (entry.State == EntityState.Modified)
            {
                if (entry.Entity is BaseEntity baseEntity)
                {
                    baseEntity.UpdatedAt = DateTimeOffset.UtcNow.ToUnixTimeSeconds();
                    baseEntity.UpdatedBy = _currentUserService.UserId;
                }
            }
        }
    }

}
