using Domain.Abstract;
using System.Linq.Expressions;

namespace Service.Abstract.Base;

public interface IBaseEntityService<TEntity> where TEntity : BaseEntity
{
    Task<IEnumerable<TEntity>> GetAllAsNoTrackingAsync(CancellationToken cancellationToken);

    Task<IEnumerable<TEntity>> GetAllWithIncludeAsNoTrackingAsync(CancellationToken cancellationToken, params Expression<Func<TEntity, object>>[] includeProperties);

    Task<TEntity> GetByIdAsNoTrackingAsync(long id, CancellationToken cancellationToken);

    Task<TEntity> GetByIdWithIncludeAsNoTrackingAsync(long id, CancellationToken cancellationToken, params Expression<Func<TEntity, object>>[] includeProperties);

    Task<IEnumerable<TEntity>> GetWhereWithIncludeAsNoTrackingAsync(Expression<Func<TEntity,bool>> predicate,CancellationToken cancellationToken,params Expression<Func<TEntity, object>>[] includeProperties);
}