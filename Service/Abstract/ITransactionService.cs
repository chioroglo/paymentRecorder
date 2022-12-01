using Domain;
using Service.Abstract.Base;

namespace Service.Abstract;

public interface ITransactionService : IBaseEntityService<Transaction>
{
    Task<Transaction> AddAsync(Transaction dto, CancellationToken cancellationToken);

    Task<Transaction> EditAsync(Transaction dto, CancellationToken cancellationToken);

    Task RemoveAsync(long id, Guid version, CancellationToken cancellationToken);
}