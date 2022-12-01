using System.Linq.Expressions;
using Common.Exceptions;
using Data;
using Data.Extensions;
using Domain.Abstract;
using Microsoft.EntityFrameworkCore;
using static Common.Exceptions.ExceptionMessages.ValidationExceptionMessages;

namespace Service.Abstract.Base;

public abstract class BaseEntityService<TEntity> : IBaseEntityService<TEntity> where TEntity : BaseEntity
{
    protected readonly EfDbContext _db;

    protected BaseEntityService(EfDbContext db)
    {
        _db = db;
    }

    public async Task<IEnumerable<TEntity>> GetAllAsNoTrackingAsync(CancellationToken cancellationToken)
    {
        return await _db.Set<TEntity>().AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<IEnumerable<TEntity>> GetAllWithIncludeAsNoTrackingAsync(CancellationToken cancellationToken, params Expression<Func<TEntity, object>>[] includeProperties)
    {
        var query = _db.IncludeProperties(includeProperties);

        return await query.AsNoTracking().ToListAsync(cancellationToken);
    }

    public async Task<TEntity> GetByIdAsNoTrackingAsync(long id, CancellationToken cancellationToken)
    {
        return await _db.Set<TEntity>().AsNoTracking().FirstOrDefaultAsync(e => e.Id == id, cancellationToken) ??
               throw new EntityValidationException(EntityWasNotFoundBecause<TEntity>($"of ID:{id} does not exist"));

    }

    public async Task<TEntity> GetByIdWithIncludeAsNoTrackingAsync(long id, CancellationToken cancellationToken,
        params Expression<Func<TEntity, object>>[] includeProperties)
    {
        var query = _db.IncludeProperties<TEntity>(includeProperties);

        return await query.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id, cancellationToken) ??
               throw new EntityValidationException(EntityWasNotFoundBecause<TEntity>($" ID:{id} does not exist"));
    }

    public async Task<IEnumerable<TEntity>> GetWhereWithIncludeAsNoTrackingAsync(Expression<Func<TEntity, bool>> predicate, CancellationToken cancellationToken,
        params Expression<Func<TEntity, object>>[] includeProperties)
    {
        var query = _db.IncludeProperties(includeProperties);

        return await query.AsNoTracking().Where(predicate).ToListAsync(cancellationToken);
    }
}