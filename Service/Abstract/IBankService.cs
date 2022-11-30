using Domain;
using Service.Abstract.Base;

namespace Service.Abstract;

public interface IBankService : IBaseEntityService<Bank>
{
    Task<Bank> GetByCodeAsync(string bankCode, CancellationToken cancellationToken);

    Task<Bank> Add(Bank entity, CancellationToken cancellationToken);

    Task RemoveAsync(long id, Guid version, CancellationToken cancellationToken);

    Task<Bank> UpdateAsync(Bank entity, CancellationToken cancellationToken);
}