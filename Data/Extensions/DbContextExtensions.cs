using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using Domain.Abstract;

namespace Data.Extensions;

public static class DbContextExtensions
{
    public static IQueryable<TEntity> IncludeProperties<TEntity>(this DbContext dbContext,
        params Expression<Func<TEntity, object>>[] includeProperties) where TEntity : BaseEntity
    {
        IQueryable<TEntity> entities = dbContext.Set<TEntity>();

        foreach (var includeProperty in includeProperties)
        {
            entities = entities.Include(includeProperty);
        }

        return entities;
    }
}