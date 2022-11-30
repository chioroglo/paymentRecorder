using Domain.Abstract;
using System.Linq.Expressions;

namespace Service.Abstract.Base;

public interface IBaseEntityService<TEntity> where TEntity : BaseEntity
{
    Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken);

    Task<IEnumerable<TEntity>> GetAllWithIncludeAsync(CancellationToken cancellationToken, params Expression<Func<TEntity, object>>[] includeProperties);

    Task<TEntity> GetByIdAsync(long id, CancellationToken cancellationToken);

    Task<TEntity> GetByIdWithIncludeAsync(long id, CancellationToken cancellationToken, params Expression<Func<TEntity, object>>[] includeProperties);

    Task<IEnumerable<TEntity>> GetWhereWithIncludeAsync(Expression<Func<TEntity,bool>> predicate,CancellationToken cancellationToken,params Expression<Func<TEntity, object>>[] includeProperties);
}