using Domain;
using Service.Abstract.Base;

namespace Service.Abstract;

public interface IAgentService : IBaseEntityService<Agent>
{
    Task<Agent> GetByFiscalCodeWithAccountsAsync(long fiscalCode, CancellationToken cancellationToken);
}