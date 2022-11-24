using Domain;
using Service.Abstract.Base;

namespace Service.Abstract;

public interface IBankService : IBaseEntityService<Bank>
{
    Task<Bank> GetByCodeAsync(string bankCode, CancellationToken cancellationToken);
}