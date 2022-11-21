using System.Linq.Expressions;
using Domain.Abstract;
using Microsoft.EntityFrameworkCore;

namespace Service.Abstract.Base;

public abstract class BaseEntityService<TEntity> where TEntity : BaseEntity
{
    public abstract Task<TEntity> Add(TEntity entity, CancellationToken cancellationToken);

    public abstract Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken);

    public abstract Task<TEntity> GetByIdAsync(int id, CancellationToken cancellationToken);

    public abstract Task<TEntity> GetByIdWithIncludeAsync(int id, CancellationToken cancellationToken, params Expression<Func<TEntity, object>>[] includeProperties);

    public abstract Task RemoveAsync(int id, int issuerId, CancellationToken cancellationToken);

    public abstract Task<TEntity> UpdateAsync(TEntity entity, CancellationToken cancellationToken);
    
}