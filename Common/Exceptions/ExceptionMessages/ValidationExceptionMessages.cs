using Domain.Abstract;

namespace Common.Exceptions.ExceptionMessages;

public static class ValidationExceptionMessages
{
    public static string EntityWasNotFoundBecause<TEntity>(string reason) where TEntity : BaseEntity => $"{typeof(TEntity).Name} was not found, because {reason}";

    public static string ThisMethodRequiresHttpHeader(string httpHeaderName) => $"{httpHeaderName} header is required for this operation";

    public static string EntityCannotBeCreatedBecause<TEntity>(string reason) where TEntity : BaseEntity => $"{typeof(TEntity).Name} can not be created, because {reason}";

    public static string EntityCannotBeModifiedBecause<TEntity>(string reason) where TEntity : BaseEntity => $"{typeof(TEntity).Name} can not be modified, because {reason}";

    public static string EntityCannotBeDeletedBecause<TEntity>(string reason) where TEntity : BaseEntity => $"{typeof(TEntity).Name} can not be deleted,because {reason}";
}