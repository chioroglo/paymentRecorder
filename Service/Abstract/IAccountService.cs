using Domain;
using Service.Abstract.Base;

namespace Service.Abstract;

public interface IAccountService : IBaseEntityService<Account>
{
    Task<IEnumerable<Account>> GetByAgentFiscalCodeAsync(long agentFiscalCode, CancellationToken cancellationToken);

    Task<Account> Add(Account entity, CancellationToken cancellationToken);

    Task RemoveAsync(long id, Guid version, CancellationToken cancellationToken);

    Task<Account> UpdateAsync(Account entity, CancellationToken cancellationToken);
}