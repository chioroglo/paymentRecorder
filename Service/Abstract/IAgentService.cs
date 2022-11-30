using Domain;
using Service.Abstract.Base;

namespace Service.Abstract;

public interface IAgentService : IBaseEntityService<Agent>
{
    Task<Agent> GetByFiscalCodeWithAccountsAsync(long fiscalCode, CancellationToken cancellationToken);

    Task<Agent> Add(Agent entity, CancellationToken cancellationToken);

    Task RemoveAsync(long id, Guid version, CancellationToken cancellationToken);

    Task<Agent> UpdateAsync(Agent entity, CancellationToken cancellationToken);
}