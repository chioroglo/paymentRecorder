using Microsoft.EntityFrameworkCore;

namespace Service.Extensions;

public static class EntityValidationUtils
{
    public static void ValidateRowVersionEqualityThrowDbConcurrencyExceptionIfNot(Guid entityInDatabaseConcurrencyToken, Guid requestEntityConcurrencyToken)
    {
        if (entityInDatabaseConcurrencyToken != requestEntityConcurrencyToken)
        {
            throw new DbUpdateConcurrencyException($"Concurrency conflict, please actualize your information");
        }
    }
}