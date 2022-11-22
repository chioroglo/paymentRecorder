using Domain.Abstract;
using System.Linq.Expressions;

namespace Service.Abstract.Base;

public interface IBaseEntityService<TEntity> where TEntity : BaseEntity
{
    Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken);

    Task<TEntity> GetByIdAsync(long id, CancellationToken cancellationToken);

    Task<TEntity> GetByIdWithIncludeAsync(long id, CancellationToken cancellationToken, params Expression<Func<TEntity, object>>[] includeProperties);

    Task<TEntity> Add(TEntity entity, CancellationToken cancellationToken);

    Task RemoveAsync(long id,Guid version, CancellationToken cancellationToken);

    Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken);
}