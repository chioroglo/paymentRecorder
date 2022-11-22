using Domain.Abstract;

namespace Common.Exceptions.ExceptionMessages;

public static class ValidationExceptionMessages
{
    public static string EntityWasNotFoundBecause<TEntity>(string reason) where TEntity : BaseEntity => $"{typeof(TEntity).Name} was not found, because {reason}";

    public static string ThisMethodRequiresHttpHeader(string httpHeaderName) => $"{httpHeaderName} header required for this operation";
}